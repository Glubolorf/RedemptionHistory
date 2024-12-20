using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class TearOfPainBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tear of Pain");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.penetrate = 2;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.8f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 30; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.penetrate--;
			if (base.projectile.penetrate <= 0)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(-base.projectile.velocity.X + (float)Main.rand.Next(-2, 2), -base.projectile.velocity.Y + (float)Main.rand.Next(-2, 2)), ModContent.ProjectileType<TearsOfPain>(), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(-base.projectile.velocity.X + (float)Main.rand.Next(-2, 2), -base.projectile.velocity.Y + (float)Main.rand.Next(-2, 2)), ModContent.ProjectileType<TearsOfPain>(), base.projectile.damage / 2, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
				base.projectile.Kill();
			}
			else
			{
				if (base.projectile.velocity.X != oldVelocity.X)
				{
					base.projectile.velocity.X = -oldVelocity.X;
				}
				if (base.projectile.velocity.Y != oldVelocity.Y)
				{
					base.projectile.velocity.Y = -oldVelocity.Y;
				}
				base.projectile.velocity *= 0.75f;
				Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			}
			return false;
		}
	}
}
