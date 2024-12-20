using System;
using Redemption.Tiles.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class RadioactiveIce : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Ice");
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
			base.item.value = 0;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<RadioactiveIceTile>();
		}
	}
}
