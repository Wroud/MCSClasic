using Minecraft_Server.Server.Utils;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet5Block : Framework.Network.Packet
    {
        private byte opcode = 5;

        public static void Read(TcpClientm d)
        {
            Vector3 pos = d.NetStream.ReadVector3();
            byte mode = d.NetStream.ReadByte(d);
            byte type = d.NetStream.ReadByte(d);
            d.cli.onCBlock(pos, mode, type);
        }
        public override void Write()
        {
            Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(opcode);
            this.data.Flush();
        }
    }
}
