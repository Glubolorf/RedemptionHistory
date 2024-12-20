using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class LabReceptionDesk : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Laboratory Reception Desk");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 1400;
			base.item.rare = 6;
			base.item.createTile = base.mod.TileType("LabReceptionDeskTile");
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 18);
			modRecipe.AddIngredient(null, "Computer", 1);
			modRecipe.AddRecipeGroup("Redemption:Plating", 3);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
