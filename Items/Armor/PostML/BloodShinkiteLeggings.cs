﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class BloodShinkiteLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blood Shinkite Greaves");
			base.Tooltip.SetDefault("40% increased movement speed\n10% increased damage\n10% increased melee speed and critical strike chance\nGrants the ability to walk on lava");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.defense = 30;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.4f;
			player.allDamage += 0.1f;
			player.meleeCrit += 10;
			player.meleeSpeed += 0.1f;
			player.fireWalk = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteBlood", 16);
			modRecipe.AddIngredient(172, 40);
			modRecipe.AddIngredient(794, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
