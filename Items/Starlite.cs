using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Starlite : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starlite");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 26;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 7, 50);
			base.item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1106, 2);
			modRecipe.AddIngredient(182, 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 10);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(366, 2);
			modRecipe2.AddIngredient(182, 1);
			modRecipe2.AddTile(null, "XenoForgeTile");
			modRecipe2.SetResult(this, 10);
			modRecipe2.AddRecipe();
			ModRecipe modRecipe3 = new ModRecipe(base.mod);
			modRecipe3.AddIngredient(null, "StarliteBar", 1);
			modRecipe3.AddTile(null, "XenoTank1");
			modRecipe3.SetResult(this, 2);
			modRecipe3.AddRecipe();
		}
	}
}
