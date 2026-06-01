using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBot
{
    public class ResponseEngine
    {
        /// <summary>
        /// Handles matching user input to predefined cybersecurity responses.
        /// Uses keyword detection for flexible, natural-language matching.
        /// </summary>
        
        
            // ─── Response Dictionary ───────────────────────────────────────────────
            // Key   = keyword(s) to detect (lowercase)
            // Value = bot response

            private readonly Dictionary<string[], string> _responses = new Dictionary<string[], string>
        {
            // Greetings / Small talk
            {
                new[] { "how are you", "how r you", "how are u" },
                "I'm running at full security capacity, thank you for asking! 😊\n" +
                "  Ready to help you stay safe online."
            },
            {
                new[] { "your purpose", "what do you do", "what can you do", "what are you" },
                "My purpose is to raise cybersecurity awareness! 🛡️\n" +
                "  I can answer questions about:\n" +
                "    • Password safety\n" +
                "    • Phishing attacks\n" +
                "    • Safe browsing habits\n" +
                "    • Two-factor authentication\n" +
                "    • Malware & ransomware\n" +
                "    • Data privacy\n" +
                "  Just ask me anything!"
            },
            {
                new[] { "what can i ask", "what topics", "help" },
                "Great question! You can ask me about:\n" +
                "  🔑  Password safety\n" +
                "  🎣  Phishing & scams\n" +
                "  🌐  Safe browsing\n" +
                "  🔐  Two-factor authentication (2FA)\n" +
                "  🦠  Malware & ransomware\n" +
                "  🔒  Data privacy & VPNs\n" +
                "  Type 'exit' or 'quit' to leave."
            },
 
            // ── Password Safety ────────────────────────────────────────────────
            {
                new[] { "password" },
                "🔑 PASSWORD SAFETY TIPS:\n" +
                "  • Use at least 12 characters — mix letters, numbers & symbols.\n" +
                "  • Never reuse the same password across sites.\n" +
                "  • Use a reputable password manager (e.g., Bitwarden, 1Password).\n" +
                "  • Change passwords immediately if a breach is suspected.\n" +
                "  • Avoid using personal info like birthdays or names.\n" +
                "  💡 Tip: Use a passphrase — e.g., 'Coffee$Jumps7Over!Moon'"
            },
 
            // ── Phishing ───────────────────────────────────────────────────────
            {
                new[] { "phishing", "phish", "scam", "suspicious email", "fake email" },
                "🎣 PHISHING AWARENESS:\n" +
                "  Phishing is when attackers impersonate trusted sources to steal your info.\n" +
                "  Watch out for:\n" +
                "  • Urgent or threatening language ('Act NOW or your account is closed!')\n" +
                "  • Mismatched sender email addresses\n" +
                "  • Suspicious links — hover before you click!\n" +
                "  • Requests for passwords or payment details via email\n" +
                "  💡 When in doubt, go directly to the official website instead of clicking links."
            },
 
            // ── Safe Browsing ──────────────────────────────────────────────────
            {
                new[] { "browsing", "browser", "safe browsing", "https", "website safety" },
                "🌐 SAFE BROWSING TIPS:\n" +
                "  • Always check for HTTPS (🔒) before entering any personal data.\n" +
                "  • Keep your browser and plugins up to date.\n" +
                "  • Use an ad-blocker to reduce malicious ads.\n" +
                "  • Avoid public Wi-Fi for sensitive tasks — use a VPN.\n" +
                "  • Be cautious of pop-ups asking you to install software."
            },
 
            // ── Two-Factor Authentication ──────────────────────────────────────
            {
                new[] { "two factor", "2fa", "multi factor", "mfa", "authenticator" },
                "🔐 TWO-FACTOR AUTHENTICATION (2FA):\n" +
                "  2FA adds a second layer of security beyond your password.\n" +
                "  Options include:\n" +
                "  • Authenticator apps (Google Authenticator, Authy) — most secure\n" +
                "  • SMS codes — convenient but less secure\n" +
                "  • Hardware keys (YubiKey) — strongest option\n" +
                "  💡 Enable 2FA on ALL important accounts: email, bank, social media."
            },
 
            // ── Malware / Ransomware ───────────────────────────────────────────
            {
                new[] { "malware", "ransomware", "virus", "trojan", "spyware" },
                "🦠 MALWARE & RANSOMWARE:\n" +
                "  Malware is malicious software designed to harm or exploit your device.\n" +
                "  Ransomware locks your files and demands payment.\n" +
                "  Protect yourself:\n" +
                "  • Install reputable antivirus software and keep it updated.\n" +
                "  • Never open email attachments from unknown senders.\n" +
                "  • Regularly back up your data (3-2-1 rule: 3 copies, 2 media, 1 offsite).\n" +
                "  • Keep your OS and software patched and up to date."
            },
 
            // ── VPN / Privacy ──────────────────────────────────────────────────
            {
                new[] { "vpn", "privacy", "data privacy", "personal data" },
                "🔒 DATA PRIVACY & VPNs:\n" +
                "  A VPN (Virtual Private Network) encrypts your internet traffic.\n" +
                "  Benefits:\n" +
                "  • Hides your IP address and location\n" +
                "  • Secures you on public Wi-Fi\n" +
                "  • Bypasses geo-restrictions safely\n" +
                "  For general privacy:\n" +
                "  • Minimise the personal info you share online.\n" +
                "  • Review app permissions regularly.\n" +
                "  • Use privacy-focused search engines (e.g., DuckDuckGo)."
            },
 
            // ── Social Engineering ─────────────────────────────────────────────
            {
                new[] { "social engineering", "pretexting", "vishing", "smishing" },
                "🎭 SOCIAL ENGINEERING:\n" +
                "  Attackers manipulate people psychologically to gain access.\n" +
                "  Types:\n" +
                "  • Vishing — voice phishing over the phone\n" +
                "  • Smishing — phishing via SMS\n" +
                "  • Pretexting — fabricating a scenario to extract info\n" +
                "  💡 Always verify the identity of anyone requesting sensitive information."
            },

            //---Identity Theft-----------------------------
                {
                    new[] { "identity theft", "identity fraud" },
                    "IDENTITY THEFT:\n" +
                    "Identity theft occurs when someone steals your personal info to commit fraud.\n" +
                    "Protect yourself by:\n" +
                    "Never share your ID number or banking details online \n" +
                    "Monitor your bank statements regularly.\n" +
                    "💡If you suspect identity theft, contact your bank immediately."

                },
        };

            // ─── Public Method ─────────────────────────────────────────────────────

            /// <summary>
            /// Matches input against known keywords and returns an appropriate response.
            /// Returns null if no match is found (so the caller can show a default message).
            /// </summary>
            public string GetResponse(string userInput)
            {
                if (string.IsNullOrWhiteSpace(userInput))
                    return null;

                string lower = userInput.ToLower().Trim();

                foreach (var entry in _responses)
                {
                    foreach (string keyword in entry.Key)
                    {
                        if (lower.Contains(keyword))
                            return entry.Value;
                    }
                }

                return null; // No match
            }
        }
    }

