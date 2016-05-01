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

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            Console.WriteLine("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("PlaybackActor PreRestart because: {0}", reason);

            // Disable the base class call here below in case you want to 
            // prevent a PostStop on all child actors.
            base.PreRestart(reason, message);
        }

        public override void AroundPostRestart(Exception cause, object message)
        {
            Console.WriteLine("PlaybackActor PostRestart because {0}", cause);

            base.AroundPostRestart(cause, message);
        }
    }
}