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

namespace Minecraft_Server.Server.Main
{
    class Main : Framework.Main.Main
    {
        public static RSACryptoServiceProvider CryptoServiceProvider;
        public static RSAParameters ServerKey;
        new public static void Initz()
        {
            CryptoServiceProvider = new RSACryptoServiceProvider(1024);
            ServerKey = CryptoServiceProvider.ExportParameters(true);
        }
    }
}
