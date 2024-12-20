﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class Mk1MicrobotFactory : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-1 Microbot Factory");
			base.Tooltip.SetDefault("Right-clicking will construct a Mk-1 Microbot");
		}

		public override void SetDefaults()
		{
			base.item.width = 42;
			base.item.height = 40;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 5;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.createTile = base.mod.TileType("Mk1MicrobotFactoryTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(21, 20);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk1Capacitator", 2);
			modRecipe.AddIngredient(null, "Mk1Plating", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(705, 20);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk1Capacitator", 2);
			modRecipe.AddIngredient(null, "Mk1Plating", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}