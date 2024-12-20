using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class ArmorHKLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holy Knight's Greaves");
			base.Tooltip.SetDefault("'Great for impersonating demigods!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 14;
			base.item.rare = 8;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.vanity = true;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName")).overrideColor = new Color?(new Color(255, 213, 0));
			}
		}
	}
}
