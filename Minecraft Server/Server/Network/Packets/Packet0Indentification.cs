using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet0Indentification : Framework.Network.Packet
    {
        private byte opcode = 0;

        public static void Read(TcpClientm d)
        {
            byte pv = d.NetStream.ReadByte(d);
            byte[] st = d.NetStream.ReadBytes(d, 64);
            string str = new string(UnicodeEncoding.BigEndianUnicode.GetChars(st));
            byte[] vk = d.NetStream.ReadBytes(d, 64);
            d.NetStream.ReadByte(d);
        }

        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Flush();
        }
    }
}
