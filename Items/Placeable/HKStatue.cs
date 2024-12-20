using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class HKStatue : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statue of the Demigod");
			base.Tooltip.SetDefault("[c/ffea9b:He watches...]\n[c/ffc300:Legendary]");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 44;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 8;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(5, 0, 0, 0);
			base.item.createTile = base.mod.TileType("HKStatueTile");
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(255, 195, 0));
			}
		}
	}
}
