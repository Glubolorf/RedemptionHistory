using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
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
