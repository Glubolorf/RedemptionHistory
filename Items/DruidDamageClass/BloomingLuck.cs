﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class BloomingLuck : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blooming Luck");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nGrants immunity to knockback\n5% increased druidic damage\nSlightly increases the chance of finding Rare and Epic drops");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 4, 0, 0);
			base.item.rare = 3;
			base.item.accessory = true;
			base.item.defense = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(313, 10);
			modRecipe.AddIngredient(null, "XenomiteShard", 10);
			modRecipe.AddIngredient(158, 1);
			modRecipe.AddIngredient(156, 1);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.bloomingLuck = true;
			player.noKnockback = true;
		}
	}
}
