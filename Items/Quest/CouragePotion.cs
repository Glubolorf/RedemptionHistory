using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class CouragePotion : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Elixir of Courage");
			base.Tooltip.SetDefault("50% courage, 50% alcohol");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 42;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
			base.item.consumable = true;
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.noUseGraphic = true;
			base.item.useTurn = true;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.buffType = 25;
			base.item.buffTime = 30000;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(353, 25);
			modRecipe.AddIngredient(2324, 2);
			modRecipe.AddIngredient(2359, 2);
			modRecipe.AddIngredient(501, 10);
			modRecipe.AddIngredient(548, 15);
			modRecipe.AddIngredient(549, 5);
			modRecipe.AddIngredient(547, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
