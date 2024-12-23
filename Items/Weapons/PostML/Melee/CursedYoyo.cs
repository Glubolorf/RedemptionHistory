﻿using System;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class CursedYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ashrune Yoyo");
			base.Tooltip.SetDefault("Shoots Cursed Orbs\nThe orbs inflict Cursed Flames, if one kills an enemy, it will multiply into 2 Cursed Orbs\nThe orbs created by the multiply won't multiply themselves");
		}

		public override void SetDefaults()
		{
			base.item.damage = 520;
			base.item.melee = true;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<CursedYoyoPro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 8);
			modRecipe.AddIngredient(172, 15);
			modRecipe.AddIngredient(3291, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
