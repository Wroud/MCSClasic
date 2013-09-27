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
        private byte protocolVersion;
        private byte usertype;

        public Packet0Indentification(TcpClientm d,byte pv, byte ut)
        {
            this.data = d;
            this.protocolVersion = pv;
            this.usertype = ut;
        }

        public static void Read(TcpClientm d)
        {
            byte pv = d.NetStream.ReadByte(d);
            string str = d.NetStream.ReadString(d);
            string vk = d.NetStream.ReadString(d);
            d.NetStream.ReadByte(d);
            d.cli.onJoin(pv,str,vk);
        }

        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(protocolVersion);
            this.data.Write(ServerConf.Config.server_name);
            this.data.Write(ServerConf.Config.motd);
            this.data.Write(usertype);
            this.data.Flush();
        }
    }
}
