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
        private List<string> playerIPs = new List<string>();
        private List<string> aiIPs = new List<string>();

        public void Start()
        {
            InitializeGame();

            while (!isGameOver)
            {
                DisplayPrompt();
                string command = Console.ReadLine()?.ToLower() ?? "";
                ProcessCommand(command);

                // Introduce random events
                IntroduceRandomEvent();

                // AI Opponents take their turn
                AITakeTurn();
            }
        }
        private void SetConsoleUI()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("===        Hacker Simulator          ===");
            Console.WriteLine("========================================");
            Console.ResetColor();
        }

        private void InitializeGame()
        {
            SetConsoleUI();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome, Agent!");

            // Generate random IPs for player and AI opponents
            playerIP = GenerateRandomIP();
            playerIPs.Add(playerIP);
            enemyIP = GenerateRandomIP();
            aiIPs.Add(enemyIP);

            // Add player and enemy nodes to the network map
            AddNodeToNetwork(playerIP, 0);
            AddNodeToNetwork(enemyIP, 0);

            Console.WriteLine($"Your IP: {playerIP}");
            Console.WriteLine("\nAvailable Commands:");
            ShowHelp();

            // Initialize AI opponents
            InitializeAIOpponents();
            Console.ResetColor();
        }

        private void InitializeAIOpponents()
        {
            // Add more AI opponents if needed
            for (int i = 0; i < 2; i++)
            {
                string aiIP = GenerateRandomIP();
                aiIPs.Add(aiIP);
            }
        }

        private string GenerateRandomIP()
        {
            Random rand = new Random();
            return $"192.168.{rand.Next(0, 255)}.{rand.Next(1, 255)}";
        }

        private void DisplayPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n> ");
            Console.ResetColor();
        }


        private void ProcessCommand(string command)
        {
            string[] parts = command.Split(' ');
            string baseCommand = parts[0];

            switch (baseCommand)
            {
                case "nmap":
                    if (parts.Length > 1)
                    {
                        ScanNetwork(parts[1]); // deep, stealth, or basic
                    }
                    else
                    {
                        Console.WriteLine("Usage: nmap [basic|deep|stealth]");
                    }
                    break;

                case "ping":
                    if (parts.Length > 1)
                        ProbeIP(parts[1]);
                    else
                        Console.WriteLine("Usage: ping <ip_address>");
                    break;

                case "nc":
                    if (parts.Length > 1)
                        ExploitSystem(parts[1]);
                    else
                        Console.WriteLine("Usage: nc <ip_address>");
                    break;

                case "hping3":
                    if (parts.Length > 1)
                        DDoSAttack(parts[1]);
                    else
                        Console.WriteLine("Usage: hping3 <ip_address>");
                    break;

                case "social-engineer":
                    if (parts.Length > 1)
                        PhishingAttack(parts[1]);
                    else
                        Console.WriteLine("Usage: social-engineer <ip_address>");
                    break;

                case "msfconsole":
                    if (parts.Length > 1)
                        MalwareAttack(parts[1]);
                    else
                        Console.WriteLine("Usage: msfconsole <ip_address>");
                    break;

                case "traceroute":
                    if (parts.Length > 1)
                        TraceRoute(parts[1]);
                    else
                        Console.WriteLine("Usage: traceroute <ip_address>");
                    break;

                case "iptables":
                    if (parts.Length > 1)
                        DisconnectIP(parts[1]);
                    else
                        Console.WriteLine("Usage: iptables <ip_address>");
                    break;

                case "help":
                    ShowHelp();
                    break;

                case "exit":
                    isGameOver = true;
                    Console.WriteLine("Exiting game...");
                    break;

                case "openssl":
                    if (parts.Length > 2)
                    {
                        if (parts[1] == "enc")
                            EncryptIP(parts[2]);
                        else if (parts[1] == "dec")
                            DecryptIP(parts[2]);
                        else
                            Console.WriteLine("Usage: openssl [enc|dec] <ip_address>");
                    }
                    else
                    {
                        Console.WriteLine("Usage: openssl [enc|dec] <ip_address>");
                    }
                    break;

                case "ufw":
                    if (parts.Length > 1)
                        UpgradeSecurity(parts[1]);
                    else
                        Console.WriteLine("Usage: ufw <ip_address>");
                    break;

                case "tcpdump":
                    MonitorTraffic();
                    break;

                case "rsync":
                    BackupData();
                    break;
                case "install-firewall":
                    if (parts.Length > 1)
                        InstallFirewall(parts[1]);
                    else
                        Console.WriteLine("Usage: install-firewall <ip_address>");
                    break;

                case "patch-system":
                    if (parts.Length > 1)
                        PatchSystem(parts[1]);
                    else
                        Console.WriteLine("Usage: patch-system <ip_address>");
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'help' for available commands.");
                    break;
            }
        }

        private void ScanNetwork(string scanType = "basic", bool isAI = false)
        {
            if (!isAI)
            {
                Console.WriteLine($"Initiating {scanType} network scan...");
            }
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

            if (!isAI)
            {
                DisplayNetworkMap();
            }
        }

        private void ProbeIP(string ip, bool isAI = false)
        {
            if (!isAI)
            {
                Console.WriteLine($"Probing {ip}...");
            }
            Thread.Sleep(1000);

            if (ip == enemyIP)
            {
                if (!isAI)
                {
                    Console.WriteLine("System Analysis:");
                    Console.WriteLine($"Security Level: {securityLevel}%");
                    Console.WriteLine("Vulnerabilities detected: 2");
                    Console.WriteLine("- Outdated firewall");
                    Console.WriteLine("- Weak encryption");
                }
            }
            else
            {
                if (!isAI)
                {
                    Console.WriteLine("No significant vulnerabilities found.");
                }
            }
        }

        private void AttackSystem(string ip, bool isAI = false)
        {
            if (ip == enemyIP)
            {
                if (!isAI)
                {
                    Console.WriteLine("Attempting attack...");
                }
                Thread.Sleep(2000);

                Random rand = new Random();
                int attackStrength = rand.Next(30, 100);

                if (attackStrength > securityLevel)
                {
                    if (!isAI)
                    {
                        Console.WriteLine("Attack successful!");
                        Console.WriteLine("System compromised!");
                        Console.WriteLine("You win!");
                    }
                    isGameOver = true;
                }
                else
                {
                    securityLevel -= (attackStrength / 2);
                    if (!isAI)
                    {
                        Console.WriteLine("Attack failed!");
                        Console.WriteLine($"Target security level reduced to {securityLevel}%");
                    }
                }
            }
            else
            {
                if (!isAI)
                {
                    Console.WriteLine("Attack failed: Invalid target");
                }
            }
        }


        private void DDoSAttack(string ip)
        {
            Console.WriteLine($"Launching DDoS attack on {ip}...");
            Thread.Sleep(2000);

            Random rand = new Random();
            int attackStrength = rand.Next(40, 100);

            if (ip == enemyIP)
            {
                if (attackStrength > securityLevel)
                {
                    Console.WriteLine("DDoS attack successful!");
                    Console.WriteLine("System overwhelmed!");
                    Console.WriteLine("You win!");
                    isGameOver = true;
                }
                else
                {
                    securityLevel -= (attackStrength / 2);
                    Console.WriteLine("DDoS attack failed!");
                    Console.WriteLine($"Target security level reduced to {securityLevel}%");
                }
            }
            else
            {
                Console.WriteLine("DDoS attack failed: Invalid target");
            }
        }

        private void PhishingAttack(string ip)
        {
            Console.WriteLine($"Launching phishing attack on {ip}...");
            Thread.Sleep(2000);

            Random rand = new Random();
            int attackStrength = rand.Next(30, 90);

            if (ip == enemyIP)
            {
                if (attackStrength > securityLevel)
                {
                    Console.WriteLine("Phishing attack successful!");
                    Console.WriteLine("Credentials compromised!");
                    Console.WriteLine("You win!");
                    isGameOver = true;
                }
                else
                {
                    securityLevel -= (attackStrength / 2);
                    Console.WriteLine("Phishing attack failed!");
                    Console.WriteLine($"Target security level reduced to {securityLevel}%");
                }
            }
            else
            {
                Console.WriteLine("Phishing attack failed: Invalid target");
            }
        }

        private void MalwareAttack(string ip)
        {
            Console.WriteLine($"Launching malware attack on {ip}...");
            Thread.Sleep(2000);

            Random rand = new Random();
            int attackStrength = rand.Next(50, 100);

            if (ip == enemyIP)
            {
                if (attackStrength > securityLevel)
                {
                    Console.WriteLine("Malware attack successful!");
                    Console.WriteLine("System infected!");
                    Console.WriteLine("You win!");
                    isGameOver = true;
                }
                else
                {
                    securityLevel -= (attackStrength / 2);
                    Console.WriteLine("Malware attack failed!");
                    Console.WriteLine($"Target security level reduced to {securityLevel}%");
                }
            }
            else
            {
                Console.WriteLine("Malware attack failed: Invalid target");
            }
        }

        private void TraceRoute(string ip)
        {
            Console.WriteLine($"Tracing route to {ip}...");
            Thread.Sleep(1000);

            if (networkMap.ContainsKey(ip))
            {
                Console.WriteLine($"Route to {ip}:");
                NetworkNode node = networkMap[ip];
                Console.WriteLine($"- {node.IP} (Distance: {node.Distance} hops)");
                foreach (var connectedIP in node.ConnectedIPs)
                {
                    Console.WriteLine($"  └─── Connected to: {connectedIP}");
                }
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }

        private void DisconnectIP(string ip)
        {
            Console.WriteLine($"Disconnecting {ip}...");
            Thread.Sleep(1000);

            if (networkMap.ContainsKey(ip))
            {
                networkMap.Remove(ip);
                Console.WriteLine($"{ip} has been disconnected from the network.");
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }

        private void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("- nmap [basic|deep|stealth] : Scan for nearby IPs");
            Console.WriteLine("- ping <ip_address> : Probe specific IP for vulnerabilities");
            Console.WriteLine("- nc <ip_address> : Attempt to exploit a system");
            Console.WriteLine("- hping3 <ip_address> : Launch a DDoS attack on a system");
            Console.WriteLine("- social-engineer <ip_address> : Launch a phishing attack on a system");
            Console.WriteLine("- msfconsole <ip_address> : Launch a malware attack on a system");
            Console.WriteLine("- traceroute <ip_address> : Trace the route to a specific IP");
            Console.WriteLine("- openssl enc <ip_address> : Encrypt a specific IP to increase its security");
            Console.WriteLine("- openssl dec <ip_address> : Decrypt a specific IP to decrease its security");
            Console.WriteLine("- ufw <ip_address> : Upgrade the security level of a specific IP");
            Console.WriteLine("- tcpdump : Monitor network traffic for suspicious activity");
            Console.WriteLine("- rsync : Backup your data to prevent data loss");
            Console.WriteLine("- iptables <ip_address> : Disconnect a specific IP from the network");
            Console.WriteLine("- install-firewall <ip_address> : Install a firewall to upgrade the security level of a specific IP");
            Console.WriteLine("- patch-system <ip_address> : Patch the system to repair the security level of a specific IP");
            Console.WriteLine("- help : Show this help message");
            Console.WriteLine("- exit : Exit game");
            Console.ResetColor();
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
                    networkMap[enemyIP].AddConnectedIP(connectedIP);
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
                string[] nodeTypes = { "Server", "Workstation", "Router" };
                Random rand = new Random();
                string nodeType = nodeTypes[rand.Next(nodeTypes.Length)];
                int securityLevel = rand.Next(50, 100); // Security level between 50 and 100

                networkMap[ip] = new NetworkNode(ip, nodeType, securityLevel)
                {
                    Distance = distance
                };

                // Connect the new node to a random existing node
                if (networkMap.Count > 1)
                {
                    var existingNode = networkMap.Values.ElementAt(rand.Next(networkMap.Count - 1));
                    existingNode.AddConnectedIP(ip);
                    networkMap[ip].AddConnectedIP(existingNode.IP);
                }
            }
        }

        private void DisplayNetworkMap()
        {
            Console.WriteLine("\nNetwork Map:");
            foreach (var node in networkMap.Values.OrderBy(n => n.Distance))
            {
                Console.WriteLine($"└─[{node.IP}] - {node.NodeType} - Security Level: {node.SecurityLevel}% - Distance: {node.Distance} hops");
                foreach (var connectedIP in node.ConnectedIPs)
                {
                    Console.WriteLine($"  └─── Connected to: {connectedIP}");
                }
            }
        }

        private void AITakeTurn()
        {
            foreach (var aiIP in aiIPs)
            {
                Random rand = new Random();
                int action = rand.Next(1, 5);

                switch (action)
                {
                    case 1:
                        // AI performs a network scan
                        ScanNetwork("basic", true);
                        break;
                    case 2:
                        // AI probes a random IP
                        if (networkMap.Count > 0)
                        {
                            string targetIP = networkMap.Keys.ElementAt(rand.Next(networkMap.Count));
                            ProbeIP(targetIP, true);
                        }
                        break;
                    case 3:
                        // AI attacks a random IP
                        if (networkMap.Count > 0)
                        {
                            string targetIP = networkMap.Keys.ElementAt(rand.Next(networkMap.Count));
                            AttackSystem(targetIP, true);
                        }
                        break;
                    case 4:
                        // AI installs a firewall
                        InstallFirewall(aiIP);
                        break;
                    case 5:
                        // AI patches its system
                        PatchSystem(aiIP);
                        break;
                }
            }
        }
        private void IntroduceRandomEvent()
        {
            Random rand = new Random();
            int eventChance = rand.Next(100);

            if (eventChance < 20) // 20% chance for a random event
            {
                Console.WriteLine("A mysterious signal has been detected...");
                string mysteriousIP = GenerateRandomIP();
                AddNodeToNetwork(mysteriousIP, rand.Next(1, 5));
                Console.WriteLine($"Discovered mysterious IP: {mysteriousIP} (Distance: {networkMap[mysteriousIP].Distance} hops)");
            }
            else if (eventChance < 40) // Another 20% chance for a different event
            {
                Console.WriteLine("A network anomaly has been detected...");
                string anomalyIP = GenerateRandomIP();
                AddNodeToNetwork(anomalyIP, rand.Next(1, 5));
                Console.WriteLine($"Anomaly detected at IP: {anomalyIP} (Distance: {networkMap[anomalyIP].Distance} hops)");
            }
        }

        private void ExploitSystem(string ip)
        {
            if (ip == enemyIP)
            {
                Console.WriteLine("Attempting to exploit system...");
                Thread.Sleep(2000);

                Random rand = new Random();
                int exploitStrength = rand.Next(30, 100);

                if (exploitStrength > securityLevel)
                {
                    Console.WriteLine("Exploit successful!");
                    Console.WriteLine("System compromised!");
                    Console.WriteLine("You win!");
                    isGameOver = true;
                }
                else
                {
                    securityLevel -= (exploitStrength / 2);
                    Console.WriteLine("Exploit failed!");
                    Console.WriteLine($"Target security level reduced to {securityLevel}%");
                }
            }
            else
            {
                Console.WriteLine("Exploit failed: Invalid target");
            }
        }

        private void EncryptIP(string ip)
        {
            if (networkMap.ContainsKey(ip))
            {
                Console.WriteLine($"Encrypting IP {ip}...");
                Thread.Sleep(1000);
                networkMap[ip].SecurityLevel += 20;
                Console.WriteLine($"{ip} is now encrypted. Security level increased to {networkMap[ip].SecurityLevel}%.");
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }

        private void DecryptIP(string ip)
        {
            if (networkMap.ContainsKey(ip))
            {
                Console.WriteLine($"Decrypting IP {ip}...");
                Thread.Sleep(1000);
                networkMap[ip].SecurityLevel -= 20;
                Console.WriteLine($"{ip} is now decrypted. Security level decreased to {networkMap[ip].SecurityLevel}%.");
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }

        private void UpgradeSecurity(string ip)
        {
            if (networkMap.ContainsKey(ip))
            {
                Console.WriteLine($"Upgrading security for IP {ip}...");
                Thread.Sleep(1000);
                networkMap[ip].SecurityLevel += 10;
                Console.WriteLine($"{ip} security level increased to {networkMap[ip].SecurityLevel}%.");
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }

        private void MonitorTraffic()
        {
            Console.WriteLine("Monitoring network traffic...");
            Thread.Sleep(1000);
            Console.WriteLine("No suspicious activity detected.");
        }

        private void BackupData()
        {
            Console.WriteLine("Backing up data...");
            Thread.Sleep(1000);
            Console.WriteLine("Data backup complete.");
        }

        private void InstallFirewall(string ip)
        {
            if (networkMap.ContainsKey(ip))
            {
                Console.WriteLine($"Installing firewall for IP {ip}...");
                Thread.Sleep(1000);
                networkMap[ip].IncreaseSecurityLevel(10);
                Console.WriteLine($"{ip} security level increased to {networkMap[ip].SecurityLevel}%.");
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }

        private void PatchSystem(string ip)
        {
            if (networkMap.ContainsKey(ip))
            {
                Console.WriteLine($"Patching system for IP {ip}...");
                Thread.Sleep(1000);
                networkMap[ip].RepairSecurityLevel(20);
                Console.WriteLine($"{ip} security level repaired to {networkMap[ip].SecurityLevel}%.");
            }
            else
            {
                Console.WriteLine("IP not found in network map.");
            }
        }
    }
}
