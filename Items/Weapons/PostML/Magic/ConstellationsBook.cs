﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class ConstellationsBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Constellations");
			base.Tooltip.SetDefault("'We own the stars, We own the sky...'\nSummons stars around the user");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1300;
			base.item.magic = true;
			base.item.width = 28;
			base.item.height = 24;
			base.item.mana = 15;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<ConstellationsStar>();
			base.item.shootSpeed = 0f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-600, 600), player.position.Y + (float)Main.rand.Next(-600, 600)), new Vector2(speedX, speedY), ModContent.ProjectileType<ConstellationsStar>(), damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-600, 600), player.position.Y + (float)Main.rand.Next(-600, 600)), new Vector2(speedX, speedY), ModContent.ProjectileType<ConstellationsStar>(), damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(new Vector2(player.position.X + (float)Main.rand.Next(-600, 600), player.position.Y + (float)Main.rand.Next(-600, 600)), new Vector2(speedX, speedY), ModContent.ProjectileType<ConstellationsStar>(), damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
	}
}
