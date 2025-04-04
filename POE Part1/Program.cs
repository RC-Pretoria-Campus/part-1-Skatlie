using System;
using System.Speech.Synthesis;
using System.Threading;
using Figgle; 

class Program
{
    static void Main()
    {
        Console.Title = "CyberGuard - Cybersecurity Awareness Bot";
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        // Initialize Speech Synthesizer
        SpeechSynthesizer synth = new SpeechSynthesizer();
        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);

        // Display ASCII Art Logo using Figgle
        string cyberGuardLogo = FiggleFonts.Standard.Render("CyberGuard");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(cyberGuardLogo);
        Console.ResetColor();

        synth.Speak("Welcome to CyberGuard, your cybersecurity awareness bot.");

        // Typing effect function
        void TypeText(string text, int delay = 30, bool speak = true)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
            if (speak)
            {
                synth.Speak(text);
            }
        }

        // Ask for user’s name
        string userName = "";
        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeText("\n→ Please enter your name: ", speak: false);
            Console.ResetColor();
            userName = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeText("⚠ Oops! You need to enter a valid name.");
                Console.ResetColor();
            }
        }

        // Personalized Welcome Message
        Console.Clear();
        string welcomeMessage = $"Welcome, {userName}! I am here to help you stay safe online!";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║   {welcomeMessage} ║ ");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
        Console.ResetColor();

        synth.Speak(welcomeMessage);
        Thread.Sleep(1000);

        // Chatbot conversation loop
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string questionPrompt = "\n═══════════════════════════════════════════════════════════════\n" +
                                    " Ask me a question about cybersecurity, or type 'exit' to quit:\n" +
                                    "════════════════════════════════════════════════════════════════";
            TypeText(questionPrompt);
            Console.ResetColor();

            Console.Write("> ");
            string userQuestion = Console.ReadLine().Trim().ToLower();

            // Handle empty input
            if (string.IsNullOrWhiteSpace(userQuestion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeText(" Please enter a valid question.");
                Console.ResetColor();
                continue;
            }

            if (userQuestion == "exit")
            {
                string exitMessage = "Goodbye! Stay safe online.";
                synth.Speak(exitMessage);
                Console.ForegroundColor = ConsoleColor.Magenta;
                TypeText(exitMessage);
                Console.ResetColor();
                break;
            }

            string response = GetCybersecurityResponse(userQuestion);
            Console.ForegroundColor = ConsoleColor.Yellow;
            TypeText("🤖 " + response);
            synth.Speak(response);
            Console.ResetColor();
        }
    }

    static string GetCybersecurityResponse(string question)
    {
        switch (question)
        {
            case "how are you":
                return "I'm just a bot, but I'm always here to help you with cybersecurity!";

            case "what's your purpose":
                return "I'm here to educate you on cybersecurity and help you stay safe online.";

            case "what can i ask you about":
                return "You can ask me about password safety, phishing, and safe browsing.";

            case "tell me about password safety":
                return "Always use strong, unique passwords. A mix of letters, numbers, and symbols is best.";

            case "what is phishing":
                return "Phishing is a cyber attack where attackers trick you into giving personal information using fake emails or websites.";

            case "how can i browse safely":
                return "Use updated browsers, avoid clicking on suspicious links, and enable two-factor authentication where possible.";

            case "how do i create a strong password":
                return "A strong password should be at least 12 characters long, include upper and lower case letters, numbers, and special symbols.";

            case "what are some safe browsing tips":
                return "Avoid clicking on unknown links, keep your software updated, use HTTPS websites, and don't share personal information on public Wi-Fi.";

            default:
                return "I didn't quite understand that. Could you rephrase your question?";
        }
    }
}
