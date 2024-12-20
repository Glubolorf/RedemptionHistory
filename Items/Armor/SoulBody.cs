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
	public class SoulBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lost Soul's Chestplate");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n2% increased druidic damage\n4% increased druidic critical strike chance\n2% damage reduction\nSpirits shoot faster\n[c/bdffff:Spirit Level +1]");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 26;
			base.item.value = 75;
			base.item.rare = 1;
			base.item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.02f;
			druidDamagePlayer.druidCrit += 4;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
			player.endurance += 0.02f;
			redePlayer.spiritLevel++;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallLostSoul", 6);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
