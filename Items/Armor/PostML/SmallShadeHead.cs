﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class SmallShadeHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Small Shadehead");
			base.Tooltip.SetDefault("10% increased druidic damage\n15% increased druidic critical strike chance\n4% increased damage reduction\nThrows seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 26;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 15;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterSeedbags = true;
			player.endurance *= 0.04f;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SmallShadeBody>() && legs.type == ModContent.ItemType<SmallShadeLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Attacks inflict the 'Soulless' debuff";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).smallShadeSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallShadesoul", 6);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
