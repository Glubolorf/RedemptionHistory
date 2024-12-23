﻿using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class IcarsFire : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Fire");
			base.Tooltip.SetDefault("'Blesses you with Icar's protection...'\nIncreases damage, life regen and mana regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 38;
			base.item.value = 10000;
			base.item.rare = 7;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<IcarsFrost>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<IcarsElements>())
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
			modRecipe.AddIngredient(null, "IcarsFlower", 1);
			modRecipe.AddIngredient(521, 15);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(ModContent.BuffType<FireBlessed>(), 10, true);
		}
	}
}
