using System;
using Redemption.Tiles.Furniture.Misc;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class LotusBanner : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lotus Rune Banner");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 52;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.rare = 7;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 2000;
			base.item.createTile = ModContent.TileType<LotusBannerTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(225, 25);
			modRecipe.AddIngredient(9, 20);
			modRecipe.AddIngredient(null, "SoulOfBloom", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
