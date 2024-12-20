using System;
using Redemption.Tiles.LabDeco;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabTestTubes : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Test Tubes");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 500;
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<LabTubesTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 1);
			modRecipe.AddIngredient(170, 2);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
