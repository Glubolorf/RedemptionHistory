using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class RainbowStarDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.fadeIn = Utils.NextFloat(Main.rand, 0.8f, 1.8f);
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale = Utils.NextFloat(Main.rand, 0.2f, 0.5f);
			dust.alpha = 255;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color?(dust.color);
		}

		public override bool Update(Dust dust)
		{
			dust.color = Main.DiscoColor;
			dust.position += dust.velocity;
			if (dust.fadeIn > dust.scale)
			{
				dust.scale += 0.02f;
				dust.velocity *= 0.96f;
				if (dust.alpha > 0)
				{
					dust.alpha -= 8;
				}
			}
			else
			{
				dust.alpha += 2;
				dust.fadeIn = 0f;
				dust.scale *= 0.9f;
				dust.velocity *= 0.9f;
				if ((double)dust.scale < 0.001)
				{
					dust.active = false;
				}
			}
			return false;
		}
	}
}
