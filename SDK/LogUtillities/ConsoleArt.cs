using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discreet.SDK.LogUtillities
{
    public class ConsoleArt
    {
        private static Random _random = new Random();
        private static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
        }
        public static List<string> RandomMessage = new List<string>
        {
            "No Bitches??",
            "No Maidens??",
            "Smoke With Me",
            "One Hit For Every Bitch You Dont Have",
            "Did You Know Cats Can Lick Their Eyes...? Dont Believe Me? Stick Your Finger In Your Ass!",
            "I LOVE CHEESE",
            "Try Minecraft <3",
            "Try Terraria <3",
            "Turn On The Power",
            "Im Ready ~Spongebob 2003 May 14 12:45pm",
            "Spartan Likes Femboys",
            "Try Finger, But Whole",
            "Whalecum!",
            "The Giant Horse Cock Weighs Over 11 Pounds",
            "Perfect",
            "Pulling Your Ip Address.....",
            "Have you seen Joe?",
            "JoeMomma",
            "I Jerk Off When I Pee",
            "Integers Are Decimals... ~Glowking",
            "No ~Bulxcy",
            "All Asian Do Is Eat Cat And Suck At Driving",
            "Havin A Gay Old Time, Hehehe Dick And Nuts ~Teddy",
            "Idk Im Retarded, Tired Asf, Actually I Got One, Why Is My Burrito Leaking ~Ani",
            "Dn, :dogTrollFace: Hahahah ~MimicVirus",
            "I'm Taking A Shit In My Poop Sock Wanna See? ~PC Principle"



        };
        public static void ConsoleStart(ConsoleColor color)
        {
            Console.ForegroundColor = GetRandomConsoleColor();
            Console.WriteLine(@"                            ██████╗ ██╗███████╗ ██████╗██████╗ ███████╗███████╗████████╗                            ");
            Console.WriteLine(@"                            ██╔══██╗██║██╔════╝██╔════╝██╔══██╗██╔════╝██╔════╝╚══██╔══╝                            ");
            Console.WriteLine(@"                            ██║  ██║██║███████╗██║     ██████╔╝█████╗  █████╗     ██║                               ");
            Console.WriteLine(@"                            ██║  ██║██║╚════██║██║     ██╔══██╗██╔══╝  ██╔══╝     ██║                               ");
            Console.WriteLine(@"                            ██████╔╝██║███████║╚██████╗██║  ██║███████╗███████╗   ██║                               ");
            Console.WriteLine(@"                            ╚═════╝ ╚═╝╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚══════╝   ╚═╝                               ");
            Console.WriteLine(@"------------------------------------Devloped And Owned By: Lime/Pyro/Creed#1212-------------------------------------");
            Console.WriteLine(@"                                                 _{Version 1.0}_                                                    ");
            Console.WriteLine("");
            Console.WriteLine($"   [WhaleCum!] || [Time Of Initiation:{DateTime.Now.ToString("hh:mm tt")}] || Random Message =//> [{RandomMessage[new Random().Next(0, RandomMessage.Count)]}] ");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");

        }
        public static void ConsoleClear()
        {
            Console.Clear();
            ConsoleStart(GetRandomConsoleColor());
            
        }
    }
}
