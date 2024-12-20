using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.DruidDamageClass;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Stave.Guardians
{
	public class NatureGuardian25 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nature Pixies");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 68;
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
			if (num >= 3)
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
			if (!player.HasBuff(ModContent.BuffType<NatureGuardian25Buff>()))
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
				for (int i = 0; i < 5; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				for (int j = 0; j < 5; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.4f;
				}
				for (int k = 0; k < 5; k++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 107, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex3].velocity *= 1.4f;
				}
				for (int l = 0; l < 5; l++)
				{
					int dustIndex4 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 55, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex4].velocity *= 1.4f;
				}
				for (int m = 0; m < 5; m++)
				{
					int dustIndex5 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 67, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex5].velocity *= 1.4f;
				}
				for (int n = 0; n < 5; n++)
				{
					int dustIndex6 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 44, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex6].velocity *= 1.4f;
				}
				Main.PlaySound(SoundID.Item74, base.projectile.position);
			}
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f);
			base.projectile.position.X = player.Center.X - 39f;
			base.projectile.position.Y = player.Center.Y - 150f;
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 30f == 0f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<PixieSpark>(), 5, 3f, Main.myPlayer, 0f, 0f);
			}
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 25f == 0f)
			{
				if (Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 59f) : (base.projectile.position.X + 19f), base.projectile.position.Y + 39f), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), ModContent.ProjectileType<PixieCorruptSpark>(), 5, 3f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 59f) : (base.projectile.position.X + 19f), base.projectile.position.Y + 39f), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), ModContent.ProjectileType<PixieCorruptSpark>(), 5, 3f, Main.myPlayer, 0f, 0f);
				}
				Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 59f) : (base.projectile.position.X + 19f), base.projectile.position.Y + 39f), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<PixieCorruptSpark>(), 5, 3f, Main.myPlayer, 0f, 0f);
			}
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 30f == 0f)
			{
				if (Main.rand.Next(3) == 0)
				{
					Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 12f) : (base.projectile.position.X + 66f), base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), ModContent.ProjectileType<PixieAncientSpark>(), 8, 3f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 12f) : (base.projectile.position.X + 66f), base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), ModContent.ProjectileType<PixieAncientSpark>(), 8, 3f, Main.myPlayer, 0f, 0f);
				}
				Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 12f) : (base.projectile.position.X + 66f), base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<PixieAncientSpark>(), 8, 3f, Main.myPlayer, 0f, 0f);
			}
			if (base.projectile.localAI[0] % 180f == 0f)
			{
				Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 28f) : (base.projectile.position.X + 50f), base.projectile.Center.Y), new Vector2(0f, 0f), ModContent.ProjectileType<SunPulse>(), 40, 3f, Main.myPlayer, 0f, 0f);
			}
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 30f == 0f)
			{
				int p = Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 53f) : (base.projectile.position.X + 25f), base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), 309, 7, 3f, Main.myPlayer, 0f, 0f);
				p = Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 53f) : (base.projectile.position.X + 25f), base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), 309, 7, 3f, Main.myPlayer, 0f, 0f);
				p = Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == -1) ? (base.projectile.position.X + 53f) : (base.projectile.position.X + 25f), base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), 309, 7, 3f, Main.myPlayer, 0f, 0f);
				Main.projectile[p].GetGlobalProjectile<DruidProjectile>().druidic = true;
				Main.projectile[p].GetGlobalProjectile<DruidProjectile>().fromStave = true;
				Main.projectile[p].magic = false;
				Main.projectile[p].melee = false;
			}
			if (base.projectile.localAI[0] % 15f == 0f)
			{
				int p2 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, (float)(-12 + Main.rand.Next(0, 24)), (float)(-12 + Main.rand.Next(0, 24)), 567, 11, 3f, Main.myPlayer, 0f, 0f);
				Main.projectile[p2].GetGlobalProjectile<DruidProjectile>().druidic = true;
				Main.projectile[p2].GetGlobalProjectile<DruidProjectile>().fromStave = true;
				Main.projectile[p2].magic = false;
				Main.projectile[p2].melee = false;
				Main.projectile[p2].ranged = false;
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.natureGuardian25 = false;
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int j = 0; j < 5; j++)
			{
				int dustIndex2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex2].velocity *= 1.4f;
			}
			for (int k = 0; k < 5; k++)
			{
				int dustIndex3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 107, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex3].velocity *= 1.4f;
			}
			for (int l = 0; l < 5; l++)
			{
				int dustIndex4 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 55, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex4].velocity *= 1.4f;
			}
			for (int m = 0; m < 5; m++)
			{
				int dustIndex5 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 67, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex5].velocity *= 1.4f;
			}
			for (int n = 0; n < 5; n++)
			{
				int dustIndex6 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 44, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex6].velocity *= 1.4f;
			}
		}

		private NPC target;
	}
}
