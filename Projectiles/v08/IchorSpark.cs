using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class IchorSpark : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ichor Spark");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			int DustID2 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 169, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1.2f);
			Main.dust[DustID2].noGravity = true;
			for (int p = 0; p < 200; p++)
			{
				this.clearCheck = Main.npc[p];
				if (!this.clearCheck.immortal && !this.clearCheck.dontTakeDamage && !this.clearCheck.friendly && this.clearCheck.CanBeChasedBy(null, false) && base.projectile.Hitbox.Intersects(this.clearCheck.Hitbox))
				{
					this.clearCheck.AddBuff(69, 600, false);
					Main.PlaySound(this.clearCheck.HitSound, this.clearCheck.position);
					base.projectile.Kill();
				}
			}
		}

		private NPC clearCheck;
	}
}
