﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class ShadeJavelinPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Javelin");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 3;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 3)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return new bool?(projHitbox.Intersects(targetHitbox));
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Vector2 vector = base.projectile.position;
			Vector2 vector2 = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			vector += vector2 * 16f;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(vector, base.projectile.width, base.projectile.height, base.mod.DustType("VoidFlame"), 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += vector2 * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				vector -= vector2 * 8f;
			}
		}
	}
}