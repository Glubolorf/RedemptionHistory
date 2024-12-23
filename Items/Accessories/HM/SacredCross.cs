﻿using System;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Accessories.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class SacredCross : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sacred Cross");
			base.Tooltip.SetDefault("7% increased druidic damage\nIncreases spirits summoned by 2\n[c/bdffff:Spirit Level +3]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 38;
			base.item.height = 48;
			base.item.value = Item.sellPrice(0, 1, 80, 0);
			base.item.rare = 5;
			base.item.accessory = true;
			this.spiritWeapon = false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SpiritualRelic", 1);
			modRecipe.AddIngredient(520, 10);
			modRecipe.AddIngredient(null, "LostSoul", 5);
			modRecipe.AddIngredient(1225, 5);
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
					if (slot != i && player.armor[i].type == ModContent.ItemType<TinCross>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<SpiritualRelic>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<CopperCross>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<SpiritualGuide>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<LastBurden>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.07f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritLevel += 3;
			redePlayer.spiritExtras += 2;
		}
	}
}
