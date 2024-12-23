﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class SmallShadeBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Small Shadeplate");
			base.Tooltip.SetDefault("50 max life and mana\n10% increased druidic damage\n8% increased damage reduction");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 26;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 50;
			player.statManaMax2 += 50;
			player.endurance += 0.08f;
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallShadesoul", 10);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
