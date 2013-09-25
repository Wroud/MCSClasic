using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet0KeepAlive : Framework.Network.Packet
    {
        private byte opcode = 0;
        private int rand;

        public Packet0KeepAlive(TcpClientm data)
        {
            this.data = data;
        }
        public Packet0KeepAlive(TcpClientm d,int r)
        {
            this.data = d;
            this.rand = r;
        }
        
        public static void Read(TcpClientm d)
        {
            int r = d.NetStream.ReadInt32(d);
            //return new Packet0KeepAlive(d,r);
        }
        //public override void Process()
        //{
        //    this.Write();
        //}
        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Write(this.rand);
            this.data.Flush();
        }
    }
}
