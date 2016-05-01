using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreamingConsole.Messages;

namespace MovieStreamingConsole.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine("Received movie title {0}", message.MovieTitle);
            Console.WriteLine("Received user id: {0}", message.UserId);
        }
    }
}