using System;
using Akka.Actor;
using MovieStreamingConsole.Messages;

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
            var playMovieMessage = message as PlayMovieMessage;
            if (playMovieMessage != null)
            {
                Console.WriteLine("Received movie title {0}", playMovieMessage.MovieTitle);
                Console.WriteLine("Received user id: {0}", playMovieMessage.UserId);
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}