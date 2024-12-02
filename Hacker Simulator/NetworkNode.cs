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

        public NetworkNode(string ip)
        {
            IP = ip;
            Distance = 0;
            IsActive = true;
            ConnectedIPs = new List<string>();
        }
    }
}
