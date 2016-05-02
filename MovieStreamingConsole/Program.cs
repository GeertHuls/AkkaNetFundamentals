using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreamingConsole.Actors;
using MovieStreamingConsole.Messages;

namespace MovieStreamingConsole
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        static void Main(string[] args)
        {
            Start().Wait();

            Console.ReadLine();
        }

        private static async Task Start()
        {
            CreateActorSystem();

            do
            {
                ShortPause();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("enter a command and hit enter");

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    await TerminateActorSystem();
                    Environment.Exit(1);
                }

            } while (true);
        }

        private static void CreateActorSystem()
        {
            Console.WriteLine("Creating MovieStreamingActorSystem");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");


            Console.WriteLine("Creating actor supervisory hierachy");
            _movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");
        }

        private static async Task TerminateActorSystem()
        {
            // Tell actor system (and all child actors) to shutdown
            await _movieStreamingActorSystem.Terminate();
            // Wait for actor system to finish shutting down
            await _movieStreamingActorSystem.WhenTerminated;
            Console.WriteLine("Actor system Terminated");
        }

        // Perform a short pause for demo purposes to allow console to update nicely
        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}
