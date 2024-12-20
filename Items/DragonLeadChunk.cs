using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class DragonLeadChunk : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon-Lead Chunk");
			base.Tooltip.SetDefault("'A chunk of heated dragon bones and lead'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 4;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<DragonLeadOreTile>();
		}
	}
}
