using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class InvisibleCloak : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Invisible Cloak");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 40;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(532, 1);
			modRecipe.AddIngredient(297, 10);
			modRecipe.AddIngredient(1272, 10);
			modRecipe.AddIngredient(520, 5);
			modRecipe.AddIngredient(521, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
