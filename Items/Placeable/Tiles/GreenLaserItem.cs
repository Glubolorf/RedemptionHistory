using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
{
	public class GreenLaserItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Green Laser Block");
			base.Tooltip.SetDefault("[c/ff0000:Unbreakable (500% Pickaxe Power)]");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.value = Item.buyPrice(0, 0, 2, 0);
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<GreenLaserTile>();
		}
	}
}
