using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet255Kick : Framework.Network.Packet
    {
        public byte opcode = 255;
        public string messag;
        public Packet255Kick(TcpClientm d,string mes)
        {
            this.data = d;
            this.messag = mes;
        }
        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(this.opcode);
            WriteString(this.data, this.messag);
            this.data.Flush();
        }
    }
}
