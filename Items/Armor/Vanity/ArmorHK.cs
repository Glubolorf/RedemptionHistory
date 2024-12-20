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
		1
	})]
	internal class ArmorHK : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holy Knight's Chestplate");
			base.Tooltip.SetDefault("'Great for impersonating demigods!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 28;
			base.item.rare = 8;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = false;
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
