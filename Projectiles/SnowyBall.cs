﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SnowyBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chilling Sphere");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(359);
			this.aiType = 359;
			base.projectile.width = 16;
			base.projectile.height = 16;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(1) == 0))
			{
				target.AddBuff(44, 50, true);
			}
		}
	}
}
