using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Util;
using Minecraft_Server.Server.Utils;

namespace Minecraft_Server.Server.Client
{
    partial class Client : Framework.Client.Client
    {
        private Network.TcpClientm Net;


        public void onCBlock(Vector3 pos, byte mod, byte type)
        {
            World.worlds[level].Change(pos, type, mod, (sbyte)Net.id);
        }

        public void onPosition(Vector3 pos, Vector2 rot)
        {
            Position = pos;
            Rotation = rot;
        }

        public void onJoin(byte protocolVersion, string name, string vk)
        {
            if (protocolVersion != 7)
            {
                new PacketKick(this.Net, "Bad protocol version").Write();
                return;
            }

            this.username = name;
            Load();
            this.verfikey = vk;

            new Packet0Indentification(this.Net, protocolVersion, isop).Write();//0x64 будет оп
            ChangeWorld(this.level);
            new Packet13Message(this.Net, (sbyte)-1, Config.motd).Write();
            Main.Main.players++;
        }
    }
}
