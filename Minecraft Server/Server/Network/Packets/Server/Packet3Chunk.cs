
namespace Minecraft_Server.Server.Network.Packets
{
    class Packet3Chunk : Framework.Network.Packet
    {
        private byte opcode = 3;
        private byte[] Chunkdata;
        private byte complete;

        public Packet3Chunk(TcpClientm d, byte[] data, byte compl)
        {
            this.data = d;
            this.Chunkdata = data;
            this.complete = compl;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Write((short)this.Chunkdata.Length);
            this.data.Write(this.Chunkdata);
            this.data.Write(this.complete);
            this.data.Flush();
        }
    }
}
