using System;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Common;
using MovieStreaming.Common.Actors;
using MovieStreaming.Common.Messages;

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

                //Example play command:
                // > play,42,some movie title
                var commandAndArgumentList = command.Split(',');
                if (command.StartsWith("play"))
                {
                    var userId = int.Parse(commandAndArgumentList[1]);
                    var movieTitle = commandAndArgumentList[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator")
                        .Tell(message);
                }

                //Example stop command:
                // > stop,42
                if (command.StartsWith("stop"))
                {
                    var userId = int.Parse(commandAndArgumentList[1]);

                    var message = new StopMovieMessage(userId);
                    _movieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator")
                        .Tell(message);
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
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem");
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            ColorConsole.WriteLineGray("Creating actor supervisory hierachy");
            _movieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");
        }

        private static async Task TerminateActorSystem()
        {
            // Tell actor system (and all child actors) to shutdown
            await _movieStreamingActorSystem.Terminate();
            // Wait for actor system to finish shutting down
            await _movieStreamingActorSystem.WhenTerminated;
            ColorConsole.WriteLineGray("Actor system Terminated");
        }

        // Perform a short pause for demo purposes to allow console to update nicely
        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}
