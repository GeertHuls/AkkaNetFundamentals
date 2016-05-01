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
            if (message is string)
            {
                Console.WriteLine("Received movie title {0}", message);
            }
            else if (message is int)
            {
                Console.WriteLine("Received user id: {0}", message);
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}