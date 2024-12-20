using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XenomiteEyePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(84);
			this.aiType = 84;
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.magic = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 200;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(1) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(999) == 0 || (Main.expertMode && Main.rand.Next(99) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
		}
	}
}
