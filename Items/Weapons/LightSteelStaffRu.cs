﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LightSteelStaffRu : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Staff");
			base.Tooltip.SetDefault("'Asteria, the Royal Knight'\nCasts Red Blasts");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 85;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 46;
			base.item.height = 46;
			base.item.useTime = 11;
			base.item.useAnimation = 11;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("LSStaffRuPro");
			base.item.shootSpeed = 13f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 24);
			modRecipe.AddIngredient(182, 8);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
			speedX = vector.X;
			speedY = vector.Y;
			return true;
		}
	}
}