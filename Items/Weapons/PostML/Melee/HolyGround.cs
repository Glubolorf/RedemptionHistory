﻿using System;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class HolyGround : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holy Ground");
			base.Tooltip.SetDefault("'We are building better worlds...'\nCreates a mini earthquake when hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1300;
			base.item.melee = true;
			base.item.width = 70;
			base.item.height = 70;
			base.item.useTime = 2;
			base.item.useAnimation = 20;
			base.item.hammer = 100;
			base.item.useStyle = 1;
			base.item.knockBack = 11f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<HolyGroundPro1>(), base.item.damage, base.item.knockBack, Main.myPlayer, 0f, 0f);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
