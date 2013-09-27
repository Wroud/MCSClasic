using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet13Message : Framework.Network.Packet
    {
        private byte opcode = 13;
        private sbyte id;
        private string message;

        public Packet13Message(TcpClientm d,sbyte id, string message)
        {
            this.data = d;
            this.id = id;
            this.message = message;
        }
        public static void Read(TcpClientm d)
        {
            byte c = d.NetStream.ReadByte(d);
            string mes = d.NetStream.ReadString(d);
            d.cli.onMessage(c, mes);
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(id);
            this.data.Write(message);
            this.data.Flush();
        }
    }
}
