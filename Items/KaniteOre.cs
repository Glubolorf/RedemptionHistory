using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class KaniteOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Ore");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 2, 0);
			base.item.rare = 0;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<KaniteOreTile>();
		}
	}
}
