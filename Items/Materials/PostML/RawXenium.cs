using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PostML
{
	public class RawXenium : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Raw Xenium");
			base.Tooltip.SetDefault("Use a Xenium Refinery to craft Xenium Bars");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 20;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 15, 0);
			base.item.rare = 11;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XenomiteShard", 2);
			modRecipe.AddIngredient(null, "Starlite", 2);
			modRecipe.AddIngredient(3460, 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}
	}
}
