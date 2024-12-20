using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class IceTentacle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icy Grasp");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
			base.projectile.MaxUpdates = 3;
			base.projectile.magic = true;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			if (base.projectile.velocity.X != base.projectile.velocity.X)
			{
				if (Math.Abs(base.projectile.velocity.X) < 1f)
				{
					base.projectile.velocity.X = -base.projectile.velocity.X;
				}
				else
				{
					base.projectile.Kill();
				}
			}
			if (base.projectile.velocity.Y != base.projectile.velocity.Y)
			{
				if (Math.Abs(base.projectile.velocity.Y) < 1f)
				{
					base.projectile.velocity.Y = -base.projectile.velocity.Y;
				}
				else
				{
					base.projectile.Kill();
				}
			}
			Vector2 center10 = base.projectile.Center;
			base.projectile.scale = 1f - base.projectile.localAI[0];
			base.projectile.width = (int)(20f * base.projectile.scale);
			base.projectile.height = base.projectile.width;
			base.projectile.position.X = center10.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = center10.Y - (float)(base.projectile.height / 2);
			if ((double)base.projectile.localAI[0] < 0.1)
			{
				base.projectile.localAI[0] += 0.01f;
			}
			else
			{
				base.projectile.localAI[0] += 0.025f;
			}
			if (base.projectile.localAI[0] >= 0.95f)
			{
				base.projectile.Kill();
			}
			base.projectile.velocity.X = base.projectile.velocity.X + base.projectile.ai[0] * 2f;
			base.projectile.velocity.Y = base.projectile.velocity.Y + base.projectile.ai[1] * 2f;
			if (base.projectile.velocity.Length() > 16f)
			{
				base.projectile.velocity.Normalize();
				base.projectile.velocity *= 16f;
			}
			base.projectile.ai[0] *= 1.05f;
			base.projectile.ai[1] *= 1.05f;
			if (base.projectile.scale < 1f)
			{
				int num897 = 0;
				while ((float)num897 < base.projectile.scale * 10f)
				{
					int num898 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 135, base.projectile.velocity.X, base.projectile.velocity.Y, 100, default(Color), 1f);
					Main.dust[num898].position = (Main.dust[num898].position + base.projectile.Center) / 2f;
					Main.dust[num898].noGravity = true;
					Main.dust[num898].velocity *= 0.1f;
					Main.dust[num898].velocity -= base.projectile.velocity * (1.3f - base.projectile.scale);
					Main.dust[num898].scale += base.projectile.scale * 0.75f;
					num897++;
				}
				return;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(44, 120, false);
		}
	}
}
