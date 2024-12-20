﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CorruptedStarliteBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Starlite Bar");
			base.Tooltip.SetDefault("'The star's life has ended...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = 5000;
			base.item.rare = 10;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "StarliteBar", 1);
			modRecipe.AddIngredient(1508, 1);
			modRecipe.AddTile(null, "CorruptorTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
