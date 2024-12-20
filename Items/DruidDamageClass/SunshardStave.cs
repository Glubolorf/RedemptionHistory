using System;
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
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/" + base.GetType().Name + "_Glow");
				SunshardStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
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
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveTripleShot)
			{
				float numberProjectiles = 3f;
				float rotation = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int i = 0;
				while ((float)i < numberProjectiles)
				{
					Vector2 perturbedSpeed2 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage / 2, knockBack, player.whoAmI, 0f, 0f);
					i++;
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveScatterShot && Main.rand.Next(5) == 0)
			{
				int numberProjectiles2 = 2 + Main.rand.Next(2);
				for (int j = 0; j < numberProjectiles2; j++)
				{
					Vector2 perturbedSpeed3 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					perturbedSpeed3 *= scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed3.X, perturbedSpeed3.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveQuadShot)
			{
				float numberProjectiles3 = 5f;
				float rotation2 = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int k = 0;
				while ((float)k < numberProjectiles3)
				{
					Vector2 perturbedSpeed4 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation2, rotation2, (float)k / (numberProjectiles3 - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed4.X, perturbedSpeed4.Y, type, damage / 3, knockBack, player.whoAmI, 0f, 0f);
					k++;
				}
				return false;
			}
			return true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
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
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "MoonflareStave", 1);
			modRecipe2.AddIngredient(null, "FDruidStave", 1);
			modRecipe2.AddIngredient(null, "CrystalStave", 1);
			modRecipe2.AddIngredient(null, "HallowedStave", 1);
			modRecipe2.AddIngredient(null, "ScarletStave", 1);
			modRecipe2.AddIngredient(549, 10);
			modRecipe2.AddIngredient(548, 10);
			modRecipe2.AddIngredient(547, 10);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}

		public static short customGlowMask;
	}
}
