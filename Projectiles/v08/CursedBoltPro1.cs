using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CursedBoltPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Bolt");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.ignoreWater = true;
			base.projectile.extraUpdates = 2;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.height, base.projectile.width, 75, base.projectile.velocity.X, base.projectile.velocity.Y, 200, default(Color), 1f);
				dust.velocity += base.projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 3)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[base.projectile.owner];
			Main.PlaySound(2, target.Center, 124);
			Vector2 vector12 = new Vector2(target.Center.X, target.Center.Y);
			Vector2 vector13 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num75 = 20f;
			if (vector12.Y > player.Center.Y - 200f)
			{
				Vector2 center = player.Center;
			}
			for (int num76 = 0; num76 < 6; num76++)
			{
				vector13 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * (float)player.direction, -600f);
				vector13.Y -= (float)(100 * num76);
				Vector2 vector14 = vector12 - vector13;
				if (vector14.Y < 0f)
				{
					vector14.Y *= -1f;
				}
				if (vector14.Y < 20f)
				{
					vector14.Y = 20f;
				}
				vector14.Normalize();
				vector14 *= num75;
				float num77 = vector14.X;
				float y = vector14.Y;
				float speedX5 = num77;
				float speedY6 = y + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(vector13.X, vector13.Y, speedX5, speedY6, ModContent.ProjectileType<CursedBoltPro2>(), damage / 2, 0f, Main.myPlayer, 0f, 0f);
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(39, 300, false);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, base.projectile.position);
			for (int index = 0; index < 20; index++)
			{
				int index2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}
	}
}
