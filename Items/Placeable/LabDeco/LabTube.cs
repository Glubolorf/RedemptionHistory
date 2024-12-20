﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabTube : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Tubes");
			base.Tooltip.SetDefault("'Filled with green sludge...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 2, 0);
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createTile = base.mod.TileType("LabTubeTile");
		}
	}
}
