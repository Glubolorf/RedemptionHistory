using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Misc
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class GodArmorLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kamite Greaves");
			base.Tooltip.SetDefault("'Blessed with Oysus' power, this greaves hold overwhelming energy...'\nSlightly increased max fall speed\nGreatly increased movement speed\nIncreased jump height\n+500 max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 14;
			base.item.value = Item.sellPrice(1, 75, 0, 0);
			base.item.rare = 9;
			base.item.defense = 84;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxFallSpeed += 2f;
			player.moveSpeed *= 100f;
			player.jumpBoost = true;
			player.statLifeMax2 += 500;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName")).overrideColor = new Color?(new Color(208, 255, 255));
			}
		}
	}
}
