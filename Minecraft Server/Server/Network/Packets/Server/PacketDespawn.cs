using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class PacketDespawn : Framework.Network.Packet
    {
        private byte opcode = 0x0c;
        private sbyte id;

        public PacketDespawn(TcpClientm d,sbyte id)
        {
            this.data = d;
            this.id = id;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            this.data.Write(this.opcode);
            this.data.Write(this.id);
            this.data.Flush();
        }
    }
}
