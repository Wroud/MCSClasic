using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet4LevelFin : Framework.Network.Packet
    {
        private byte opcode = 4;
        private short x, y, z;

        public Packet4LevelFin(TcpClientm d, short x, short y, short z)
        {
            this.data = d;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(x);
            this.data.Write(y);
            this.data.Write(z);
            this.data.Flush();
        }
    }
}
