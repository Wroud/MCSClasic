using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet8Position : Framework.Network.Packet
    {
        private byte opcode = 8;
        private sbyte id;
        private short x, y, z;
        private byte yaw, pith;

        public Packet8Position(TcpClientm d,sbyte id, short x, short y, short z, byte yaw, byte pith)
        {
            this.data = d;
            this.id = id;
            this.x = x;
            this.y = y;
            this.z = z;
            this.yaw = yaw;
            this.pith = pith;
        }

        public static void Read(TcpClientm d)
        {
            d.NetStream.ReadByte(d);
            short x = d.NetStream.ReadInt16(d);
            short y = d.NetStream.ReadInt16(d);
            short z = d.NetStream.ReadInt16(d);
            byte yaw = d.NetStream.ReadByte(d);
            byte pith = d.NetStream.ReadByte(d);
            d.cli.onPosition(x, y, z, yaw, pith);
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(id);
            this.data.Write(x);
            this.data.Write(y);
            this.data.Write(z);
            this.data.Write(yaw);
            this.data.Write(pith);
            this.data.Flush();
        }
    }
}
