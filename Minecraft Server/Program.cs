using Minecraft_Server.Framework.Util;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Server.Util;
using System;
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
                Log.Update("Инициализация", ".");
            }
            Network.Run();
            Log.Info("Сервер запущен");
            HeartBeats.Start();
            while (true)
                if (Console.ReadLine() == "/save")
                    foreach (var v in World.worlds.Values)
                        v.Save();
        }
    }
}
