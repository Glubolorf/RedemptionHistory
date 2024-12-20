using System;
using Redemption.Tiles.Trophies;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Trophies
{
	public class SeedOfInfectionTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Seed of Infection Trophy");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 100;
			base.item.rare = 1;
			base.item.createTile = ModContent.TileType<SeedOfInfectionTrophyTile>();
			base.item.placeStyle = 0;
		}
	}
}
