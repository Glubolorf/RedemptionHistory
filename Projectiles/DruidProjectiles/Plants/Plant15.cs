using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant15 : DruidPlant
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
				Plant15.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Fireleaf Plant");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 38;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 160;
			base.projectile.glowMask = Plant15.customGlowMask;
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
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] % 30f == 0f && base.projectile.frame >= 4)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), base.projectile.velocity, ModContent.ProjectileType<PollenCloud5>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			}
			if (base.projectile.frame >= 4 && this.IsOnNativeTerrain)
			{
				if (Main.rand.Next(20) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 12f, (float)(-3 + Main.rand.Next(0, 6)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<FieryOil>(), 7, 0f, base.projectile.owner, 0f, 0f);
				}
				if (Main.rand.Next(20) == 0)
				{
					Projectile.NewProjectile(base.projectile.position.X + 28f, base.projectile.position.Y + 18f, (float)(-3 + Main.rand.Next(0, 6)), (float)(-3 + Main.rand.Next(0, 6)), ModContent.ProjectileType<FieryOil>(), 7, 0f, base.projectile.owner, 0f, 0f);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 6, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public static short customGlowMask;
	}
}
