using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class ShinkiteBlood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blood Shinkite");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 32;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "UnrefinedShinkite", 3);
			modRecipe.AddIngredient(1329, 1);
			modRecipe.AddIngredient(1332, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
