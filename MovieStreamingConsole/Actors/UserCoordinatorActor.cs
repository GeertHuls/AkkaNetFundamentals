using System;
using System.Collections.Generic;
using Akka.Actor;

namespace MovieStreamingConsole.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();
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