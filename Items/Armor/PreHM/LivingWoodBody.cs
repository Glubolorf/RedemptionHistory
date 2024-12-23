﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class LivingWoodBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Breastplate");
			base.Tooltip.SetDefault("Increases minion damage by 4%");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 22;
			base.item.value = 0;
			base.item.rare = 0;
			base.item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage *= 1.04f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingTwig", 36);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
