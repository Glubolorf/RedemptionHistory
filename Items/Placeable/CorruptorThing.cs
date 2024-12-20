using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class CorruptorThing : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Corruptor");
			base.Tooltip.SetDefault("Used to corrupt Xenomite, Starlite, AI Chips, etc.\nFound in the Abandoned Lab");
		}

		public override void SetDefaults()
		{
			base.item.width = 54;
			base.item.height = 64;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = (base.item.value = Item.sellPrice(0, 10, 0, 0));
			base.item.rare = 10;
			base.item.createTile = ModContent.TileType<CorruptorTile>();
			base.item.placeStyle = 0;
		}
	}
}
