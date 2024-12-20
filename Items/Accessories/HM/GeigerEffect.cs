using System;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class GeigerEffect : ModPlayer
	{
		public override void ResetEffects()
		{
			this.effect = false;
		}

		public bool effect;
	}
}
