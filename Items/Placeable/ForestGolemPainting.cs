using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class ForestGolemPainting : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("'Forest Golem'");
			base.Tooltip.SetDefault("'A painting of a hibernating Forest Golem...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 9;
			base.item.expert = true;
			base.item.createTile = base.mod.TileType("ForestGolemPaintingTile");
			base.item.placeStyle = 0;
		}
	}
}
