using Craft.Net.Common;
using Minecraft_Server.Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet2Protocol : Framework.Network.Packet
    {
        private byte opcode = 2;
        private byte protocolVersion;

        public Packet2Protocol(TcpClientm d, byte p)
        {
            this.data = d;
            this.protocolVersion = p;
        }

        public static void Read(TcpClientm d)
        {
            byte p = d.NetStream.ReadByte(d);
            d.cli.username = ReadString(d,16);
            ReadString(d, 255);
            d.NetStream.ReadInt32(d);
            d.cli.onJoin(p);
        }
        public override void Write()
        {
        }
    }
}
