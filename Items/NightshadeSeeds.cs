using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class NightshadeSeeds : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Seeds");
		}

		public override void SetDefaults()
		{
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.useStyle = 1;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.maxStack = 99;
			base.item.consumable = true;
			base.item.placeStyle = 0;
			base.item.width = 12;
			base.item.height = 14;
			base.item.value = 80;
			base.item.rare = 1;
			base.item.createTile = ModContent.TileType<NightshadeTile>();
		}
	}
}
