﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class IcarsFlowerBag : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Flower Seed Bag");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrows a seed that grows into an upside-down floating flower");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 23;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 48;
			base.item.useAnimation = 48;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 45, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("Seed7");
			base.item.shootSpeed = 2.5f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSeedbags)
			{
				base.item.useTime = 43;
				base.item.useAnimation = 43;
			}
			else
			{
				base.item.useTime = 48;
				base.item.useAnimation = 48;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).moreSeeds)
			{
				int num = 2 + Main.rand.Next(2);
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(35f));
					float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector *= num2;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(751, 5);
			modRecipe.AddIngredient(null, "IcarsFlower", 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
