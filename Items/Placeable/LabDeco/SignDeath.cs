using System;
using Redemption.Tiles.LabDeco;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.LabDeco
{
	public class SignDeath : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Death Sign");
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
			base.item.rare = 6;
			base.item.createTile = ModContent.TileType<SignDeathTile>();
			base.item.placeStyle = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LabPlating", 6);
			modRecipe.AddIngredient(1075, 4);
			modRecipe.AddIngredient(1097, 2);
			modRecipe.AddTile(null, "LabWorkbenchTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
