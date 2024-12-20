using System;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class AncientGoldCoin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Antique Dorul");
			base.Tooltip.SetDefault("'Ancient gold coins used in the olden days of Gathuram'\nCan be given to a certain Undead as currency");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 6));
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 1, 0);
			base.item.rare = -1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.createTile = ModContent.TileType<AncientGoldCoinPileTile>();
		}
	}
}
