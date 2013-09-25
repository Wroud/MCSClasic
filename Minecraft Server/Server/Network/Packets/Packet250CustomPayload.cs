using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet250CustomPayload : Framework.Network.Packet
    {
        private byte opcode = 250;

        public Packet250CustomPayload(TcpClientm d)
        {
            this.data = d;
        }

        public static void Read(TcpClientm d)
        {
            string s = ReadString(d, 20);
            byte[] sd = ReadBytes(d);
        }
        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(opcode);
            WriteString(this.data, "MC|Brand");
            WriteString(this.data, "vanilla");
            this.data.Flush();
        }
    }
}
