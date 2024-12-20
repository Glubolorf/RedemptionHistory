using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class BoneChickenPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Chicken");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.friendly = true;
			base.projectile.timeLeft = 180;
			base.projectile.penetrate = 1;
		}

		public override bool PreAI()
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
				if (num >= 6)
				{
					base.projectile.frame = 0;
				}
			}
			if (base.projectile.velocity.X < 0f)
			{
				base.projectile.spriteDirection = 1;
			}
			else
			{
				base.projectile.spriteDirection = -1;
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.4f;
			Projectile projectile4 = base.projectile;
			projectile4.velocity.X = projectile4.velocity.X * 1.008f;
			base.projectile.velocity.X = MathHelper.Clamp(base.projectile.velocity.X, -10f, 10f);
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCHit2.WithVolume(0.3f), base.projectile.position);
			for (int i = 0; i < 2; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.9f;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (oldVelocity.X != base.projectile.velocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			return false;
		}
	}
}
