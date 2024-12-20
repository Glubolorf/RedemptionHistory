﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Stave.Guardians
{
	public class NatureGuardian15 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("True Lunar Statuette");
			Main.projPet[base.projectile.type] = true;
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
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
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
				if (num >= 6)
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
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.position.X = player.Center.X - 23f;
			base.projectile.position.Y = player.Center.Y - 94f;
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 40f == 0f)
			{
				if (Main.rand.Next(10) == 0)
				{
					Main.PlaySound(SoundID.Item105, (int)base.projectile.position.X, (int)base.projectile.position.Y);
					for (int j = -4; j <= 4; j++)
					{
						int p = Projectile.NewProjectile(base.projectile.Center, 12f * Utils.RotatedBy(Vector2.UnitX, 0.7853981633974483 * (double)j, default(Vector2)), 503, 60, 7f, Main.myPlayer, 0f, 0f);
						Main.projectile[p].GetGlobalProjectile<DruidProjectile>().druidic = true;
						Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromStave = true;
						Main.projectile[p].melee = false;
					}
					for (int k = -4; k <= 4; k++)
					{
						int p2 = Projectile.NewProjectile(base.projectile.Center, 8f * Utils.RotatedBy(Vector2.UnitX, 0.7853981633974483 * (double)k, default(Vector2)), 503, 60, 7f, Main.myPlayer, 0f, 0f);
						Main.projectile[p2].GetGlobalProjectile<DruidProjectile>().druidic = true;
						Main.projectile[p2].GetGlobalProjectile<DruidProjectile>().fromStave = true;
						Main.projectile[p2].melee = false;
					}
				}
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
				}
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(15f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("NightSoulPro2"), 35, 3f, Main.myPlayer, 0f, 0f);
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.natureGuardian15 = false;
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
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		private NPC target;
	}
}
