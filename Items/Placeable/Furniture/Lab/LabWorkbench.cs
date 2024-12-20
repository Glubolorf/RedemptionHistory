using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class LabWorkbench : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Work Bench");
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
			base.item.createTile = ModContent.TileType<LabWorkbenchTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 10);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
