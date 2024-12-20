﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class DragonLeadBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon-Lead Bar");
			base.Tooltip.SetDefault("'A heated ingot made from old dragon bone and lead...'\nBurns the players hands when held");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 26;
			base.item.maxStack = 99;
			base.item.value = Item.sellPrice(0, 0, 25, 0);
			base.item.rare = 4;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadChunk", 5);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 2);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("BurntHands"), 60, true);
		}
	}
}