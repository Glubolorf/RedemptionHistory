using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class UraniumRaygunRingPro : ModProjectile
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/v08/" + base.GetType().Name + "_Glow");
				UraniumRaygunRingPro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Uranium Ring");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 62;
			base.projectile.height = 62;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.ranged = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 4;
			base.projectile.timeLeft = 300;
			base.projectile.alpha = 50;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
			base.projectile.glowMask = UraniumRaygunRingPro.customGlowMask;
		}

		public override void AI()
		{
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			if (this.offsetLeft)
			{
				base.projectile.scale -= 0.03f;
				if (base.projectile.scale <= 0.7f)
				{
					base.projectile.scale = 0.7f;
					this.offsetLeft = false;
					return;
				}
			}
			else
			{
				base.projectile.scale += 0.03f;
				if (base.projectile.scale >= 1.3f)
				{
					base.projectile.scale = 1.3f;
					this.offsetLeft = true;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 5;
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 74;
			int pieCut = 16;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}

		public static short customGlowMask;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
