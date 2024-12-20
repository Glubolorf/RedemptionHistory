using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class TerraBombaPart1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraforma Bomba Tail");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 18;
			base.item.maxStack = 3;
			base.item.value = Item.buyPrice(5, 0, 0, 0);
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
