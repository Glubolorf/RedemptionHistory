﻿using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public abstract class DruidDamageItem : ModItem
	{
		public virtual void SafeSetDefaults()
		{
		}

		public sealed override void SetDefaults()
		{
			this.SafeSetDefaults();
			base.item.melee = false;
			base.item.ranged = false;
			base.item.magic = false;
			base.item.thrown = false;
			base.item.summon = false;
		}

		public override void GetWeaponDamage(Player player, ref int damage)
		{
			damage = (int)((float)damage * DruidDamagePlayer.ModPlayer(player).druidDamage + 5E-06f);
		}

		public override void GetWeaponKnockback(Player player, ref float knockback)
		{
			knockback += DruidDamagePlayer.ModPlayer(player).druidKnockback;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit += DruidDamagePlayer.ModPlayer(player).druidCrit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = Enumerable.FirstOrDefault<TooltipLine>(tooltips, (TooltipLine x) => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] array = tt.text.Split(new char[]
				{
					' '
				});
				string damageValue = Enumerable.First<string>(array);
				string damageWord = Enumerable.Last<string>(array);
				tt.text = damageValue + " druidic " + damageWord;
			}
		}
	}
}
