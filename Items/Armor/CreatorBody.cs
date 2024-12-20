﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class CreatorBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Creation Druid's Leaf Plating");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n17% increased druidic damage\n17% increased druidic critical strike chance\nStaves swing faster\nThrows seedbags faster\nSpirits shoot faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 30;
			base.item.rare = 10;
			base.item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.9f;
			druidDamagePlayer.druidCrit += 9;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterStaves = true;
			redePlayer.fasterSeedbags = true;
			redePlayer.fasterSpirits = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CreationFragment", 20);
			modRecipe.AddIngredient(3467, 16);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}