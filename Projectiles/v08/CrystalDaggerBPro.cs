using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CrystalDaggerBPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Dagger");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 36;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.thrown = true;
			base.projectile.penetrate = 1;
		}

		public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 68, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 6, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 6, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 14f, base.projectile.position.Y + 26f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 90, 6, 1f, base.projectile.owner, 0f, 1f);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return new bool?(projHitbox.Intersects(targetHitbox));
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(27, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Vector2 usePos = base.projectile.position;
			Vector2 rotVector = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			usePos += rotVector * 16f;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(usePos, base.projectile.width, base.projectile.height, 68, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
		}
	}
}
