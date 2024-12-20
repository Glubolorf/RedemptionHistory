using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class StarliteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starlite Bar");
			base.Tooltip.SetDefault("'Man-made metal that was made a long time ago, but recipe was lost for hundreds of years...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 10, 0);
			base.item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Starlite", 2);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
