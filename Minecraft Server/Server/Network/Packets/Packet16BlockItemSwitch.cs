using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet16BlockItemSwitch : Framework.Network.Packet
    {
        private byte opcode = 16;

        public Packet16BlockItemSwitch(TcpClientm d)
        {
            this.data = d;
        }

        public static Packet Read(TcpClientm d)
        {
            return null;
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write((short)0);
            this.data.Flush();
        }
    }
}
