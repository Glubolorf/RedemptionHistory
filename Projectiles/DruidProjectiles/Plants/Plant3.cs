using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.DruidDamageClass;
using Terraria;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant3 : DruidPlant
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/DruidProjectiles/Plants/" + base.GetType().Name + "_Glow");
				Plant3.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Blinkroot Bush");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.glowMask = Plant3.customGlowMask;
			base.projectile.timeLeft = 7200;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		protected override void PlantAI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				if (this.IsOnNativeTerrain)
				{
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 5)
					{
						base.projectile.frame = 4;
					}
				}
			}
			if (base.projectile.frame >= 4)
			{
				int DustID2 = Dust.NewDust(base.projectile.position, base.projectile.height, base.projectile.width, 64, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
				Main.dust[DustID2].noGravity = true;
				if (this.IsOnNativeTerrain)
				{
					Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1.8f / 255f, (float)(255 - base.projectile.alpha) * 1.5f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.Center + base.projectile.velocity, base.projectile.width, base.projectile.height, 64, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
