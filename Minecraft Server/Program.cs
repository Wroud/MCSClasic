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
            {
                string lan = Console.ReadLine();
                if (lan == "/save")
                    foreach (var v in World.worlds.Values)
                        v.Save();
                else
                {
                    string[] str = lan.Split(' ');
                    if (str[0] == "/op")
                        foreach (var us in Server.Network.Network.net.connects.Values)
                            if (((Server.Network.TcpClientm)us).cli.username == str[1])
                                ((Server.Network.TcpClientm)us).cli.isop = 0x64;
                }
            }
        }
    }
}
