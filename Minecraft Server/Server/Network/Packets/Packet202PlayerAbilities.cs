using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet202PlayerAbilities : Framework.Network.Packet
    {
        private byte opcode = 202;

        public Packet202PlayerAbilities(TcpClientm d)
        {
            this.data = d;
        }

        public static Packet Read(TcpClientm d)
        {
            return null;
        }
        public override void Write()
        {
            byte byt = 0;
            if(false)//net yrony
                byt = (byte)(byt | 1);
            if (true)//letaet
                byt = (byte)(byt | 2);
            if (true)//mojno li letat
                byt = (byte)(byt | 4);
            if (false)//kreativ li
                byt = (byte)(byt | 8);
            this.data.Write(this.opcode);
            this.data.Write(byt);
            this.data.Write(1.0f);
            this.data.Write(1.0f);
            this.data.Flush();
        }
    }
}
