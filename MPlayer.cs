using System;
using Terraria.ModLoader;

namespace Redemption
{
	public class MPlayer : ModPlayer
	{
		public override void SetControls()
		{
			if (MPlayer.useItem)
			{
				MPlayer.useItem = false;
				base.player.delayUseItem = false;
				base.player.controlUseItem = true;
			}
		}

		public static bool useItem;
	}
}
