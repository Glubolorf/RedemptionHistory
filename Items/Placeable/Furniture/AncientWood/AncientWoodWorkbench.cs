﻿using System;
using Redemption.Tiles.Furniture.AncientWood;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.AncientWood
{
	public class AncientWoodWorkbench : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Wood Work Bench");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 150;
			base.item.createTile = ModContent.TileType<AncientWoodWorkbenchTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientWood", 10);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
