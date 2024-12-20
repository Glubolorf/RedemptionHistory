using System;
using Redemption.Tiles.Furniture.Ship;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class ShipGlass : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Reinforced Glass");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable]");
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
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ShipGlassTile>();
		}
	}
}
