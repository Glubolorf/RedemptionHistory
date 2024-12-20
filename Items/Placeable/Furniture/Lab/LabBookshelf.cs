using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class LabBookshelf : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Server Cabinet");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 32;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 7000;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<LabBookshelfTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 12);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddRecipeGroup("Redemption:Capacitators", 2);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
