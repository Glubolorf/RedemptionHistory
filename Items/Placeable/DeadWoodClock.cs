﻿using System;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class DeadWoodClock : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Clock");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 28;
			base.item.maxStack = 99;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.value = 500;
			base.item.createTile = base.mod.TileType("DeadWoodClockTile");
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(22, 3);
			modRecipe.AddIngredient(170, 6);
			modRecipe.AddIngredient(null, "DeadWood", 10);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(704, 3);
			modRecipe.AddIngredient(170, 6);
			modRecipe.AddIngredient(null, "DeadWood", 10);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}