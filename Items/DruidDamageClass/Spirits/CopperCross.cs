﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class CopperCross : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Copper Cross");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n5% increased druidic damage\n[c/bdffff:Spirit Level +1]");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 38;
			base.item.value = Item.sellPrice(0, 0, 15, 0);
			base.item.rare = 1;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(20, 5);
			modRecipe.AddIngredient(965, 10);
			modRecipe.AddIngredient(null, "SmallLostSoul", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("TinCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SpiritualRelic"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SacredCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SpiritualGuide"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("LastBurden"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritLevel++;
		}
	}
}
