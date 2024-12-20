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
			if (++base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 3)
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
			Vector2 vector;
			vector..ctor(target.Center.X, target.Center.Y);
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num = 20f;
			float num2 = vector.Y;
			if (num2 > player.Center.Y - 200f)
			{
				num2 = player.Center.Y - 200f;
			}
			for (int i = 0; i < 6; i++)
			{
				vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * (float)player.direction, -600f);
				vector2.Y -= (float)(100 * i);
				Vector2 vector3 = vector - vector2;
				if (vector3.Y < 0f)
				{
					vector3.Y *= -1f;
				}
				if (vector3.Y < 20f)
				{
					vector3.Y = 20f;
				}
				vector3.Normalize();
				vector3 *= num;
				float x = vector3.X;
				float y = vector3.Y;
				float num3 = x;
				float num4 = y + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(vector2.X, vector2.Y, num3, num4, base.mod.ProjectileType("CursedBoltPro2"), damage / 2, 0f, Main.myPlayer, 0f, 0f);
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(39, 300, false);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, base.projectile.position);
			for (int i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
				Main.dust[num].velocity *= 1.1f;
				Main.dust[num].scale *= 0.99f;
			}
		}
	}
}
