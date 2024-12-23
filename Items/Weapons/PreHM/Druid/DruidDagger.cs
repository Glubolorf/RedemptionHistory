﻿using System;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid
{
	public class DruidDagger : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid Dagger");
			base.Tooltip.SetDefault("Any enemy that gets hit is inflicted with a pollen cloud");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 20f;
			base.item.crit = 4;
			base.item.damage = 8;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 26;
			base.item.useTime = 26;
			base.item.width = 26;
			base.item.height = 48;
			base.item.maxStack = 999;
			base.item.rare = 1;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 0, 75);
			base.item.shoot = ModContent.ProjectileType<DruidDaggerPro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddRecipeGroup("Redemption:Plant", 2);
			modRecipe.AddIngredient(19, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 200);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddRecipeGroup("Redemption:Plant", 2);
			modRecipe2.AddIngredient(706, 1);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 200);
			modRecipe2.AddRecipe();
		}
	}
}
