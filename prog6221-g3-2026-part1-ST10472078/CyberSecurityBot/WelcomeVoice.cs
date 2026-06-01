using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using System.IO;

namespace CyberSecurityBot
{
    public class WelcomeVoice
    {
        private const string AudioFileName = "greeting.wav";

        /// <summary>
        /// Plays the WAV greeting file if it exists.
        
        public static void Play()
        {
            string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AudioFileName);

            if (File.Exists(audioPath))
            {
                try
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        // PlaySync blocks until audio finishes — keeps startup flow clean
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    Display.PrintColour($"[Audio] Could not play greeting: {ex.Message}", ConsoleColor.DarkYellow);
                }
            }
            else
            {
                // Friendly notice — audio is optional for the app to run
                Display.PrintColour(
                    $"[Audio] '{AudioFileName}' not found in output directory. " +
                    "Record a WAV greeting and place it next to the executable.",
                    ConsoleColor.DarkYellow);
            }
        }
    }
}

