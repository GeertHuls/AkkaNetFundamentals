using System;
using Akka.Actor;
using MovieStreamingConsole.Messages;

namespace MovieStreamingConsole.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        private int _userId;

        public UserActor(int userId)
        {
            ColorConsole.WriteLineYellow("Creating a UserActor");
            _userId = userId;

            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                m => ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping the existing one"));
            Receive<StopMovieMessage>(m => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow($"UserActor{_userId} has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(m => StartPlayingMovie(m.MovieTitle));
            Receive<StopMovieMessage>(
                m => ColorConsole.WriteLineRed("Error: the movie cannot be stopped, nothing is playing"));

            ColorConsole.WriteLineYellow($"UserActor {_userId} has now become Stopped");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            ColorConsole.WriteLineYellow($"User {_userId} is currently watching '{title}'");

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User {_userId} has stopped watching  '{_currentlyWatching}'");

            _currentlyWatching = null;

            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow("UserActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow("UserActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
    }
}