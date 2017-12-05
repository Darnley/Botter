using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botter
{
    class Program
    {
        /// <summary>
        /// Logging instance.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Console.WriteLine("Botter, a Xat Bot");
            Console.WriteLine("-----------------");
            Console.WriteLine("Getting chat information...");

            // Clear "Getting chat information..." line
            Console.SetCursorPosition(2, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            try
            {
                Bot bot = new Bot("Megafire");

                Console.WriteLine(String.Format("Chat Name: {0}", bot.Chat.Name));
                Console.WriteLine(String.Format("Chat Id:   {0}", bot.Chat.Id));
                Console.WriteLine(String.Format("Chat Type: {0}", bot.Chat.Type));
                Console.WriteLine(String.Format("Chat Host: {0}", bot.Chat.Server.Host));
                Console.WriteLine(String.Format("Chat Port: {0}", bot.Chat.Server.Port));
                Console.WriteLine("-----------------");

                bot.Chat.Server.Connect();
            } catch(Exception ex)
            {
                Log.Fatal(ex.Message, ex);
            }

        }

        /// <summary>
        /// Clear current line.
        /// </summary>
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
