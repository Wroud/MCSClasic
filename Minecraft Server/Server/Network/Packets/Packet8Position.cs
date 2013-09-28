using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet8Position : Framework.Network.Packet
    {
        private byte opcode = 8;
        private sbyte id;
        private Vector3 pos;
        private Vector2 rot;

        public Packet8Position(TcpClientm d, sbyte id, Vector3 pos, Vector2 rot)
        {
            this.data = d;
            this.id = id;
            this.pos = pos;
            this.rot = rot;
        }

        public static void Read(TcpClientm d)
        {
            d.NetStream.ReadByte(d);
            Vector3 pos = d.NetStream.ReadVector3();
            Vector2 rot = d.NetStream.ReadVector2();
            d.cli.onPosition(pos,rot);
        }
        public override void Write()
        {
            this.data.Write(opcode);
            this.data.Write(id);
            this.data.Write(pos);
            this.data.Write(rot);
            this.data.Flush();
        }
    }
}
