﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class InfectousJavelinPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Projectiles/v08/" + base.GetType().Name + "_Glow");
				InfectousJavelinPro.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Infectious Javelin");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.penetrate = 3;
			base.projectile.glowMask = InfectousJavelinPro.customGlowMask;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 10f)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("InfectiousSludgePro"), 30, 4f, Main.myPlayer, 0f, 0f);
				base.projectile.localAI[0] = 0f;
			}
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
				Dust dust = Dust.NewDustDirect(vector, base.projectile.width, base.projectile.height, 74, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += vector2 * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				vector -= vector2 * 8f;
			}
		}

		public static short customGlowMask;
	}
}