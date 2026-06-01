namespace CyberSecurityBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set console to support colours
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Play voice greeting (WAV file)
            WelcomeVoice.Play();

            // Display ASCII logo
            Display.ShowLogo();

            // Start the chatbot
            Chatbot bot = new Chatbot();
            bot.Start();
        }
    }
}
//REFRENCES
/*
 * National Cybersecurity Alliance. (2024). Stay safe online. https://staysafeonline.org
 * Microsoft. (2024). ConsoleColor enum. Microsoft Learn. https://learn.microsoft.com/en-us/dotnet/api/system.consolecolor
 * Troelsen, A. and Japikse, P. (2021) Pro C# 9 with .NET 5: Foundational Principles and Practices in Programming. 10th edn. Apress.
 * C# IF else statement - https://youtu.be/pSPQnXleaS8?si=zTq6js9Rt1jgh8_o
 * C# Arrays - https://youtu.be/IHMmPVEOT64?si=OD56IXiiTnbTvRab
 * Loops - https://youtu.be/h4hY2hho73Q?si=ccakCGqlYYaSGBTu
 * Lindemulder, G., Kosinski, M. and Jonker, A. (2024) What is cybersecurity? IBM. Available at: https://www.ibm.com/think/topics/cybersecurity (Accessed: 29 March 2026)
 */

