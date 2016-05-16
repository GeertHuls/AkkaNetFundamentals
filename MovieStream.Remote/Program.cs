using Akka.Actor;
using MovieStreaming.Common;

namespace MovieStream.Remote
{
    internal class Program
    {
        private static void Main()
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process");

            ActorSystem.Create("MovieStreamingActorSystem")
                .WhenTerminated
                .Wait();
        }
    }
}
