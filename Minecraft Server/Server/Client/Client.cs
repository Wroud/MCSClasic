using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Util;
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
        public string level;

        public Client(Network.TcpClientm Net)
            : base(Net)
        {
            this.Net = Net;
            this.level = Config.level_name;
        }

        public void onConnect()
        {
            //Net.SendPacket(254);
            //Network.MessageQueue.Enqueue(packet);
        }

        public void onCBlock(short x, short y, short z, byte mod, byte type)
        {
            World.worlds[level][x, y, z] = (mod == 0) ? (byte)0 : type;
            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id && ((Minecraft_Server.Server.Network.TcpClientm)us).cli.level == level)
                {
                    new Packet6SetBlock((Minecraft_Server.Server.Network.TcpClientm)us, x, y, z, (mod == 0) ? (byte)0 : type).Write();
                }
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
            byte[] gz = World.worlds[level].GetGzipMap();
            for (int point = 0; point < gz.Length; )
            {
                if (point + 1024 < gz.Length)
                    Array.Copy(gz, point, da, 0, 1024);
                else
                    Array.Copy(gz, point, da, 0, gz.Length - point);
                new Packet3Chunk(this.Net, da, (byte)(point / gz.Length * 100)).Write();
                Thread.Sleep(10);
                point += 1024;
            }
            new Packet4LevelFin(this.Net, (short)World.worlds[level].sx, (short)World.worlds[level].sy, (short)World.worlds[level].sz).Write();
            Thread.Sleep(10);
            new Packet7Spawn(this.Net, (sbyte)-1, username, World.worlds[level].stx, World.worlds[level].sty, World.worlds[level].stz, 0, 0).Write();

            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id && ((Minecraft_Server.Server.Network.TcpClientm)us).cli.level == level)
                {
                    new Packet7Spawn(Net, (sbyte)us.id, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.username, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.x, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.y, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.z, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.yaw, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.pitch).Write();
                    new Packet7Spawn((Minecraft_Server.Server.Network.TcpClientm)us, (sbyte)Net.id, username, World.worlds[level].stx, World.worlds[level].sty, World.worlds[level].stz, 0, 0).Write();
                }
        }

        public void onKick()
        {
            //Net.SendPacket(254);
        }
    }
}
