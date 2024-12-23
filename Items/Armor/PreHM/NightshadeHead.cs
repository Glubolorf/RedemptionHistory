﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class NightshadeHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Cowl");
			base.Tooltip.SetDefault("10% increased ranged critical strike chance\n+40 max mana");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 0, 18, 0);
			base.item.rare = 1;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 10;
			player.statManaMax2 += 40;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<NightshadeBody>() && legs.type == ModContent.ItemType<NightshadeLegs>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Not moving puts you in stealth, increasing ranged ability and reducing chance for enemies to target you.";
			player.shroomiteStealth = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(92, 1);
			modRecipe.AddIngredient(null, "Nightshade", 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(696, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 6);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
