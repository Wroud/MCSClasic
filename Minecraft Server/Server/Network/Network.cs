using Minecraft_Server.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network
{
    class Network : Framework.Network.Network
    {
        public static void Initz()
        {
            Initz(new Network(),TcpClientm.Get);
        }
        public override void SetPackets()
        {
            //packets.Add(1, new Packet1Login());
            packets.Add(0, Packet0Indentification.Read);
            packets.Add(5, Packet5Block.Read);
            packets.Add(8, Packet8Position.Read);
            packets.Add(13, Packet13Message.Read);
        }
    }
}
