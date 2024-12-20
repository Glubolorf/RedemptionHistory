using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Daerel
{
	public class PatchingKit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Patching Kit");
			base.Tooltip.SetDefault("'For fixing clothes and cloaks and cloths and socks'");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 26;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(362, 2);
			modRecipe.AddIngredient(225, 10);
			modRecipe.AddIngredient(21, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(362, 2);
			modRecipe2.AddIngredient(225, 10);
			modRecipe2.AddIngredient(705, 1);
			modRecipe2.AddTile(18);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
