﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class DruidBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Druid's Tunic");
			base.Tooltip.SetDefault("12% increased druidic damage\n12% increased druidic critical strike chance\nStaves cast faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 20;
			base.item.value = 9000;
			base.item.rare = 4;
			base.item.defense = 11;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.12f;
			druidDamagePlayer.druidCrit += 12;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(225, 20);
			modRecipe.AddIngredient(null, "ForestCore", 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
