﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class LastRPro4 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/Melee/" + base.GetType().Name);
				LastRPro4.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Redemptive Embrace");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 408;
			base.projectile.height = 408;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 100;
			base.projectile.glowMask = LastRPro4.customGlowMask;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (!player.HasBuff(ModContent.BuffType<RedemptiveEmbraceBuff>()))
			{
				base.projectile.Kill();
			}
			base.projectile.velocity *= 0f;
			base.projectile.rotation += 0.06f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
			base.projectile.position.X = player.Center.X - 204f;
			base.projectile.position.Y = player.Center.Y - 204f;
		}

		public static short customGlowMask;
	}
}
