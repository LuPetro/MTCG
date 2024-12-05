using Swen1.MTCG_Petrovic.Handlers;
using Swen1.MTCG_Petrovic.Server;
using System.Diagnostics;

namespace Swen1.MTCG_Petrovic
{
    internal class Program
    {
        public static bool DEBUG_MODE = Environment.GetEnvironmentVariable("DEBUG_MODE") == "true";

        static void Main(string[] args)
        {
            if (DEBUG_MODE)
            {
                Console.WriteLine("DEBUG MODE AKTIVIERT: Tokens sind vordefiniert.");
            }

            HttpSvr server = new();

            server.Incoming += (sender, e) =>
            {
                _ = Handler.HandleEvent(e); // Leitet eingehende Anfragen an die Handler weiter
            };

            Console.WriteLine("Server running on http://127.0.0.1:12000");
            server.Run(); // Server starten
        }
    }
}
