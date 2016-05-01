using System;
using Akka.Actor;

namespace MovieStreamingConsole.Actors
{
    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
        }

        protected override void OnReceive(object message)
        {
        }
    }
}