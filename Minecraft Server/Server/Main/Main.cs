namespace Minecraft_Server.Server.Main
{
    class Main : Framework.Main.Main
    {
        public static int players = 0;
        new public static void Initz()
        {
            World.Initialize();
        }
    }
}
