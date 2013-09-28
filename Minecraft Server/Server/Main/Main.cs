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

namespace Minecraft_Server.Server.Main
{
    class Main : Framework.Main.Main
    {
        public static Thread users;
        new public static void Initz()
        {
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
                            new Packet8Position((TcpClientm)con, (sbyte)us.id, ((TcpClientm)us).cli.x, ((TcpClientm)us).cli.y, ((TcpClientm)us).cli.z, ((TcpClientm)us).cli.yaw, ((TcpClientm)us).cli.pitch).Write();
                Thread.Sleep(20);
            }
        }
    }
}
