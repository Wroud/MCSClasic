
namespace Minecraft_Server.Server.Network.Packets
{
    class PacketDespawn : Framework.Network.Packet
    {
        private byte opcode = 0x0c;
        private sbyte id;

        public PacketDespawn(TcpClientm d, sbyte id)
        {
            this.data = d;
            this.id = id;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Write(this.id);
            this.data.Flush();
        }
    }
}
