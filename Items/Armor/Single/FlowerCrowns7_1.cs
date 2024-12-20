﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Single
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class FlowerCrowns7_1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dawn Flower Crown");
			base.Tooltip.SetDefault("'Dawn brings the light back'\nStaves cast faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 10;
			base.item.value = 5000;
			base.item.rare = 1;
			base.item.defense = 3;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == 83 && legs.type == 79;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "15% increased druidic damage";
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.15f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(19, 6);
			modRecipe.AddIngredient(318, 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}