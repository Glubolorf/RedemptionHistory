﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Banners.v08
{
	public class VirusSpiderBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Virus Spider Banner");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 46;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 0, 10, 0);
			base.item.createTile = base.mod.TileType("VirusSpiderBannerTile");
			base.item.placeStyle = 0;
		}
	}
}
