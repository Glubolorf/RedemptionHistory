﻿using System;
using Redemption.Tiles.Furniture.Ship;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Ship
{
	public class SlayerChair : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Big Chair for a Big Robot");
			base.Tooltip.SetDefault("If you have this you're either cheating or you broke something\n[c/ff0000:Unbreakable (500% Pickaxe Power after 3rd Overlord)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = (base.item.value = Item.sellPrice(0, 0, 0, 0));
			base.item.rare = 9;
			base.item.createTile = ModContent.TileType<SlayerChairTile>();
			base.item.placeStyle = 0;
		}
	}
}
