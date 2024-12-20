using System;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class RadarEffect : ModPlayer
	{
		public override void ResetEffects()
		{
			this.effect = false;
		}

		public bool effect;
	}
}
