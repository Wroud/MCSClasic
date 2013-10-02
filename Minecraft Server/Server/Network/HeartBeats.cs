using Minecraft_Server.Framework.Util;
using Minecraft_Server.Server.Util;
using System.Net;
using System.Threading;

namespace Minecraft_Server.Server.Network
{
    class HeartBeats
    {
        public static WebClient http;
        public static Thread thread;
        public static void Start()
        {
            thread = new Thread(Thr);
            http = new WebClient();
            thread.Start();
            Log.Info("HeartBeat запущен");
        }
        private static void Thr()
        {
            while (true)
            {
                try
                {
                    http.DownloadData("https://minecraft.net/heartbeat.jsp"
                        + "?port=" + Config.server_port
                        + "&max=" + Config.max_players
                        + "&name=" + Config.server_name
                        + "&public=" + Config.white_list
                        + "&version=7"
                        + "&salt=" + Config.Salt
                        + "&users=" + Main.Main.players);
                }
                catch {
                    Log.Info("HeartBeats: Не удалось выполнить запрос");
                }
                Thread.Sleep(45000);
            }
        }
    }
}
