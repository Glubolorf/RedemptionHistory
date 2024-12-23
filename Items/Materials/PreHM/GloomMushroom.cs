﻿using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class GloomMushroom : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Mushroom");
			base.Tooltip.SetDefault("Used to craft Gloom Druid equipment");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 0, 0, 50);
			base.item.rare = 3;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<GloomShroomTile>();
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}
	}
}
