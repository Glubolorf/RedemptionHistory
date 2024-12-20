using System;
using Redemption.Tiles.SlayerShip;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class HolochestItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holochest");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 500;
			base.item.createTile = ModContent.TileType<HolochestTile>();
		}
	}
}
