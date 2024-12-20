using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class TitaniumSwordSheath : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Titanium Sword Sheath");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 32;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1198, 12);
			modRecipe.AddIngredient(259, 8);
			modRecipe.AddIngredient(255, 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
