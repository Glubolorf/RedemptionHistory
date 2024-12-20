using System;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
{
	public class InfectionTextPlayer : ModPlayer
	{
		public override void ResetEffects()
		{
			this.text = false;
			this.alphaText = 0f;
		}

		public bool text;

		public float alphaText;
	}
}
