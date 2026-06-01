using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBot
{
    public class Chatbot
    {
        /// <summary>
        /// Main chatbot class. Orchestrates user interaction, input validation,
        /// response generation, and goodbye handling.
        /// </summary>
        
        
            private readonly ResponseEngine _engine;
            private User _user;

            private readonly string[] _exitCommands = { "exit", "quit", "bye", "goodbye", "q" };
            private readonly string _defaultResponse =
                "🤔 I didn't quite understand that. Could you rephrase?\n" +
                "  Type 'help' to see what topics I can assist with.";

            public Chatbot()
            {
                _engine = new ResponseEngine();
            }

            // ─── Entry Point ───────────────────────────────────────────────────────

            /// <summary>Starts the full chatbot session.</summary>
            public void Start()
            {
                GreetUser();
                RunConversationLoop();
                SayGoodbye();
            }

            // ─── Greeting ──────────────────────────────────────────────────────────

            private void GreetUser()
            {
                Display.PrintSectionHeader("Welcome", ConsoleColor.Cyan);

                Display.TypeWrite(
                    "Hello! I'm CyberBot, your Cybersecurity Awareness assistant.",
                    ConsoleColor.Green);

                Display.TypeWrite(
                    "I'm here to help you stay safe in the digital world. 🌐",
                    ConsoleColor.Green);

                Console.WriteLine();

                // Ask for the user's name — with validation
                string name = PromptForName();
                _user = new User(name);

                Console.WriteLine();
                Display.TypeWrite(
                    $"Great to meet you, {_user.Name}! 😊 Let's get started.",
                    ConsoleColor.Cyan);

                Display.TypeWrite(
                    "Type 'help' to see available topics, or just ask me anything!",
                    ConsoleColor.DarkCyan);

                Display.PrintDivider('─', 60, ConsoleColor.DarkGray);
                Console.WriteLine();

            AskOpeningQuestions();
            }

            // ─── Name Input with Validation ────────────────────────────────────────

            private string PromptForName()
            {
                string name = null;

                while (string.IsNullOrWhiteSpace(name))
                {
                    Display.PrintBotLabel();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("What's your name? ");
                    Console.ResetColor();

                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Display.PrintColour(
                            "  ⚠️  Name cannot be empty — please enter your name.",
                            ConsoleColor.DarkYellow);
                    }
                }

                // Capitalise first letter using string manipulation
                return char.ToUpper(name.Trim()[0]) + name.Trim().Substring(1);
            }


        private void AskOpeningQuestions()
        {
            //Question 1
            Display.PrintSectionHeader("Quick Cyber Check-In", ConsoleColor.Cyan);

            Display.PrintBotLabel();
            Display.TypeWrite("Have you changed your passwords in the last 3 months? (yes/no)", ConsoleColor.Green);
            Display.PrintUserLabel(_user.Name);
            string answer1 = Console.ReadLine();

            if (answer1?.ToLower() == "no" || answer1?.ToLower() == "n")
                Display.TypeWrite("  ⚠️  You should update your passwords regularly for better security!", ConsoleColor.DarkYellow);
            else
                Display.TypeWrite("  ✅ Great habit! Keep it up.", ConsoleColor.Green);

            Console.WriteLine();

            //Question 2
            Display.PrintBotLabel();
            Display.TypeWrite("Do you use two-factor authentication (2FA) on your accounts? (yes/no)", ConsoleColor.Green);
            Display.PrintUserLabel(_user.Name);
            string answer2 = Console.ReadLine();

            if (answer2?.ToLower() == "no" || answer2?.ToLower() == "n")
                Display.TypeWrite("  ⚠️  Enable 2FA immediately — it blocks 99% of account hacks!", ConsoleColor.DarkYellow);
            else
                Display.TypeWrite("  ✅ Excellent! 2FA is one of the best defences.", ConsoleColor.Green);

            Console.WriteLine();

            //Question 3
            Display.PrintBotLabel();
            Display.TypeWrite("Do you know how to identify a phishing email? (yes/no)", ConsoleColor.Green);
            Display.PrintUserLabel(_user.Name);
            string answer3 = Console.ReadLine();

            if (answer3?.ToLower() == "no" || answer3?.ToLower() == "n")
                Display.TypeWrite("  💡 Ask me about 'phishing' and I'll teach you what to look out for!", ConsoleColor.Cyan);
            else
                Display.TypeWrite("  ✅ Great! Stay vigilant — phishing attacks are getting smarter.", ConsoleColor.Green);

            Console.WriteLine();

            //Question 4
            Display.PrintBotLabel();
            Display.TypeWrite("Is your antivirus software up to date? (yes/no)", ConsoleColor.Green);
            Display.PrintUserLabel(_user.Name);
            string answer4 = Console.ReadLine();

            if (answer4?.ToLower() == "no" || answer4?.ToLower() == "n")
                Display.TypeWrite("  ⚠️  Update your antivirus now — outdated software leaves you vulnerable!", ConsoleColor.DarkYellow);
            else
                Display.TypeWrite("  ✅ Perfect! An updated antivirus is your first line of defence.", ConsoleColor.Green);

            Console.WriteLine();

            //Question 5
            Display.PrintBotLabel();
            Display.TypeWrite("Do you use the same password for multiple accounts? (yes/no)", ConsoleColor.Green);
            Display.PrintUserLabel(_user.Name);
            string answer5 = Console.ReadLine();

            if (answer5?.ToLower() == "yes" || answer5?.ToLower() == "y")
                Display.TypeWrite(" This is risky! Ask me about 'password manager' to fix this.", ConsoleColor.DarkYellow);
            else
                Display.TypeWrite("  ✅ Smart! Unique passwords for every account is the way to go.", ConsoleColor.Green);

            Console.WriteLine();

            Display.PrintDivider('-', 60, ConsoleColor.Blue);
            Display.TypeWrite($" Appreciate it for the check-in, {_user.Name}! Now let's get started shall we.", ConsoleColor.Cyan);
            Display.PrintDivider('─', 60, ConsoleColor.DarkGray);
            Console.WriteLine();


        }

            // ─── Main Conversation Loop ────────────────────────────────────────────

            private void RunConversationLoop()
            {
                while (true)
                {
                    // User prompt
                    Display.PrintUserLabel(_user.Name);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string input = Console.ReadLine();
                    Console.ResetColor();
                    Console.WriteLine();

                    // ── Input Validation ───────────────────────────────────────────

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Display.PrintBotLabel();
                        Display.TypeWrite(
                            "⚠️  It looks like you didn't type anything. Please ask me a question!",
                            ConsoleColor.DarkYellow);
                        Console.WriteLine();
                        continue;
                    }

                    // ── Exit Check ─────────────────────────────────────────────────

                    if (IsExitCommand(input))
                        break;

                    _user.IncrementMessageCount();

                    // ── Get & Display Response ─────────────────────────────────────

                    string response = _engine.GetResponse(input) ?? _defaultResponse;

                    Display.PrintBotLabel();
                    Display.TypeWrite(response, ConsoleColor.White);
                    Console.WriteLine();
                    Display.PrintDivider('·', 60, ConsoleColor.DarkGray);
                    Console.WriteLine();
                }
            }

            // ─── Goodbye ───────────────────────────────────────────────────────────

            private void SayGoodbye()
            {
                Console.WriteLine();
                Display.PrintSectionHeader("Goodbye", ConsoleColor.Cyan);

                Display.TypeWrite(
                    $"Thanks for chatting, {_user.Name}! You asked {_user.MessageCount} question(s).",
                    ConsoleColor.Green);

                Display.TypeWrite(
                    "Stay cyber-safe out there! 🔐 Remember:",
                    ConsoleColor.Green);

                Display.PrintColour("  • Use strong, unique passwords.", ConsoleColor.Cyan);
                Display.PrintColour("  • Enable 2FA everywhere you can.", ConsoleColor.Cyan);
                Display.PrintColour("  • Think before you click!", ConsoleColor.Cyan);

                Console.WriteLine();
                Display.PrintDivider('═', 60, ConsoleColor.DarkCyan);
                Display.PrintColour("  CyberBot signing off. Goodbye! 👋", ConsoleColor.DarkCyan);
                Display.PrintDivider('═', 60, ConsoleColor.DarkCyan);
                Console.WriteLine();
            }

            // ─── Helpers ───────────────────────────────────────────────────────────

            private bool IsExitCommand(string input)
            {
                string lower = input.Trim().ToLower();
                foreach (string cmd in _exitCommands)
                {
                    if (lower == cmd)
                        return true;
                }
                return false;
            }
        }
    }


