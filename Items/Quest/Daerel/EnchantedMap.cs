using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Daerel
{
	public class EnchantedMap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Enchanted Map");
			base.Tooltip.SetDefault("For all your exploring needs!");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 42;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(893, 1);
			modRecipe.AddIngredient(18, 1);
			modRecipe.AddIngredient(393, 1);
			modRecipe.AddIngredient(549, 15);
			modRecipe.AddIngredient(548, 5);
			modRecipe.AddIngredient(547, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
