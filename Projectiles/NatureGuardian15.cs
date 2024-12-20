using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class NatureGuardian15 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("True Lunar Statuette");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 46;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 80;
			base.projectile.timeLeft = 36000;
			base.projectile.netImportant = true;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 6)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			if (!player.HasBuff(base.mod.BuffType("NatureGuardian15Buff")))
			{
				base.projectile.Kill();
			}
			if (player.direction == 1)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				for (int i = 0; i < 25; i++)
				{
					int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num].velocity *= 1.4f;
				}
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.position.X = player.Center.X - 23f;
			base.projectile.position.Y = player.Center.Y - 94f;
			this.shootTimer++;
			if (this.shootTimer >= 40)
			{
				if (Main.rand.Next(10) == 0)
				{
					Main.PlaySound(SoundID.Item105, (int)base.projectile.position.X, (int)base.projectile.position.Y);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(0f, -12f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(0f, 12f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(-12f, 0f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(12f, 0f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(8f, 8f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(8f, -8f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(-8f, 8f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(-8f, -8f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(0f, -8f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(0f, 8f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(-8f, 0f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(8f, 0f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(4f, 4f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(4f, -4f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(-4f, 4f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 13f, base.projectile.position.Y + 16f), new Vector2(-4f, -4f), 503, 60, 7f, Main.myPlayer, 0f, 0f);
				}
				if (Main.rand.Next(10) == 0)
				{
					if (base.projectile.spriteDirection == -1)
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-6f, 4f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-6f, -4f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-4f, 6f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-4f, -6f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
					}
					else
					{
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 12f, base.projectile.position.Y + 16f), new Vector2(6f, 4f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 12f, base.projectile.position.Y + 16f), new Vector2(6f, -4f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-4f, 6f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-4f, -6f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
					}
				}
				if (base.projectile.spriteDirection == -1)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 14f, base.projectile.position.Y + 16f), new Vector2(-8f, 0f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
				}
				else
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 12f, base.projectile.position.Y + 16f), new Vector2(8f, 0f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
				}
				this.shootTimer = 0;
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.dead)
			{
				modPlayer.natureGuardian15 = false;
			}
			if (modPlayer.natureGuardian15)
			{
				base.projectile.timeLeft = 2;
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num].velocity *= 1.4f;
			}
		}

		private int shootTimer;

		private bool shootBarrage;
	}
}
