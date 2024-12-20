using System;
using Terraria;

namespace Redemption
{
	public class MNet
	{
		public static void SendBaseNetMessage(int msg, params object[] param)
		{
			if (Main.netMode == 0)
			{
				return;
			}
			BaseNet.WriteToPacket(Redemption.inst.GetPacket(256), (byte)msg, param).Send(-1, -1);
		}
	}
}
