﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DaybloomBag : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Daybloom Seed Bag");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrows a seed that grows into a Daybloom\nDeals more damage at daytime");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 3;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.mana = 3;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 70, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("Seed13");
			base.item.shootSpeed = 12f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.dayTime)
			{
				base.item.damage = 6;
			}
			else
			{
				base.item.damage = 3;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSeedbags)
			{
				base.item.useTime = 31;
				base.item.useAnimation = 31;
			}
			else
			{
				base.item.useTime = 36;
				base.item.useAnimation = 36;
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
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).extraSeed && Main.rand.Next(3) == 0)
			{
				int num3 = 2;
				for (int j = 0; j < num3; j++)
				{
					Vector2 vector2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
					float num4 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector2 *= num4;
					Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(313, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
