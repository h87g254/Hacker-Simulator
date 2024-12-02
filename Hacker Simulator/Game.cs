using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacker_Simulator
{
    public class Game
    {
        private string playerIP;
        private string enemyIP;
        private bool isGameOver = false;
        private List<string> discoveredIPs = new List<string>();
        private int securityLevel = 100;

        public void Start()
        {
            InitializeGame();

            while (!isGameOver)
            {
                DisplayPrompt();
                string command = Console.ReadLine()?.ToLower() ?? "";
                ProcessCommand(command);
            }
        }

        private void InitializeGame()
        {
            Console.WriteLine("=== Hacker Simulator ===");
            Console.WriteLine("Welcome, Agent!");

            // Generate random IPs
            playerIP = GenerateRandomIP();
            enemyIP = GenerateRandomIP();

            Console.WriteLine($"Your IP: {playerIP}");
            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine("- scan : Scan for nearby IPs");
            Console.WriteLine("- analyze <ip> : Analyze specific IP for vulnerabilities");
            Console.WriteLine("- exploit <ip> : Attempt to exploit a system");
            Console.WriteLine("- help : Show available commands");
            Console.WriteLine("- exit : Exit game");
        }

        private string GenerateRandomIP()
        {
            Random rand = new Random();
            return $"{rand.Next(1, 255)}.{rand.Next(0, 255)}.{rand.Next(0, 255)}.{rand.Next(1, 255)}";
        }

        private void DisplayPrompt()
        {
            Console.Write("\n> ");
        }

        private void ProcessCommand(string command)
        {
            string[] parts = command.Split(' ');
            string baseCommand = parts[0];

            switch (baseCommand)
            {
                case "scan":
                    ScanNetwork();
                    break;

                case "analyze":
                    if (parts.Length > 1)
                        AnalyzeIP(parts[1]);
                    else
                        Console.WriteLine("Usage: analyze <ip>");
                    break;

                case "exploit":
                    if (parts.Length > 1)
                        ExploitSystem(parts[1]);
                    else
                        Console.WriteLine("Usage: exploit <ip>");
                    break;

                case "help":
                    ShowHelp();
                    break;

                case "exit":
                    isGameOver = true;
                    Console.WriteLine("Exiting game...");
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'help' for available commands.");
                    break;
            }
        }

        private void ScanNetwork()
        {
            Console.WriteLine("Scanning network...");
            Thread.Sleep(1500); // Simulate scanning delay

            if (!discoveredIPs.Contains(enemyIP))
            {
                Random rand = new Random();
                if (rand.Next(100) < 40) // 40% chance to discover enemy IP
                {
                    discoveredIPs.Add(enemyIP);
                    Console.WriteLine($"Discovered IP: {enemyIP}");
                }
                else
                {
                    string decoyIP = GenerateRandomIP();
                    discoveredIPs.Add(decoyIP);
                    Console.WriteLine($"Discovered IP: {decoyIP}");
                }
            }
        }

        private void AnalyzeIP(string ip)
        {
            Console.WriteLine($"Analyzing {ip}...");
            Thread.Sleep(1000);

            if (ip == enemyIP)
            {
                Console.WriteLine("System Analysis:");
                Console.WriteLine($"Security Level: {securityLevel}%");
                Console.WriteLine("Vulnerabilities detected: 2");
                Console.WriteLine("- Outdated firewall");
                Console.WriteLine("- Weak encryption");
            }
            else
            {
                Console.WriteLine("No significant vulnerabilities found.");
            }
        }

        private void ExploitSystem(string ip)
        {
            if (ip == enemyIP)
            {
                Console.WriteLine("Attempting exploitation...");
                Thread.Sleep(2000);

                Random rand = new Random();
                int attackStrength = rand.Next(30, 100);

                if (attackStrength > securityLevel)
                {
                    Console.WriteLine("Exploitation successful!");
                    Console.WriteLine("System compromised!");
                    Console.WriteLine("You win!");
                    isGameOver = true;
                }
                else
                {
                    securityLevel -= (attackStrength / 2);
                    Console.WriteLine("Exploitation failed!");
                    Console.WriteLine($"Target security level reduced to {securityLevel}%");
                }
            }
            else
            {
                Console.WriteLine("Exploitation failed: Invalid target");
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("- scan : Scan for nearby IPs");
            Console.WriteLine("- analyze <ip> : Analyze specific IP for vulnerabilities");
            Console.WriteLine("- exploit <ip> : Attempt to exploit a system");
            Console.WriteLine("- help : Show this help message");
            Console.WriteLine("- exit : Exit game");
        }
    }
}
