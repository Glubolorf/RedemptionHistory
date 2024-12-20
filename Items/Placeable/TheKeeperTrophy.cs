﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class TheKeeperTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper's Heart");
			base.Tooltip.SetDefault("'A tainted heart...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 1;
			base.item.createTile = base.mod.TileType("TheKeeperTrophyTile");
			base.item.placeStyle = 0;
		}
	}
}
