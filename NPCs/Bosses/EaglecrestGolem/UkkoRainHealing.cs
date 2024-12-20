using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoRainHealing : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Healing Rain");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 2;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.5f;
			base.projectile.alpha -= 10;
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(103, 600, false);
					this.clearCheck.statLife++;
					this.clearCheck.HealEffect(1, true);
					base.projectile.Kill();
				}
			}
			for (int p2 = 0; p2 < 200; p2++)
			{
				this.clearCheck2 = Main.npc[p2];
				if (!this.clearCheck2.immortal && !this.clearCheck2.dontTakeDamage && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck2.position, this.clearCheck2.Size) && this.clearCheck2.life < this.clearCheck2.lifeMax - 1000)
				{
					this.clearCheck2.AddBuff(103, 600, false);
					this.clearCheck2.life += 1000;
					this.clearCheck2.HealEffect(1000, true);
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 31, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[dustIndex].velocity *= 1.2f;
		}

		private Player clearCheck;

		private NPC clearCheck2;
	}
}
