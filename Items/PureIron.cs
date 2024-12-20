﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class PureIron : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pure-Iron Bar");
			base.Tooltip.SetDefault("'A freezing cold ingot of pure Iron...'\nMakes the player Chilled when held");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 25, 0);
			base.item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(22, 2);
			modRecipe.AddIngredient(null, "GathicCryoCrystal", 1);
			modRecipe.AddTile(null, "GathicCryoFurnaceTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(704, 2);
			modRecipe.AddIngredient(null, "GathicCryoCrystal", 1);
			modRecipe.AddTile(null, "GathicCryoFurnaceTile");
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(46, 60, true);
		}
	}
}