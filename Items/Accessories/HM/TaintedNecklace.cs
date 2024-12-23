﻿using System;
using Redemption.Items.Materials.HM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class TaintedNecklace : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tainted Necklace");
			base.Tooltip.SetDefault("'Even in it's darkest moment, the corruption refuses to stand still.'\nUpon being attacked, you unleash an storm of hateful spirits around you");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 36;
			base.item.value = Item.sellPrice(0, 4, 0, 0);
			base.item.rare = 6;
			base.item.accessory = true;
			base.item.defense = 6;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(ModContent.ItemType<SapphireBar>(), 5);
			modRecipe.AddIngredient(1225, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).taintedNecklace = true;
		}
	}
}
