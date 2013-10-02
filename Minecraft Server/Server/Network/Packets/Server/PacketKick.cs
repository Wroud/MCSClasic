
namespace Minecraft_Server.Server.Network.Packets
{
    class PacketKick : Framework.Network.Packet
    {
        private byte opcode = 0x0e;
        private string message;

        public PacketKick(TcpClientm d, string mes)
        {
            this.data = d;
            this.message = mes;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            //Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(opcode);
            this.data.Write(message);
            this.data.Flush();
        }
    }
}
