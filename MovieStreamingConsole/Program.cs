using System;
using Akka.Actor;
using MovieStreamingConsole.Actors;

namespace MovieStreamingConsole
{
    class Program
    {
        private static ActorSystem _movieStreamingActorSystem;

        static void Main(string[] args)
        {
            _movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            var playbackActorProps = Props.Create<PlaybackActor>();
            var playbackActorRef = _movieStreamingActorSystem
                .ActorOf(playbackActorProps, "PlaybackActor");

            Console.ReadLine();

            _movieStreamingActorSystem.Terminate();
        }
    }
}
