using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Keycard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Keycard");
			base.Tooltip.SetDefault("'Unlocks Lab Chests'\nOnly one is needed");
		}

		public override void SetDefaults()
		{
			base.item.width = 44;
			base.item.height = 30;
			base.item.rare = 9;
			base.item.maxStack = 1;
			base.item.value = 0;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3467, 7);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
