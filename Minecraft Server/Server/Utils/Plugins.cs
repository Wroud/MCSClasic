using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Minecraft_Server.Server.Client;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Framework.Util;

namespace Minecraft_Server.Server.Utils
{
    class Plugins 
    {
        public bool CreateBlock(int x, int y, int z, int BlockType)
        {
            return true;
        }

        public bool DestroyBlock(int x, int y, int z)
        {
            return true;
        }

        public bool Message(String text)
        {
            return true;
        }

        public bool DisconnectPlayer(int ID, String Reasons)
        {
            return true;
        }

        public bool BannedPlayer(int ID, String Reasons)
        {
         //   us=Network.Network.net.connects.Values[0];
        //    new Network.Packets.PacketKick((Server.Network.TcpClientm)us,Reasons;
          //  new Network.Packets.PacketKick((Network.Network.net.connects[gg]),Reasons);
            return true;
        }

        public bool UnbannedPlayer(int ID)
        {
            return true;
        }
 }
}
