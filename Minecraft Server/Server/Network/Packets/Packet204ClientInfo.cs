using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet204ClientInfo : Framework.Network.Packet
    {
        private byte opcode = 204;

        public Packet204ClientInfo(TcpClientm d)
        {
            this.data = d;
        }

        public static void Read(TcpClientm d)
        {
            string s = ReadString(d, 7);
            byte dis = d.NetStream.ReadByte(d);
            byte v = d.NetStream.ReadByte(d);
            int chvs = v & 7;
            bool ccol = (v & 8) == 8;
            byte dif = d.NetStream.ReadByte(d);
            bool shc = d.NetStream.ReadByte(d) == 1;
            d.cli.onClientInfo(s, dis, chvs, ccol, dif, shc);
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Flush();
        }
    }
}
