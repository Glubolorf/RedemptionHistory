using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class VeilFX : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Veil");
			Main.projFrames[base.projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 28;
			base.projectile.height = 18;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
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
			base.projectile.localAI[0] += 1f;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.04f;
			base.projectile.rotation += 0.05f;
			if (this.changeSway)
			{
				this.vectorOffset -= 0.01f;
				if (this.vectorOffset <= -1f)
				{
					this.vectorOffset = -1f;
					this.changeSway = false;
				}
			}
			else
			{
				this.vectorOffset += 0.01f;
				if (this.vectorOffset >= 1f)
				{
					this.vectorOffset = 1f;
					this.changeSway = true;
				}
			}
			float velRot = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
			base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(0f, base.projectile.velocity.Length()), velRot + this.vectorOffset * 0.5f);
		}

		public float vectorOffset;

		public bool changeSway;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
