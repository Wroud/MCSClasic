using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Utils;
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
        private Vector3 pos;
        private byte Blocktype;

        public Packet6SetBlock(TcpClientm d, Vector3 pos,byte Blocktype)
        {
            this.data = d;
            this.pos = pos;
            this.Blocktype = Blocktype;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(pos);
            this.data.Write(Blocktype);
            this.data.Flush();
        }
    }
}
