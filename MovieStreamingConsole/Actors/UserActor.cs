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

            Console.WriteLine("Setting initial behaviour to stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                m => Console.WriteLine("Error: cannot start playing another movie before stopping the existing one"));
            Receive<StopMovieMessage>(m => StopPlayingCurrentMovie());

            Console.WriteLine("UserActor has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(m => StartPlayingMovie(m.MovieTitle));
            Receive<StopMovieMessage>(
                m => Console.WriteLine("Error: the movie cannot be stopped, nothing is playing"));

            Console.WriteLine("UserActor has now become Stopped");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            Console.WriteLine("User is currently watching '{0}'", title);

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            Console.WriteLine("User has stopped watching  '{0}'", _currentlyWatching);

            _currentlyWatching = null;

            Become(Stopped);
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