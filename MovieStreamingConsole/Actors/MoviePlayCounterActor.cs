using System.Collections.Generic;
using Akka.Actor;
using MovieStreamingConsole.Messages;

namespace MovieStreamingConsole.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            ColorConsole.WriteMagenta(
                "MoviePlayCounterActor '{0}' has been watched {1} times",
                message.MovieTitle, _moviePlayCounts[message.MovieTitle]);
        }
    }
}