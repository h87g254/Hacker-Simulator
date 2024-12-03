using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hacker_Simulator
{
    public class NetworkNode
    {
        public string IP { get; set; }
        public int Distance { get; set; }
        public bool IsActive { get; set; }
        public List<string> ConnectedIPs { get; set; }
        public string NodeType { get; set; }
        public int SecurityLevel { get; set; }

        public NetworkNode(string ip, string nodeType, int securityLevel)
        {
            IP = ip;
            Distance = 0;
            IsActive = true;
            ConnectedIPs = new List<string>();
            NodeType = nodeType;
            SecurityLevel = securityLevel;
        }
        public void AddConnectedIP(string ip)
        {
            if (!ConnectedIPs.Contains(ip))
            {
                ConnectedIPs.Add(ip);
            }
        }
        public void IncreaseSecurityLevel(int amount)
        {
            SecurityLevel = Math.Min(100, SecurityLevel + amount);
        }
        public void RepairSecurityLevel(int amount)
        {
            SecurityLevel = Math.Min(100, SecurityLevel + amount);
        }
    }
}
