using Minecraft_Server.Framework.Util;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Util;
using Minecraft_Server.Server.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;

namespace Minecraft_Server.Server.Main
{
    class World
    {
        #region Static
        public static Dictionary<string, World> worlds;
        public static string[] maps;

        public static void Initialize()
        {
            worlds = new Dictionary<string, World>();

            worlds.Add(Config.level_name, new World(Config.level_name));
        }

        public static bool MapExit(string name)
        {
            maps = Directory.GetFiles("Worlds\\");
            foreach (string s in maps)
                if ("Worlds\\" + name + ".btm" == s)
                    return true;
            return false;
        }
        #endregion

        public struct Block
        {
            public Vector3 pos;
            public byte type;
            public sbyte ste;
            public Block(Vector3 p, byte t, sbyte s)
            {
                pos = p;
                type = t;
                ste = s;
            }
        }
        public byte[] memory;
        public List<sbyte> players;
        public static Thread users;
        public ConcurrentQueue<Block> messageQueue;
        public byte this[short x, short y, short z]
        {
            get { return this.memory[this.Index(x, y, z)]; }
            set { this.memory[this.Index(x, y, z)] = value; }
        }
        public byte this[Vector3 pos]
        {
            get { return this.memory[this.Index(pos)]; }
            set { this.memory[this.Index(pos)] = value; }
        }
        public Vector3 size;
        public Vector3 spawn;
        public string mpf;

        public World(string f, Vector3 s = null)
        {
            if (s == null)
                s = new Vector3(256, 64, 256);
            this.size = s;
            this.mpf = f;

            players = new List<sbyte>();
            messageQueue = new ConcurrentQueue<Block>();
            if (!Directory.Exists("Worlds\\"))
                Directory.CreateDirectory("Worlds\\");
            if (File.Exists("Worlds\\" + f + ".btm"))
                this.Load();
            else
                this.Generate();
            users = new Thread(Users);
            users.Start();
        }
        public void Users()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < messageQueue.Count; i++)
                    {
                        Block b;
                        if (messageQueue.TryDequeue(out b))
                            foreach (sbyte con in players)
                                if (con != b.ste)
                                    new Packet6SetBlock((TcpClientm)Network.Network.net.connects[(ushort)con], b.pos, b.type).Write();
                    }
                }
                catch { }
                try
                {
                    foreach (sbyte con in players)
                    {
                        foreach (sbyte us in players)
                            if (con != us)
                                new Packet8Position((TcpClientm)Network.Network.net.connects[(ushort)con], us, ((Server.Network.TcpClientm)Network.Network.net.connects[(ushort)us]).cli.Position, ((Server.Network.TcpClientm)Network.Network.net.connects[(ushort)us]).cli.Rotation).Write();

                    }
                }
                catch { }
                Thread.Sleep(10);
            }
        }

        public void Change(Vector3 pos, byte type, byte mod, sbyte ovner)
        {
            this.memory[this.Index(pos)] = (mod == 0) ? (byte)0 : type;
            this.messageQueue.Enqueue(new World.Block(pos, (mod == 0) ? (byte)0 : type, ovner));
        }

        public void Generate()
        {
            this.spawn = new Vector3(15 * 32, 1 * 32 + 51, 15 * 32);
            this.memory = new byte[this.size.X * this.size.Y * this.size.Z];
            for (int x = 0; x < this.size.X; x++)
                for (int z = 0; z < this.size.Z; z++)
                    this.memory[this.Index(x, 0, z)] = 1;
            this.Save();
        }

        public void Load()
        {
            Log.Info("Загрузка карты {0}", this.mpf);
            Stream st = File.OpenRead("Worlds\\" + this.mpf + ".btm");
            using (GZipStream read = new GZipStream(st, CompressionMode.Decompress))
            {
                byte[] l = new byte[4];

                this.size = ((Stream)read).ReadVector3();
                this.spawn = read.ReadVector3();

                read.Read(l, 0, 4);
                int len = BitConverter.ToInt32(l, 0);

                this.memory = new byte[len];
                read.Read(this.memory, 0, len);
            }
            Log.Update("Карта {0} загружена", "", 3, this.mpf);
        }

        public void Save()
        {

            Thread tc = new Thread(() =>
            {
                Log.Info("Сохранение карты {0}", this.mpf);
                Stream st = File.Create("Worlds\\" + this.mpf + ".btm");
                using (GZipStream wri = new GZipStream(st, CompressionMode.Compress))
                {
                    wri.Write(this.size);
                    wri.Write(this.spawn);
                    wri.Write(BitConverter.GetBytes(this.memory.Length), 0, 4);
                    wri.Write(this.memory, 0, this.memory.Length);
                }
                Log.Update("Карта {0} сохранена", "", 3, this.mpf);
            });
            tc.Start();
        }

        public byte[] GetGzipMap()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream compressor = new GZipStream(ms, CompressionMode.Compress))
                {
                    byte[] data = new[]
                    {
                        (byte)((this.memory.Length & 0xFF000000) >> 24),
                        (byte)((this.memory.Length & 0xFF0000) >> 16),
                        (byte)((this.memory.Length & 0xFF00) >> 8),
                        (byte)(this.memory.Length & 0xFF)
                    };
                    compressor.Write(data, 0, 4);
                    compressor.Write(this.memory, 0, this.memory.Length);
                }
                return ms.ToArray();
            }
        }

        public int Index(int x, int y, int z)
        {
            return (y * this.size.Z + z) * this.size.X + x;
        }

        public int Index(Vector3 pos)
        {
            return (pos.Y * this.size.Z + pos.Z) * this.size.X + pos.X;
        }

    }
}
