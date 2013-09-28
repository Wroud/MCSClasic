using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Minecraft_Server.Framework.Util;
using Minecraft_Server.Server.Util;

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
        public short sx, sy, sz;
        public short stx, sty, stz;
        public string mpf;

        public World(string f, short x = 256, short y = 4, short z = 256)
        {
            this.sx = x;
            this.sy = y;
            this.sz = z;
            this.mpf = f;

            if (File.Exists(f + ".btm"))
                this.Load();
            else
                this.Generate();
        }

        public void Generate()
        {
            this.stx = 15 * 32;
            this.stz = 15 * 32;
            this.sty = 1 * 32 + 51;
            this.memory = new byte[sx * sy * sz];
            for (int x = 0; x < sx; x++)
                for (int z = 0; z < sz; z++)
                    this.memory[this.Index(x, 0, z)] = 1;
            this.Save();
        }

        public void Load()
        {
            Log.Info("Загрузка карты {0}", this.mpf);
            Stream st = File.OpenRead(this.mpf + ".btm");
            using (GZipStream read = new GZipStream(st, CompressionMode.Decompress))
            {
                byte[] l = new byte[4];

                read.Read(l, 0, 2);
                this.sx = BitConverter.ToInt16(l, 0);
                read.Read(l, 0, 2);
                this.sy = BitConverter.ToInt16(l, 0);
                read.Read(l, 0, 2);
                this.sz = BitConverter.ToInt16(l, 0);
                read.Read(l, 0, 2);
                this.stx = BitConverter.ToInt16(l, 0);
                read.Read(l, 0, 2);
                this.sty = BitConverter.ToInt16(l, 0);
                read.Read(l, 0, 2);
                this.stz = BitConverter.ToInt16(l, 0);

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
            Stream st = File.Create(this.mpf + ".btm");
            using (GZipStream wri = new GZipStream(st, CompressionMode.Compress))
            {
                wri.Write(BitConverter.GetBytes(this.sx), 0, 2);
                wri.Write(BitConverter.GetBytes(this.sy), 0, 2);
                wri.Write(BitConverter.GetBytes(this.sz), 0, 2);
                wri.Write(BitConverter.GetBytes(this.stx), 0, 2);
                wri.Write(BitConverter.GetBytes(this.sty), 0, 2);
                wri.Write(BitConverter.GetBytes(this.stz), 0, 2);
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
            return (y * this.sz + z) * this.sx + x;
        }

    }
}
