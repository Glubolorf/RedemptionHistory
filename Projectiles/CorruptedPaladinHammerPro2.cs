﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class CorruptedPaladinHammerPro2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/CorruptedPaladinHammerPro";
			}
		}

		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/CorruptedPaladinHammerPro_Glow");
				CorruptedPaladinHammerPro2.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Corrupted Paladin Hammer");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(300);
			this.aiType = 300;
			base.projectile.glowMask = CorruptedPaladinHammerPro2.customGlowMask;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 235, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
