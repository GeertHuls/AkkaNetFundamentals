using System;
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
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            var playbackActorRef = CreatePlaybackActor();

            playbackActorRef.Tell(new PlayMovieMessage("Some movie title", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Some movie title the revenge", 99));
            playbackActorRef.Tell(new PlayMovieMessage("Some movie title the next generation", 77));
            playbackActorRef.Tell(new PlayMovieMessage("Some movie title the pre sequel", 1));

            StopActor(playbackActorRef);

            Console.ReadLine();

            // Tell actor system (and all child actors) to shutdown
            await _movieStreamingActorSystem.Terminate();
            // Wait for actor system to finish shutting down
            await _movieStreamingActorSystem.WhenTerminated;
            Console.WriteLine("Actor system Terminated");
        }

        private static IActorRef CreatePlaybackActor()
        {
            var playbackActorProps = Props.Create<PlaybackActor>();
            return _movieStreamingActorSystem
                .ActorOf(playbackActorProps, "PlaybackActor");
        }

        private static void StopActor(IActorRef playbackActorRef)
        {
            // By consequence the poison pill triggers the PostStop call back
            playbackActorRef.Tell(PoisonPill.Instance);
        }
    }
}
