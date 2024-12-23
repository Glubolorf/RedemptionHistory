﻿using System;
using Redemption.Items.Accessories.PostML;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class LargeSeedPouch : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Large Seed Pouch");
			base.Tooltip.SetDefault("Has a chance to throw an extra seed");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 0, 80, 0);
			base.item.rare = 2;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<DruidsCharm>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(309, 2);
			modRecipe.AddIngredient(307, 2);
			modRecipe.AddIngredient(310, 2);
			modRecipe.AddIngredient(312, 2);
			modRecipe.AddIngredient(308, 2);
			modRecipe.AddIngredient(2357, 2);
			modRecipe.AddIngredient(311, 2);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).extraSeed = true;
		}
	}
}
