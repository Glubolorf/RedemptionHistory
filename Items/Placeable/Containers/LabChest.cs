using System;
using Redemption.Tiles.Containers;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Containers
{
	public class LabChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lab Chest");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 5000;
			base.item.createTile = ModContent.TileType<LabChestTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 8);
			modRecipe.AddRecipeGroup("Redemption:Plating", 2);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
