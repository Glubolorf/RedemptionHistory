﻿using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class SewerHole : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sewer Hole");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<SewerHoleTile>();
			base.item.placeStyle = 0;
		}
	}
}
