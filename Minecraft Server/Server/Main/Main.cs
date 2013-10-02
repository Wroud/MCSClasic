using Minecraft_Server.Server.Network.Packets;
using System.Collections.Concurrent;
using System.Threading;
namespace Minecraft_Server.Server.Main
{
    class Main : Framework.Main.Main
    {
        public static int players = 0;
        private static Thread chat;
        private static ConcurrentQueue<Structures.Message> messageQueue;
        new public static void Initz()
        {
            World.Initialize();
            messageQueue = new ConcurrentQueue<Structures.Message>();
            chat = new Thread(Chat);
            chat.Start();
        }

        public static void AddMesage(string mes, string level, sbyte ovn, string ovna)
        {
            messageQueue.Enqueue(new Structures.Message(mes, level, ovn, ovna));
        }

        public static void Chat()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < messageQueue.Count; i++)
                    {
                        Structures.Message b;
                        if (messageQueue.TryDequeue(out b))
                            foreach (var us in Network.Network.net.connects.Values)
                            {
                                b.message = b.message.Replace(((Server.Network.TcpClientm)us).cli.username, "&a" + ((Server.Network.TcpClientm)us).cli.username + "&f");
                                new Packet13Message(
                                    (Server.Network.TcpClientm)us,
                                    b.ovner,
                                    (((Server.Network.TcpClientm)us).cli.level != b.level)
                                    ? "[&a" + b.level + "&f]" + b.message
                                    : b.message
                                    ).Write();
                            }
                    }
                }
                catch { }
                Thread.Sleep(5);
            }
        }
    }
}
