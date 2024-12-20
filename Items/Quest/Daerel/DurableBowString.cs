using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Daerel
{
	public class DurableBowString : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Durable Bow String");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 12);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddIngredient(3308, 3);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
