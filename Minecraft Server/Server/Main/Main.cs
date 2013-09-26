using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO.Compression;

namespace Minecraft_Server.Server.Main
{
    class Main : Framework.Main.Main
    {
        public static byte[] olda = new byte[262144];
        new public static void Initz()
        {
            for (int x = 0; x < 64; x++)
                for (int z = 0; z < 64; z++)
                    olda[Index(x, z, 0)] = 1;
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream compressor = new GZipStream(ms, CompressionMode.Compress))
                {
                    byte[] data = new[]
                    {
                        (byte)((262144 & 0xFF000000) >> 24),
                        (byte)((262144 & 0xFF0000) >> 16),
                        (byte)((262144 & 0xFF00) >> 8),
                        (byte)(262144 & 0xFF)
                    };
                    compressor.Write(data, 0, 4);
                    compressor.Write(olda, 0, olda.Length);
                }
                olda = ms.ToArray();
            }
        }
        public static int Index(int x, int y, int z)
        {
            return (z * 64 + y) * 64 + x;
        }
    }
}
