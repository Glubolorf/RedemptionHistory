using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class RubySkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ruby Skull");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 24;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1274, 1);
			modRecipe.AddIngredient(178, 2);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
