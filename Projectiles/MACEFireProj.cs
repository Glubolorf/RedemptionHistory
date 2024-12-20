using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MACEFireProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fireball Blast");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 56;
			base.projectile.height = 56;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 300;
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
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 158, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0.3f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] == 0f)
			{
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 158, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex].velocity *= 2.6f;
				}
				base.projectile.localAI[0] = 1f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Item14.WithVolume(0.2f), base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 158, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.3f;
			}
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = -oldVelocity.Y;
			}
			base.projectile.velocity *= 0.98f;
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(24, 160, false);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, base.projectile.position);
			for (int i = 0; i < 40; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 158, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 2.3f;
			}
			int pieCut = 10;
			for (int j = 0; j < pieCut; j++)
			{
				int projID = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<MACEMiniblast>(), 40, 3f, 255, 0f, 0f);
				Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)j / (float)pieCut * 6.28f);
			}
		}
	}
}
