using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet5Block : Framework.Network.Packet
    {
        private byte opcode = 5;

        public static void Read(TcpClientm d)
        {
            short x = d.NetStream.ReadInt16(d);
            short y = d.NetStream.ReadInt16(d);
            short z = d.NetStream.ReadInt16(d);
            byte mode = d.NetStream.ReadByte(d);
            byte type = d.NetStream.ReadByte(d);
            d.cli.onCBlock(x, y, z, mode, type);
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Flush();
        }
    }
}
