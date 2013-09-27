using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet7Spawn : Framework.Network.Packet
    {
        private byte opcode = 7;
        private sbyte id;
        private string name;
        private short x, y, z;
        private byte yaw, pith;

        public Packet7Spawn(TcpClientm d, sbyte id, string name, short x, short y, short z, byte yaw, byte pith)
        {
            this.data = d;
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
            this.z = z;
            this.yaw = yaw;
            this.pith = pith;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(id);
            this.data.Write(name);
            this.data.Write(x);
            this.data.Write(y);
            this.data.Write(z);
            this.data.Write(yaw);
            this.data.Write(pith);
            this.data.Flush();
        }
    }
}
