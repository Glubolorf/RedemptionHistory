using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CursedThornsF : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorn Piece");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine tooltipLine in list)
			{
				if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
				{
					tooltipLine.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}
	}
}
