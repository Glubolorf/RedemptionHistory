using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
{
	public class InfectionTextPlayer : ModPlayer
	{
		public override void ResetEffects()
		{
			this.text = false;
		}

		public bool text;

		public float alphaText = 255f;
	}
}
