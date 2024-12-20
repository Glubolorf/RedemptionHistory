using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class DualcastBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Static Dualcast");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 32;
			base.projectile.height = 32;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 400;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 4)
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
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.8f / 255f, (float)(255 - base.projectile.alpha) * 0.6f / 255f);
			if (this.originalVelocity == Vector2.Zero)
			{
				this.originalVelocity = base.projectile.velocity;
			}
			if (base.projectile.ai[0] == 0f)
			{
				if (this.offsetLeft)
				{
					this.vectorOffset -= 0.06f;
					if (this.vectorOffset <= -2.4f)
					{
						this.vectorOffset = -2.4f;
						this.offsetLeft = false;
					}
				}
				else
				{
					this.vectorOffset += 0.06f;
					if (this.vectorOffset >= 2.4f)
					{
						this.vectorOffset = 2.4f;
						this.offsetLeft = true;
					}
				}
			}
			else if (this.offsetLeft)
			{
				this.vectorOffset += 0.06f;
				if (this.vectorOffset >= 2.4f)
				{
					this.vectorOffset = 2.4f;
					this.offsetLeft = false;
				}
			}
			else
			{
				this.vectorOffset -= 0.06f;
				if (this.vectorOffset <= -2.4f)
				{
					this.vectorOffset = -2.4f;
					this.offsetLeft = true;
				}
			}
			float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
			base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(base.projectile.velocity.Length(), 0f), velRot + this.vectorOffset * 0.5f);
			base.projectile.rotation = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + base.projectile.velocity) + 1.57f - 0.7853982f;
			base.projectile.spriteDirection = 1;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(144, target.HasBuff(103) ? 320 : 160, true);
			}
			target.AddBuff(base.mod.BuffType("StunnedDebuff"), 60, true);
		}

		public float vectorOffset;

		public bool offsetLeft;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
