
namespace Minecraft_Server.Framework.Main
{
    class Main : Object
    {
        public static Main ser;
        public static bool Init { get { return ser.Inite; } }

        public static void Initz()
        {
            ser = new Main();
            ser.Initialize();
        }

        public void Initialize()
        {
            this.Inite = true;
        }
    }
}
