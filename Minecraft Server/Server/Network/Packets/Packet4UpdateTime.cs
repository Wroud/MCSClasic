using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet4UpdateTime : Framework.Network.Packet
    {
        private byte opcode = 4;

        public Packet4UpdateTime(TcpClientm d)
        {
            this.data = d;
        }

        public static Packet Read(TcpClientm d)
        {
            return null;
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write((long)0);
            this.data.Write((long)0);
            this.data.Flush();
        }
    }
}
