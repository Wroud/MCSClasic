using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet1Login : Framework.Network.Packet
    {
        private byte opcode = 1;

        public Packet1Login(TcpClientm d)
        {
            this.data = d;
        }

        public static Packet Read(TcpClientm d)
        {
            return null;
        }
        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Write((int)this.data.id);
            WriteString(this.data, "DEFAULT");
            this.data.Write((byte)2);// какая-то хрень мира
            this.data.Write((byte)0);// тип мира -1 0 1
            this.data.Write((byte)3);// какая-то хрень мира
            this.data.Write((byte)255);//высота мира
            this.data.Write((byte)255);//макс игроков
            this.data.Flush();

        }
    }
}
