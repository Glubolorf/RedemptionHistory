using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class XenoDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.noLight = true;
			dust.velocity *= 0.4f;
			dust.scale = 1.7f;
			dust.color = new Color(0, 255, 0);
			dust.alpha = 1;
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
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), (float)dust.color.R / 255f * 0f * num, (float)dust.color.G / 255f * 0.5f * num, (float)dust.color.B / 255f * 0f * num);
			}
			return false;
		}
	}
}
