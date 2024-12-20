﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class KingsOakShot2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Leaf");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(206);
			this.aiType = 206;
			base.projectile.magic = false;
			base.projectile.melee = false;
			base.projectile.ranged = false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
		}
	}
}
