using System;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.HM
{
	public class BlackGloop : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Black Gloop");
			base.Tooltip.SetDefault("'Sticky...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = 0;
			base.item.rare = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(23, 80);
			modRecipe.AddIngredient(1508, 2);
			modRecipe.AddTile(null, "CorruptorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
