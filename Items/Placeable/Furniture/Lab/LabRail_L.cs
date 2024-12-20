using System;
using Redemption.Tiles.Furniture.Lab;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Lab
{
	public class LabRail_L : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Railing (Left)");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.value = 200;
			base.item.rare = 6;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<LabRailTile_L>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 1);
			modRecipe.AddIngredient(170, 1);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
