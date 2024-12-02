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
        private Dictionary<string, NetworkNode> networkMap = new Dictionary<string, NetworkNode>();

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
                    if (parts.Length > 1)
                    {
                        ScanNetwork(parts[1]); // deep, stealth, or basic
                    }
                    else
                    {
                        ScanNetwork(); // defaults to basic
                    }
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

        private void ScanNetwork(string scanType = "basic")
        {
            Console.WriteLine($"Initiating {scanType} network scan...");
            Thread.Sleep(1000);

            switch (scanType.ToLower())
            {
                case "deep":
                    PerformDeepScan();
                    break;
                case "stealth":
                    PerformStealthScan();
                    break;
                default:
                    PerformBasicScan();
                    break;
            }

            DisplayNetworkMap();
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
            Console.WriteLine("- scan [type] : Scan for nearby IPs (types: basic, deep, stealth)");
            Console.WriteLine("  * basic  - Standard scan with balanced detection chance");
            Console.WriteLine("  * deep   - Thorough scan with higher detection chance");
            Console.WriteLine("  * stealth - Careful scan with lower detection chance");
            Console.WriteLine("- analyze <ip> : Analyze specific IP for vulnerabilities");
            Console.WriteLine("- exploit <ip> : Attempt to exploit a system");
            Console.WriteLine("- help : Show this help message");
            Console.WriteLine("- exit : Exit game");
        }

        private void PerformBasicScan()
        {
            Random rand = new Random();
            int discoveryChance = 40;

            if (!networkMap.ContainsKey(enemyIP) && rand.Next(100) < discoveryChance)
            {
                AddNodeToNetwork(enemyIP, rand.Next(1, 4));
                Console.WriteLine($"Discovered IP: {enemyIP} (Distance: {networkMap[enemyIP].Distance} hops)");
            }
            else
            {
                string decoyIP = GenerateRandomIP();
                AddNodeToNetwork(decoyIP, rand.Next(1, 3));
                Console.WriteLine($"Discovered IP: {decoyIP} (Distance: {networkMap[decoyIP].Distance} hops)");
            }
        }

        private void PerformDeepScan()
        {
            Console.WriteLine("Performing deep scan (higher detection chance but slower)...");
            Thread.Sleep(2000);

            Random rand = new Random();
            int discoveryChance = 60;

            if (!networkMap.ContainsKey(enemyIP) && rand.Next(100) < discoveryChance)
            {
                AddNodeToNetwork(enemyIP, rand.Next(1, 3));
                // Add some connected nodes
                for (int i = 0; i < rand.Next(1, 4); i++)
                {
                    string connectedIP = GenerateRandomIP();
                    AddNodeToNetwork(connectedIP, networkMap[enemyIP].Distance + 1);
                    networkMap[enemyIP].ConnectedIPs.Add(connectedIP);
                }
            }

            Console.WriteLine("Deep scan complete. Network topology updated.");
        }

        private void PerformStealthScan()
        {
            Console.WriteLine("Performing stealth scan (lower detection risk)...");
            Thread.Sleep(1500);

            Random rand = new Random();
            int discoveryChance = 30;

            if (!networkMap.ContainsKey(enemyIP) && rand.Next(100) < discoveryChance)
            {
                AddNodeToNetwork(enemyIP, rand.Next(2, 5));
                Console.WriteLine("Hidden node detected...");
            }
        }

        private void AddNodeToNetwork(string ip, int distance)
        {
            if (!networkMap.ContainsKey(ip))
            {
                networkMap[ip] = new NetworkNode(ip)
                {
                    Distance = distance
                };
            }
        }

        private void DisplayNetworkMap()
        {
            Console.WriteLine("\nNetwork Map:");
            foreach (var node in networkMap.Values.OrderBy(n => n.Distance))
            {
                Console.WriteLine($"└─[{node.IP}] - Distance: {node.Distance} hops");
                foreach (var connectedIP in node.ConnectedIPs)
                {
                    Console.WriteLine($"  └─── Connected to: {connectedIP}");
                }
            }
        }
    }
}
