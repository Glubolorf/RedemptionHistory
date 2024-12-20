using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class ShootingStarArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/" + base.GetType().Name + "_Glow");
				ShootingStarArrow.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Shooting Star Arrow");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = 1;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 600;
			base.projectile.alpha = 0;
			this.aiType = 1;
			base.projectile.glowMask = ShootingStarArrow.customGlowMask;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 14f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 14f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 14f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			}
		}

		public static short customGlowMask;
	}
}
