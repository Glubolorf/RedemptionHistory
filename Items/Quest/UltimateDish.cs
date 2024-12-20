using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class UltimateDish : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Ultimate Dish");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 22;
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 14;
			base.item.useTime = 14;
			base.item.questItem = true;
			base.item.consumable = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
			base.item.buffType = 26;
			base.item.buffTime = 162000;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2426, 1);
			modRecipe.AddIngredient(2427, 1);
			modRecipe.AddIngredient(2267, 1);
			modRecipe.AddIngredient(2268, 1);
			modRecipe.AddTile(96);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
