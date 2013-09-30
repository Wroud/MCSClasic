using Minecraft_Server.Server.Utils;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet4LevelFin : Framework.Network.Packet
    {
        private byte opcode = 4;
        private Vector3 size;

        public Packet4LevelFin(TcpClientm d, Vector3 size)
        {
            this.data = d;
            this.size = size;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(this.opcode);
            this.data.Write(this.size);
            this.data.Flush();
        }
    }
}
