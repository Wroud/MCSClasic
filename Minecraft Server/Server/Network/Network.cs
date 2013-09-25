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
            packets.Add(0, Packet0KeepAlive.Read);
            packets.Add(2, Packet2Protocol.Read);
            packets.Add(204, Packet204ClientInfo.Read);
            packets.Add(250, Packet250CustomPayload.Read);
            packets.Add(205, Packet205ClientCommand.Read);
            packets.Add(252, Packet252SharedKey.Read);
            packets.Add(254, Packet254ServerPing.Read);
        }
    }
}
