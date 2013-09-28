using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Utils;
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
        private Vector3 size;

        public Packet4LevelFin(TcpClientm d, Vector3 size)
        {
            this.data = d;
            this.size = size;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            this.data.Write(this.opcode);
            this.data.Write(this.size);
            this.data.Flush();
        }
    }
}
