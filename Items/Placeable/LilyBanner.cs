using System;
using Redemption.Tiles;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class LilyBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lily Rune Banner");
			base.Tooltip.SetDefault("'A powerful banner, proof of a true Druid'\n30% druid damage when nearby\n40% reduced damage for all other types when nearby");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 54;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.rare = 7;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 20000;
			base.item.createTile = ModContent.TileType<LilyBannerTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(225, 25);
			modRecipe.AddIngredient(null, "SoulOfBloom", 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
