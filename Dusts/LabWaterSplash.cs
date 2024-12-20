using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class LabWaterSplash : ModDust
	{
		public override void SetDefaults()
		{
			this.updateType = 33;
		}

		public override void OnSpawn(Dust dust)
		{
			dust.alpha = 170;
			dust.velocity *= 0.5f;
			dust.velocity.Y = dust.velocity.Y + 1f;
		}
	}
}
