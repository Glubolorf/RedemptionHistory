using System;
using Redemption.Tiles.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class SoulCandles : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul Candles");
			base.Tooltip.SetDefault("Creates a small aura that instantly kills any soulless enemies that enter\nLife regen is disabled in the aura");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 42;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.rare = 11;
			base.item.consumable = true;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.createTile = ModContent.TileType<SoulCandlesTile>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
