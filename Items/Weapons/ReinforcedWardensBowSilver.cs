﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ReinforcedWardensBowSilver : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Reinforced Warden's Bow");
			base.Tooltip.SetDefault("'Reinforced with silver'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 24;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 16;
			base.item.height = 42;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 5;
			base.item.shoot = 1;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 0, 1, 20);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = false;
			base.item.shootSpeed = 10f;
			base.item.crit = 0;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "WardensBow", 1);
			modRecipe.AddIngredient(21, 6);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
