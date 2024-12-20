﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class DruidHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Druid's Hood");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n6% increased druidic damage\nSpirits shoot faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.value = 10000;
			base.item.rare = 4;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.06f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("DruidBody") && legs.type == base.mod.ItemType("DruidLeggings");
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadowSubtle = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Spirits home in on enemies";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritHoming = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(225, 10);
			modRecipe.AddIngredient(null, "ForestCore", 4);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
