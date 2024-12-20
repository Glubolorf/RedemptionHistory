using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts.Tiles
{
	public class RadioactiveSandDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.scale *= 1f;
		}

		public override bool MidUpdate(Dust dust)
		{
			dust.rotation += dust.velocity.X / 3f;
			if (Collision.SolidCollision(dust.position - Vector2.One * 5f, 10, 10) && dust.fadeIn == 0f)
			{
				dust.scale *= 0.9f;
				dust.velocity *= 0.1f;
			}
			return false;
		}

		public override bool Update(Dust dust)
		{
			if (WorldGen.SolidTile(Framing.GetTileSafely((int)dust.position.X / 16, (int)dust.position.Y / 16)))
			{
				dust.noLight = true;
			}
			return true;
		}

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return new Color?(new Color((int)lightColor.R, (int)lightColor.G, (int)lightColor.B, 25));
		}
	}
}
