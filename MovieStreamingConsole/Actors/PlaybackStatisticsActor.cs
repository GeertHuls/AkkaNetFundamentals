﻿using System;
using Akka.Actor;

namespace MovieStreamingConsole.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PreRestart because: {0}", reason.Message);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteWhite("PlaybackStatisticsActor PostRestart because: {0} ", reason.Message);

            base.PostRestart(reason);
        }
    }
}