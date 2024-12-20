﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class SunshardStave : DruidDamageItem
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
				SunshardStave.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Sunshard Greatstave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nRapidly shoots Redemptive Sparks");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 70;
			base.item.height = 116;
			base.item.width = 116;
			base.item.useTime = 8;
			base.item.useAnimation = 8;
			base.item.mana = 2;
			base.item.useStyle = 5;
			base.item.crit = 18;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 30, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item125;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("LastRPro5");
			base.item.shootSpeed = 18f;
			base.item.glowMask = SunshardStave.customGlowMask;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
			speedX = vector.X;
			speedY = vector.Y;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveTripleShot)
			{
				float num = 3f;
				float num2 = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int num3 = 0;
				while ((float)num3 < num)
				{
					Vector2 vector2 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					num3++;
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveScatterShot && Main.rand.Next(5) == 0)
			{
				int num4 = 2 + Main.rand.Next(2);
				for (int i = 0; i < num4; i++)
				{
					Vector2 vector3 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float num5 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector3 *= num5;
					Projectile.NewProjectile(position.X, position.Y, vector3.X, vector3.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveQuadShot)
			{
				float num6 = 5f;
				float num7 = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int num8 = 0;
				while ((float)num8 < num6)
				{
					Vector2 vector4 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num7, num7, (float)num8 / (num6 - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, vector4.X, vector4.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					num8++;
				}
				return false;
			}
			return true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MoonflareStave", 1);
			modRecipe.AddIngredient(null, "FDruidStave", 1);
			modRecipe.AddIngredient(null, "CrystalStave", 1);
			modRecipe.AddIngredient(null, "HallowedStave", 1);
			modRecipe.AddIngredient(null, "SapphireStave", 1);
			modRecipe.AddIngredient(549, 10);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddIngredient(547, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MoonflareStave", 1);
			modRecipe.AddIngredient(null, "FDruidStave", 1);
			modRecipe.AddIngredient(null, "CrystalStave", 1);
			modRecipe.AddIngredient(null, "HallowedStave", 1);
			modRecipe.AddIngredient(null, "ScarletStave", 1);
			modRecipe.AddIngredient(549, 10);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddIngredient(547, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
