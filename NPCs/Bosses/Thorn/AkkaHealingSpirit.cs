using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class AkkaHealingSpirit : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Healing Spirit");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 400;
			base.projectile.height = 400;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 1f)
			{
				base.projectile.alpha += 6;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			else
			{
				base.projectile.alpha -= 20;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.localAI[0] = 1f;
				}
			}
			for (int p = 0; p < 200; p++)
			{
				this.clearCheck = Main.npc[p];
				if (!this.clearCheck.immortal && !this.clearCheck.dontTakeDamage && base.projectile.alpha < 200 && base.projectile.Hitbox.Intersects(this.clearCheck.Hitbox) && this.clearCheck.life <= this.clearCheck.lifeMax - 10)
				{
					this.clearCheck.life += 10;
					this.clearCheck.HealEffect(10, true);
					Dust dust = Dust.NewDustDirect(this.clearCheck.position, this.clearCheck.width, this.clearCheck.height, 99, 0f, 0f, 100, default(Color), 1f);
					dust.velocity = -this.clearCheck.DirectionTo(dust.position);
				}
			}
		}

		private NPC clearCheck;
	}
}
