﻿using System;
using Redemption.Tiles.Furniture.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Misc
{
	public class Mk2MicrobotFactory : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mk-2 Microbot Factory");
			base.Tooltip.SetDefault("Right-clicking will construct a Mk-2 Microbot\nShoots rapid-fire lasers that pierce");
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.noMelee = true;
			base.item.summon = true;
			base.item.width = 42;
			base.item.height = 40;
			base.item.maxStack = 1;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.rare = 6;
			base.item.value = Item.buyPrice(0, 80, 0, 0);
			base.item.createTile = ModContent.TileType<Mk2MicrobotFactoryTile>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(19, 20);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 2);
			modRecipe.AddIngredient(null, "Mk2Plating", 6);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(706, 20);
			modRecipe2.AddIngredient(null, "AIChip", 1);
			modRecipe2.AddIngredient(null, "Mk2Capacitator", 2);
			modRecipe2.AddIngredient(null, "Mk2Plating", 6);
			modRecipe2.AddTile(134);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
