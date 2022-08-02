using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discreet.SDK.Functions;
using VRC;
using Discreet.SDK.Wrappers;
using Discreet.QOL;

namespace Discreet.SDK.LogUtillities
{/// <summary>
/// Sent out Made by Lime/Pyro/Creed#9739
/// </summary>
    class LogHandler
    {
        public static void Log(ConsoleColor color, string message, bool ALERT = false, bool Debugpass = false, bool player = false, bool Debugfail = false,  bool timeenabled = true)
        {
            if (timeenabled)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(DateTime.Now.ToString("HH:mm:ss.fff"));
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("] ");
            }
            
            if (ALERT)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ALERT => ");
            }
            if (Debugpass)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("PASSED => ");
            }
            if (Debugfail)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("FAILED => ");
            }
            if (player)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Player => ");
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("{");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Discreet");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(" -//-> ");
            Console.ForegroundColor = color;
            Console.Write(message + "\n"); 
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
