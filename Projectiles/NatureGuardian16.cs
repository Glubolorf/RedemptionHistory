﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class NatureGuardian16 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holy Statuette");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 50;
			base.projectile.height = 46;
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
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			if (!player.HasBuff(base.mod.BuffType("NatureGuardian16Buff")))
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
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 57, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				Projectile.NewProjectile(player.position, Vector2.Zero, base.mod.ProjectileType("HolyAura"), 0, 0f, player.whoAmI, 0f, 0f);
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.position.X = player.Center.X - 25f;
			base.projectile.position.Y = player.Center.Y - 74f;
			this.shootTimer++;
			if (this.shootTimer >= 80)
			{
				if (Main.rand.Next(10) == 0)
				{
					if (base.projectile.spriteDirection == -1)
					{
						Main.PlaySound(SoundID.Item105, (int)base.projectile.position.X, (int)base.projectile.position.Y);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(0f, -12f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(0f, 12f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(-12f, 0f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(12f, 0f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(8f, 8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(8f, -8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(-8f, 8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(-8f, -8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item105, (int)base.projectile.position.X, (int)base.projectile.position.Y);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(0f, -12f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(0f, 12f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(-12f, 0f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(12f, 0f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(8f, 8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(8f, -8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(-8f, 8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
						Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(-8f, -8f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
					}
				}
				if (base.projectile.spriteDirection == -1)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 16f, base.projectile.position.Y + 31f), new Vector2(-8f, 0f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
				}
				else
				{
					Projectile.NewProjectile(new Vector2(base.projectile.position.X + 33f, base.projectile.position.Y + 31f), new Vector2(8f, 0f), base.mod.ProjectileType("LastRPro5"), 30, 3f, Main.myPlayer, 0f, 0f);
				}
				this.shootTimer = 0;
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.natureGuardian16 = false;
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
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 57, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		private int shootTimer;

		private bool shootBarrage;
	}
}
