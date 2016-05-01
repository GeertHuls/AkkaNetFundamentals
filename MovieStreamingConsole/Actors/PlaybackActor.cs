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
            Console.WriteLine("PlayMovieMessage '{0}' for user {1}",
                message.MovieTitle, message.UserId);
        }
    }
}