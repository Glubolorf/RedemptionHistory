using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class ShroomDust1 : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale = 1.5f;
			dust.frame = new Rectangle(0, Main.rand.Next(3) * 20, 12, 20);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.1f;
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
