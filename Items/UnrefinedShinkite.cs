using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class UnrefinedShinkite : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Unrefined Shinkite");
			base.Tooltip.SetDefault("'A jagged chunk of cursed crimson'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<ShinkiteTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}
	}
}
