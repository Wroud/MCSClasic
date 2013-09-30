using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Utils;
using System;
using System.Threading;

namespace Minecraft_Server.Server.Client
{
    partial class Client : Framework.Client.Client
    {
        public void onMessage(byte color, string mes)
        {
            string[] parts = mes.ToLower().Split(' ');
            switch (parts[0])
            {
                case "/world":
                    if (parts[1] != " " && parts[1] != level)
                        if (World.MapExit(parts[1]))
                        {
                            if (!World.worlds.ContainsKey(parts[1]))
                                World.worlds.Add(parts[1], new World(parts[1]));
                            ChangeWorld(parts[1], true);
                        }
                        else
                        {
                            if (parts.Length == 5)
                            {
                                World.worlds.Add(parts[1], new World(parts[1], new Vector3(Convert.ToInt16(parts[2]), Convert.ToInt16(parts[3]), Convert.ToInt16(parts[4]))));
                                ChangeWorld(parts[1], true);
                            }
                            else
                            {
                                new Packet13Message(this.Net, (sbyte)-1, "&aWorld " + parts[1] + " not found").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name]\" for teleport to the world").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name] [x] [y] [z]\" to create worlt").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&a with size x,y,z").Write();
                            }
                        }
                    break;
                default:
                    mes = "[&a" + username + "&f]: " + mes;

                    Thread tc = new Thread(() =>
                    {
                        foreach (var us in Network.Network.net.connects.Values)
                            new Packet13Message((Server.Network.TcpClientm)us, (sbyte)this.Net.id, mes).Write();
                    });
                    tc.Start();
                    break;
            }
        }
    }
}
