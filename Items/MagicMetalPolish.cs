using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MagicMetalPolish : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Magic Metal Polish");
			base.Tooltip.SetDefault("Makes things shiny with the power of magic!");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.maxStack = 1;
			base.item.value = 10000;
			base.item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 5);
			modRecipe.AddIngredient(549, 2);
			modRecipe.AddIngredient(548, 2);
			modRecipe.AddIngredient(547, 2);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
