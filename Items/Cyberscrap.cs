using System;
using Redemption.Tiles.SlayerShip;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Cyberscrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyberscrap");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 28;
			base.item.maxStack = 999;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<JunkMetalTile>();
		}
	}
}
