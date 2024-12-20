using System;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
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
