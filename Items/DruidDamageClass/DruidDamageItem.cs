using System;
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

		public virtual void SecondarySetDefaults()
		{
		}

		public sealed override void SetDefaults()
		{
			this.SafeSetDefaults();
			this.SecondarySetDefaults();
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
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("ItemName"));
			if (tooltipLocation != -1 && !RedeConfigClient.Instance.NoDruidClassTag)
			{
				tooltips.Insert(tooltipLocation + 1, new TooltipLine(base.mod, "IsDruid", "[c/91dc16:---Druid Class---]"));
			}
		}
	}
}
