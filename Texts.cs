using System;
using Terraria.ModLoader;

namespace Redemption
{
	public class Texts : ModPlayer
	{
		public override void ResetEffects()
		{
			this.text = false;
		}

		public bool text;

		public int BossID;

		public int TextID;
	}
}
