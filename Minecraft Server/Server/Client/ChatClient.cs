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
                                Vector3 size = new Vector3(Convert.ToInt16(parts[2]), Convert.ToInt16(parts[3]), Convert.ToInt16(parts[4]));
                                if (size.Mnog <= ((isop != 0) ? 1024 * 64 * 1024 : 512 * 64 * 512))
                                {
                                    World.worlds.Add(parts[1], new World(parts[1], size));
                                    ChangeWorld(parts[1], true);
                                }
                                else
                                    new Packet13Message(this.Net, (sbyte)-1, "&aMax size x*y*z = 512*64*512 (op 1024*64*1024) for sample").Write();
                            }
                            else if (parts.Length == 3)
                            {
                                Vector3 size;
                                if (parts[2] == "max")
                                    size = new Vector3(1024, 64, 1024);
                                else if (parts[2] == "min")
                                    size = new Vector3(64, 64, 64);
                                else
                                {
                                    new Packet13Message(this.Net, (sbyte)-1, "&aWrong message").Write();
                                    return;
                                }
                                World.worlds.Add(parts[1], new World(parts[1], size));
                                ChangeWorld(parts[1], true);
                            }
                            else
                            {
                                new Packet13Message(this.Net, (sbyte)-1, "&aWorld " + parts[1] + " not found").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name]\" for teleport to the world").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name] [x] [y] [z]\" to create worlt").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&awith size x, y, z").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name] [max/min]\" to create").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&a512*64*512 or 64*64*64 world").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aMax size x*y*z = 512*64*512 (op 1024*64*1024) for sample").Write();
                            }
                        }
                    break;
                default:
                    mes = "[&a" + username + "&f]: " + mes;
                    Main.Main.AddMesage(mes, level, (sbyte)this.Net.id, username);
                    break;
            }
        }
    }
}
