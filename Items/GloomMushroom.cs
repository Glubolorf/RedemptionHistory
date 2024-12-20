﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;

namespace Redemption.Items
{
	public class GloomMushroom : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Mushroom");
			base.Tooltip.SetDefault("Used to craft Gloom Druid equipment");
		}

		public override void SafeSetDefaults()
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
			base.item.createTile = base.mod.TileType("GloomShroomTile");
		}
	}
}
