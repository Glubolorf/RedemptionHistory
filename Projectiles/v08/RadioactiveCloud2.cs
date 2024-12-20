using System;
using Redemption.Buffs.Wasteland;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class RadioactiveCloud2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Cloud");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 278;
			base.projectile.height = 124;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 300;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BileDebuff>(), 90, false);
		}

		public override void AI()
		{
			if (base.projectile.ai[0] == 0f)
			{
				int num = Main.rand.Next(2);
				if (num == 0)
				{
					base.projectile.frame = 0;
				}
				if (num == 1)
				{
					base.projectile.frame = 1;
				}
				base.projectile.ai[0] = 1f;
			}
			if (base.projectile.alpha > 100)
			{
				base.projectile.alpha -= 2;
			}
			if (base.projectile.timeLeft < 60)
			{
				base.projectile.alpha += 2;
			}
		}
	}
}
