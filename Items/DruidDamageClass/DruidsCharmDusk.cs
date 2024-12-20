﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidsCharmDusk : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid's Charm");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Dusk'\n10% increased druidic damage\nNegates fall damage\nGrants immunity to fire blocks\nThrows seedbags faster\nHas a chance to throw an extra seed\n75% decrease to all other damage types");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 52;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 6;
			base.item.accessory = true;
			base.item.defense = 2;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int num = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + num; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharmDawn"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharmMidnight"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharm"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSeedbags = true;
			redePlayer.extraSeed = true;
			player.noFallDmg = true;
			player.buffImmune[67] = true;
			player.magicDamage *= 0.25f;
			player.meleeDamage *= 0.25f;
			player.minionDamage *= 0.25f;
			player.rangedDamage *= 0.25f;
			player.thrownDamage *= 0.25f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LotusHorseshoe2", 1);
			modRecipe.AddIngredient(null, "ForestCore", 15);
			modRecipe.AddIngredient(1225, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
