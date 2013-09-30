
namespace Minecraft_Server.Server.Network.Packets
{
    class Packett : Framework.Network.Packet
    {
        private byte opcode = 2;

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Flush();
        }
    }
}
