using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
                {
                    var childActorRef = FindOrCreateChildUser(message.UserId);

                    childActorRef.Tell(message);
                });

            Receive<StopMovieMessage>(message =>
            {
                var childActorRef = FindOrCreateChildUser(message.UserId);

                childActorRef.Tell(message);
            });

        }

        private IActorRef FindOrCreateChildUser(int userId)
        {
            if (_users.ContainsKey(userId))
            {
                return _users[userId];
            }

            var newChildActorRef = Context.ActorOf(
                Props.Create(() => new UserActor(userId)), $"User{userId}");

            _users.Add(userId, newChildActorRef);

            ColorConsole.WriteLineCyan(
                $"UserCoordinator created new child UserActor for {userId} (Total Users: {_users.Count})");

            return newChildActorRef;
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PreRestart because: {0}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan("UserCoordinatorActor PostRestart because: {0}", reason);

            base.PostRestart(reason);
        }
    }
}