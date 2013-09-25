using Craft.Net.Common;
using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet205ClientCommand : Framework.Network.Packet
    {
        private byte opcode = 205;
        public byte status = 0;

        public Packet205ClientCommand(TcpClientm data)
        {
            this.data = data;
        }
        public Packet205ClientCommand(TcpClientm data,byte s)
        {
            this.data = data;
            this.status = s;
        }
        public static void Read(TcpClientm d)
        {
            byte s = d.NetStream.ReadByte(d);
            d.cli.onCommand(s);
        }
        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Write(this.status);
            this.data.Flush();
        }
    }
}
