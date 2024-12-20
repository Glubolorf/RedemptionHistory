using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class Vent1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Large Vent");
		}

		public override void SetDefaults()
		{
			base.item.width = 48;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<VentTile1>();
			base.item.placeStyle = 0;
		}
	}
}
