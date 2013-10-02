
namespace Minecraft_Server.Server.Network.Packets
{
    class Packet2Level : Framework.Network.Packet
    {
        private byte opcode = 2;

        public Packet2Level(TcpClientm d)
        {
            this.data = d;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            //Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(opcode);
            this.data.Flush();
        }
    }
}
