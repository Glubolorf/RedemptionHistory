﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Single
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class Garland4 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Orichalcum Garland");
			base.Tooltip.SetDefault("15% increased druidic damage\n8% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 16;
			base.item.value = Item.sellPrice(0, 2, 25, 0);
			base.item.rare = 4;
			base.item.defense = 6;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 8;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == 1213 && legs.type == 1214;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Staves cast faster, Throws seedbags faster\nFlower petals will fall on your target for extra damage";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.staveSpeed += 0.05f;
			redePlayer.fasterSeedbags = true;
			player.onHitPetal = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1191, 12);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
