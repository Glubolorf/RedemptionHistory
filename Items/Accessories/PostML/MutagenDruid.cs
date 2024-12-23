﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class MutagenDruid : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid's Mutagen");
			base.Tooltip.SetDefault("15% increased druidic damage\n10% increased druidic critical strike chance");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 28;
			base.item.height = 36;
			base.item.value = Item.buyPrice(0, 12, 0, 0);
			base.item.rare = 11;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "EmptyMutagen", 1);
			modRecipe.AddIngredient(1301, 1);
			modRecipe.AddIngredient(null, "CreationFragment", 10);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 10;
		}
	}
}
