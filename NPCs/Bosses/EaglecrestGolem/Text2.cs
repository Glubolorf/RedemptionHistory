using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class Text2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Text");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 788;
			base.projectile.height = 40;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 230;
		}

		public override void AI()
		{
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			Player player = Main.player[base.projectile.owner];
			base.projectile.position.X = player.Center.X - 394f + 60f;
			base.projectile.position.Y = player.Center.Y - 20f - 200f;
			if (base.projectile.localAI[0] == 1f)
			{
				float[] localAI = base.projectile.localAI;
				int num = 1;
				float num2 = localAI[num] + 1f;
				localAI[num] = num2;
				if (num2 > 120f)
				{
					base.projectile.alpha += 10;
					if (base.projectile.alpha >= 255)
					{
						base.projectile.Kill();
						return;
					}
				}
			}
			else
			{
				base.projectile.alpha -= 10;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.localAI[0] = 1f;
				}
			}
		}
	}
}
