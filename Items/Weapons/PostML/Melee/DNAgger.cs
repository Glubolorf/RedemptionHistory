﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class DNAgger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("DNAgger");
			base.Tooltip.SetDefault("'Best way to defeat a serious illness? Cut it.'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 110;
			base.item.width = 64;
			base.item.height = 64;
			base.item.value = 30000;
			base.item.useStyle = 1;
			base.item.useAnimation = 8;
			base.item.useTime = 8;
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item71;
			base.item.knockBack = 6f;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<DNAPro1>();
			base.item.shootSpeed = 6f;
			base.item.useTurn = true;
		}

		public override bool Shoot(Player player, ref Vector2 shootPos, ref float speedX, ref float speedY, ref int projType, ref int damage, ref float knockback)
		{
			Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX, speedY, projType, damage, knockback, player.whoAmI, 0f, 0f);
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(shootPos.X, shootPos.Y, speedX * 1f, speedY * 1f, (i == 0) ? ModContent.ProjectileType<DNAPro2>() : ModContent.ProjectileType<DNAPro3>(), damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}
	}
}
