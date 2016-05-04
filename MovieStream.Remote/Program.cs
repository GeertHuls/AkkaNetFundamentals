using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreamingConsole;

namespace MovieStream.Remote
{
    internal class Program
    {
        private static ActorSystem _movieStreamActorSystem;

        private static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in remote process");

            Start().Wait();

            Console.ReadLine();
        }

        private static async Task Start()
        {
            CreateActorSystem();
            await TerminateActorSystem();
        }

        private static void CreateActorSystem()
        {
            _movieStreamActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
        }

        private static async Task TerminateActorSystem()
        {
            await _movieStreamActorSystem.Terminate();
        }
    }
}
