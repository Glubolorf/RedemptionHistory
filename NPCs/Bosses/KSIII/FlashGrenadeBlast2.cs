using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class FlashGrenadeBlast2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/TransitionTex";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Flash");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 1000;
			base.projectile.height = 1000;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 300;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 10;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.localAI[0] = 1f;
				}
			}
			else if (base.projectile.timeLeft < 200)
			{
				base.projectile.alpha += 3;
			}
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (base.projectile.alpha < 150 && player.active && !player.dead && base.projectile.Distance(player.Center) < 400f)
				{
					player.AddBuff(163, 10, true);
				}
			}
		}
	}
}
