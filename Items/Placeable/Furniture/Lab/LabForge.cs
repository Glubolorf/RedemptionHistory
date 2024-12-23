﻿using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class LabForge : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Smelter");
			base.Tooltip.SetDefault("Used to melt scraps of metal\nFound in the Abandoned Lab");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 36;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.createTile = ModContent.TileType<LabForgeTile>();
			base.item.placeStyle = 0;
		}
	}
}
