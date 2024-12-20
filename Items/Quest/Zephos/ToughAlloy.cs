using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Zephos
{
	public class ToughAlloy : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tough Alloy");
			base.Tooltip.SetDefault("'Looks like an ingot sandwich'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 36;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1552, 3);
			modRecipe.AddIngredient(3261, 3);
			modRecipe.AddIngredient(null, "PureIron", 3);
			modRecipe.AddIngredient(null, "DragonLeadBar", 3);
			modRecipe.AddIngredient(1225, 3);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 3);
			modRecipe.AddTile(133);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
