﻿using System;
using Terraria.ModLoader;

namespace Redemption.Tiles.Furniture.Shade
{
	public class ShadeHangedCell2 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Placeholder";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hanging Cage (With Echo)");
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
			base.item.createTile = ModContent.TileType<ShadeHangedCell2Tile>();
		}
	}
}