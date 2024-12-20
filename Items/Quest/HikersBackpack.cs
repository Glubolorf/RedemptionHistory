using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class HikersBackpack : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hiker's Backpack");
			base.Tooltip.SetDefault("'Everything an adventurer needs, all in one bag!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 36;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "RopeHook", 1);
			modRecipe.AddIngredient(3506, 1);
			modRecipe.AddIngredient(2289, 1);
			modRecipe.AddIngredient(279, 50);
			modRecipe.AddIngredient(225, 6);
			modRecipe.AddIngredient(259, 30);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "RopeHook", 1);
			modRecipe2.AddIngredient(3500, 1);
			modRecipe2.AddIngredient(2289, 1);
			modRecipe2.AddIngredient(279, 50);
			modRecipe2.AddIngredient(225, 6);
			modRecipe2.AddIngredient(259, 30);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
