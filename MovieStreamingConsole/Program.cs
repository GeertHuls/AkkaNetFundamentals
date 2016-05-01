using System;
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
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            var playbackActorRef = CreatePlaybackActor();

            playbackActorRef.Tell(new PlayMovieMessage("Some movie title", 42));
            playbackActorRef.Tell('c');

            Console.ReadLine();

            _movieStreamingActorSystem.Terminate();
        }

        private static IActorRef CreatePlaybackActor()
        {
            var playbackActorProps = Props.Create<PlaybackActor>();
            return _movieStreamingActorSystem
                .ActorOf(playbackActorProps, "PlaybackActor");
        }
    }
}
