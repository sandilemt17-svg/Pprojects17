using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBot
{
    internal class Display
    {
        public static void PrintColour(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        //Prints text inline (no newline) in the specified colour.
        public static void PrintColourInline(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.Write(text);
            Console.ResetColor();
        }

        // ─── Typing Effect ─────────────────────────────────────────────────────

        
        /// Simulates a typing effect — prints characters one at a time
        
        public static void TypeWrite(string text, ConsoleColor colour = ConsoleColor.White, int delayMs = 18)
        {
            Console.ForegroundColor = colour;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        // ─── Borders & Dividers ────────────────────────────────────────────────

        public static void PrintDivider(char symbol = '─', int width = 60, ConsoleColor colour = ConsoleColor.DarkCyan)
        {
            PrintColour(new string(symbol, width), colour);
        }

        public static void PrintSectionHeader(string title, ConsoleColor colour = ConsoleColor.Cyan)
        {
            Console.WriteLine();
            PrintDivider('═', 60, colour);
            PrintColour($"  {title.ToUpper()}", colour);
            PrintDivider('═', 60, colour);
            Console.WriteLine();
        }

        // ─── ASCII Logo ────────────────────────────────────────────────────────

        
        // Displays the Cybersecurity Awareness Bot ASCII logo as a title screen.
        
        public static void ShowLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ███████╗███████╗ ██████╗
 ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝██╔════╝██╔════╝
 ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████╗█████╗  ██║
 ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗╚════██║██╔══╝  ██║
 ╚██████╗   ██║   ██████╔╝███████╗██║  ██║███████║███████╗╚██████╗
  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝ ╚═════╝
");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(@"
         ░█████╗░░██╗░░░░░░░██╗░█████╗░██████╗░███████╗███╗░░██╗███████╗░██████╗░██████╗
         ██╔══██╗░██║░░██╗░░██║██╔══██╗██╔══██╗██╔════╝████╗░██║██╔════╝██╔════╝██╔════╝
         ███████║░╚██╗████╗██╔╝███████║██████╔╝█████╗░░██╔██╗██║█████╗░░╚█████╗░╚█████╗░
         ██╔══██║░░████╔═████║░██╔══██║██╔══██╗██╔══╝░░██║╚████║██╔══╝░░░╚═══██╗░╚═══██╗
         ██║░░██║░░╚██╔╝░╚██╔╝░██║░░██║██║░░██║███████╗██║░╚███║███████╗██████╔╝██████╔╝
         ╚═╝░░╚═╝░░░╚═╝░░░╚═╝░░╚═╝░░╚═╝╚═╝░░╚═╝╚══════╝╚═╝░░╚══╝╚══════╝╚═════╝░╚═════╝
");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                    ╔══════════════════════════════════╗");
            Console.WriteLine("                    ║   🔐  AWARENESS BOT  v1.0  🔐    ║");
            Console.WriteLine("                    ╚══════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            PrintDivider('─', 70, ConsoleColor.DarkCyan);
            PrintColour("  Keeping you safe in the digital world, one tip at a time.", ConsoleColor.DarkCyan);
            PrintDivider('─', 70, ConsoleColor.DarkCyan);
            Console.WriteLine();

            Thread.Sleep(1200); // Brief pause so the logo can be read
        }

        // ─── Bot / User Prompt Labels ──────────────────────────────────────────

        public static void PrintBotLabel()
        {
            PrintColourInline("  🤖 CyberBot » ", ConsoleColor.Green);
        }

        public static void PrintUserLabel(string name)
        {
            PrintColourInline($"  👤 {name} » ", ConsoleColor.Yellow);
        }
    }
}

