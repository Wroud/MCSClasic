using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minecraft_Server.Server.Client;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Framework.Util;
using System.Threading;

namespace Minecraft_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Info("Minecraft Server");
            Network.Initz();
            Server.Main.Main.Initz();
            Log.Info("Инициализация");
            ServerConf.Start();
            while (!Network.Init && !Server.Main.Main.Init)
            {
                Thread.Sleep(700);
                Log.Update("Инициализация",".");
            }
            Network.Run();
            Log.Info("Сервер запущен");
            while (true)
                Thread.Sleep(700);
        }
    }
}
