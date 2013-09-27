using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet6SetBlock : Framework.Network.Packet
    {
        private byte opcode = 6;
        private short x,y,z;
        private byte Blocktype;

        public Packet6SetBlock(TcpClientm d, short x, short y, short z,byte Blocktype)
        {
            this.data = d;
            this.x = x;
            this.y = y;
            this.z = z;
            this.Blocktype = Blocktype;
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
            this.data.Write(Blocktype);
            this.data.Flush();
        }
    }
}
