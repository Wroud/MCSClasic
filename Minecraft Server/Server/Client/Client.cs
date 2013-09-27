using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Client
{
    class Client : Framework.Client.Client
    {

        private Network.TcpClientm Net;
        public string username;
        public string verfikey;
        public short x, y, z;
        public byte yaw, pitch;

        public Client(Network.TcpClientm Net)
            : base(Net)
        {
            this.Net = Net;
        }

        public void onConnect()
        {
            //Net.SendPacket(254);
            //Network.MessageQueue.Enqueue(packet);
        }

        public void onCBlock(short x, short y, short z, byte mod, byte type)
        {
            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id)
                    new Packet6SetBlock((Minecraft_Server.Server.Network.TcpClientm)us, x, y, z, (mod == (byte)0) ? (byte)0 : type);
        }

        public void onPosition(short x, short y, short z, byte yaw, byte pitch)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.yaw = yaw;
            this.pitch = pitch;
        }

        public void onMessage(byte color, string mes)
        {
            mes = "[&a" + username + "&f]: " + mes;
            foreach (var us in Network.Network.net.connects.Values)
                    new Packet13Message((Minecraft_Server.Server.Network.TcpClientm)us, (sbyte)this.Net.id, mes).Write();
        }

        public void onPing(byte s, string name)
        {
        }

        public void onJoin(byte protocolVersion, string name, string vk)
        {
            if (protocolVersion != 7)
                new PacketKick(this.Net, "Bad protocol version").Write();
            this.username = name;
            this.verfikey = vk;

            new Packet0Indentification(this.Net, protocolVersion, 0);//0x64 будет оп
            Thread.Sleep(10);
            new Packet2Level(this.Net).Write();
            Thread.Sleep(10);
            byte[] da = new byte[1024];
            Array.Copy(Main.Main.olda, 0, da, 0, Main.Main.olda.Length);
            new Packet3Chunk(this.Net, da, 100).Write();
            Thread.Sleep(10);
            new Packet4LevelFin(this.Net, 64, 64, 64).Write();
            Thread.Sleep(10);
            new Packet7Spawn(this.Net, (sbyte)-1, username, 32, 32, 32, 0, 0).Write();

            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id)
                {
                    new Packet7Spawn(Net, (sbyte)us.id, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.username, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.x, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.y, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.z, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.yaw, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.pitch).Write();
                    new Packet7Spawn((Minecraft_Server.Server.Network.TcpClientm)us, (sbyte)Net.id, username, 64, 64, 64, 0, 0).Write();
                }
        }

        public void onKick()
        {
            //Net.SendPacket(254);
        }
    }
}
