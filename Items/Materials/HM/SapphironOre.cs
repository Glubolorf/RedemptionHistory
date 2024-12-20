using System;
using Redemption.Tiles.Ores;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class SapphironOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphiron Ore");
			base.Tooltip.SetDefault("'Sky Sapphire'");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 8, 0);
			base.item.rare = 6;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<SapphironOreTile>();
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}
	}
}
