using System;
using System.IO;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Security.AccessControl;
//using System.Security.Principal;
using System.IO.Compression;
using System.Threading;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Server.Client;
using System.Collections.Generic;

namespace Minecraft_Server.Server.Main
{
    class Main : Framework.Main.Main
    {
        public static Thread users;
        public static Dictionary<ushort,Player> players;
        new public static void Initz()
        {
            players = new Dictionary<ushort, Player>();
            World.Initialize();

            users = new Thread(Users);
            users.Start();
        }
        public static void Users()
        {
            while (true)
            {
                foreach (var con in Network.Network.net.connects.Values)
                    foreach (var us in Network.Network.net.connects.Values)
                        if (us.id != con.id)
                            new Packet8Position((TcpClientm)con, (sbyte)us.id, players[us.id].Position, players[us.id].Rotation).Write();
                Thread.Sleep(20);
            }
        }
    }
}
