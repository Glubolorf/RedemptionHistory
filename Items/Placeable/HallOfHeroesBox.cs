using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class HallOfHeroesBox : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Music Box (Hall of Heroes)");
			base.Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			base.item.useStyle = 1;
			base.item.useTurn = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.autoReuse = true;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<HallOfHeroesBoxTile>();
			base.item.width = 32;
			base.item.height = 24;
			base.item.rare = 4;
			base.item.value = 100000;
			base.item.accessory = true;
		}
	}
}
