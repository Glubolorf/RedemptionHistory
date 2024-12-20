﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SlayerFist : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Slayer's Rocket Fist");
			base.Tooltip.SetDefault("'For when you want to slap someone from afar'\nThrows homing rocket fists at enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 260;
			base.item.melee = true;
			base.item.width = 24;
			base.item.height = 24;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item74;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("KSFist2");
			base.item.shootSpeed = 10f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
			speedX = vector.X;
			speedY = vector.Y;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CyberPlating", 8);
			modRecipe.AddIngredient(null, "KingCore", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
