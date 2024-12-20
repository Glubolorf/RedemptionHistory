using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class UkkoDancingLights : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dancing Light");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 20;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 254;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				base.projectile.alpha -= 8;
			}
			else
			{
				base.projectile.localAI[0] += 1f;
			}
			if (base.projectile.localAI[0] >= 60f)
			{
				base.projectile.alpha += 6;
			}
			if (base.projectile.alpha < 100)
			{
				base.projectile.localAI[0] = 1f;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 1f / 255f, (float)(255 - base.projectile.alpha) * 0.9f / 255f, (float)(255 - base.projectile.alpha) * 0.6f / 255f);
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (base.projectile.alpha < 110 && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(ModContent.BuffType<HolyFireDebuff>(), 30, true);
				}
			}
			if (base.projectile.alpha >= 255 && base.projectile.localAI[0] != 0f)
			{
				base.projectile.Kill();
			}
		}

		private Player clearCheck;
	}
}
