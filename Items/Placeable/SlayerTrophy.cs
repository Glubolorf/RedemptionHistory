using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class SlayerTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer Trophy");
			base.Tooltip.SetDefault("'It's King Slayer III's broken rifle'");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 1;
			base.item.createTile = base.mod.TileType("SlayerTrophyTile");
			base.item.placeStyle = 0;
		}
	}
}
