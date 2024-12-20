using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSSurge2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Core Surge");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 80;
			base.projectile.height = 80;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = false;
			base.projectile.timeLeft = 20;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (!this.clearCheck.noKnockback && base.projectile.Hitbox.Intersects(this.clearCheck.Hitbox))
				{
					this.clearCheck.velocity.X = base.projectile.velocity.X;
					this.clearCheck.velocity.Y = base.projectile.velocity.Y;
				}
			}
		}

		private Player clearCheck;
	}
}
