using System;
using Redemption.Tiles.Plants;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Plants
{
	public class AnglonicMysticBlossom : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anglonic Mystic Blossom");
			base.Tooltip.SetDefault("'An exceptionally rare flower with an eternal lifetime. Only grows in very specific locations in Anglon.'");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 38;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 7, 5, 0);
			base.item.rare = 5;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<AnglonicMysticBlossomTile>();
		}
	}
}
