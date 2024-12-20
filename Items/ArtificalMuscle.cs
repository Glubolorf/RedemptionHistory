using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ArtificalMuscle : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Artifical Muscle");
			base.Tooltip.SetDefault("'This is stronger than you...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.value = 20000;
			base.item.rare = 5;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 8);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
