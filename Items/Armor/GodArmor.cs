using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class GodArmor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kamite Breastplate");
			base.Tooltip.SetDefault("'Blessed with Oysus' power, this breastplate holds overwhelming energy...'\n50% increased damage (Melee/Ranged/Magic/Minion/Thrown)\n16% increased critical strike chance (Melee/Ranged/Magic/Minion/Thrown)\n50% damage reduction\n+500 max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 29;
			base.item.value = Item.sellPrice(2, 0, 0, 0);
			base.item.rare = 9;
			base.item.defense = 86;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.5f;
			player.magicDamage *= 1.5f;
			player.rangedDamage *= 1.5f;
			player.minionDamage *= 1.5f;
			player.thrownDamage *= 1.5f;
			player.magicCrit += 16;
			player.meleeCrit += 16;
			player.rangedCrit += 16;
			player.thrownCrit += 16;
			player.endurance *= 0.5f;
			player.statLifeMax2 += 500;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(208, 255, 255));
			}
		}
	}
}
