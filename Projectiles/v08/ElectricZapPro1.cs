using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class ElectricZapPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Electric Bolt");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(435);
			this.aiType = 435;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 60f)
			{
				base.projectile.alpha -= 5;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 1f / 255f);
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(144, 180, true);
		}
	}
}
