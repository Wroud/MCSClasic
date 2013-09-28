using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Minecraft_Server.Framework.Util;
using Minecraft_Server.Server.Util;
using Minecraft_Server.Server.Utils;

namespace Minecraft_Server.Server.Main
{
    class World
    {
        public static Dictionary<string, World> worlds;

        public static void Initialize()
        {
            worlds = new Dictionary<string, World>();

            worlds.Add(Config.level_name, new World(Config.level_name));
        }

        public byte[] memory;
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
                s = new Vector3(256, 4, 256);
            this.size = s;
            this.mpf = f;

            if (File.Exists(f + ".btm"))
                this.Load();
            else
                this.Generate();
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
            Stream st = File.OpenRead("Worlds/"+this.mpf + ".btm");
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
            Log.Info("Сохранение карты {0}", this.mpf);
            Stream st = File.Create("Worlds/" + this.mpf + ".btm");
            using (GZipStream wri = new GZipStream(st, CompressionMode.Compress))
            {
                wri.Write(this.size);
                wri.Write(this.spawn);
                wri.Write(BitConverter.GetBytes(this.memory.Length), 0, 4);
                wri.Write(this.memory, 0, this.memory.Length);
            }
            Log.Update("Карта {0} сохранена", "", 3, this.mpf);
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
