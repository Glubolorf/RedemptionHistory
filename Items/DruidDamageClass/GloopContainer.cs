﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class GloopContainer : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/" + base.GetType().Name + "_Glow");
				GloopContainer.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Gloop Container");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrow a container filled with gloop");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 140;
			base.item.width = 18;
			base.item.height = 34;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.mana = 12;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("GloopContainerPro");
			base.item.shootSpeed = 18f;
			base.item.glowMask = GloopContainer.customGlowMask;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSeedbags)
			{
				base.item.useTime = 32;
				base.item.useAnimation = 32;
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
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
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
			modRecipe.AddIngredient(null, "OblitBrain", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 16);
			modRecipe.AddIngredient(null, "VlitchScale", 14);
			modRecipe.AddIngredient(null, "VlitchBattery", 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}