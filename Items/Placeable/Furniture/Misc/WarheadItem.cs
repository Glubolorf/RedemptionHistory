using System;
using Redemption.Tiles.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class WarheadItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nuclear Warhead");
			base.Tooltip.SetDefault("Right-click the placed warhead to view the side panel\nDetonation will create a wasteland");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 34;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 7;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.createTile = ModContent.TileType<WarheadPlaced>();
		}
	}
}
