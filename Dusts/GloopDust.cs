using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class GloopDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = false;
			dust.noLight = true;
			dust.scale = 2f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X;
			dust.scale -= 0.1f;
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			else
			{
				float num = dust.scale / 2f;
			}
			return false;
		}
	}
}
