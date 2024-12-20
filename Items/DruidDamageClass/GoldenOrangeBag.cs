﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class GoldenOrangeBag : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Orange Tree Seed Bag");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrows a seed that grows into a Golden Orange Tree");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 44;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 59;
			base.item.useAnimation = 59;
			base.item.useStyle = 1;
			base.item.mana = 10;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 4, 50, 50);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("Seed28");
			base.item.shootSpeed = 18f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSeedbags)
			{
				base.item.useTime = 54;
				base.item.useAnimation = 54;
			}
			else
			{
				base.item.useTime = 59;
				base.item.useAnimation = 59;
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
	}
}
