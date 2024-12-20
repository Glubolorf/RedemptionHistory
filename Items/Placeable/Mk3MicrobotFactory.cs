﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class Mk3MicrobotFactory : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-3 Microbot Factory");
			base.Tooltip.SetDefault("Right-clicking will construct a Mk-3 Microbot");
		}

		public override void SetDefaults()
		{
			base.item.width = 60;
			base.item.height = 42;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 7;
			base.item.value = Item.buyPrice(1, 20, 0, 0);
			base.item.createTile = base.mod.TileType("Mk3MicrobotFactoryTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(19, 40);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk3Capacitator", 2);
			modRecipe.AddIngredient(null, "Mk3Plating", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(706, 40);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk3Capacitator", 2);
			modRecipe.AddIngredient(null, "Mk3Plating", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}