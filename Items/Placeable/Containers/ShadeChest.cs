using System;
using Redemption.Tiles.Containers;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Containers
{
	public class ShadeChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Chest");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = ModContent.TileType<ShadeChestTile>();
			base.item.placeStyle = 0;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
