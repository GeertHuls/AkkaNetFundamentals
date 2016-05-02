using System;
using Akka.Actor;
using MovieStreamingConsole.Messages;

namespace MovieStreamingConsole.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(message => HandleStopMovieMessage(message));

        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                Console.WriteLine("Error: cannot start playing another movie before stopping the existing one");
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            Console.WriteLine("User is currently watching '{0}'", title);
        }

        private void HandleStopMovieMessage(StopMovieMessage message)
        {
            if (_currentlyWatching == null)
            {
                Console.WriteLine("Error: the movie cannot be stopped, nothing is playing");
            }
            else
            {
                StopPlayingCurrentMovie();
            }
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine("User has stopped watching  '{0}'", _currentlyWatching);

            _currentlyWatching = null;
        }

        protected override void PreStart()
        {
            Console.WriteLine("UserActor PreStart");
        }

        protected override void PostStop()
        {
            Console.WriteLine("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("UserActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("UserActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
    }
}