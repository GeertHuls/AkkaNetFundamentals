using System;
using Akka.Actor;

namespace MovieStreamingConsole.Actors
{
    public class UserActor : ReceiveActor
    {
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