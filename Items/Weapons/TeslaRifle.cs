﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaRifle : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				TeslaRifle.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = TeslaRifle.customGlowMask;
			base.DisplayName.SetDefault("Tesla Rifle");
			base.Tooltip.SetDefault("Rapidly fires tesla blasts");
		}

		public override void SetDefaults()
		{
			base.item.damage = 208;
			base.item.ranged = true;
			base.item.width = 70;
			base.item.height = 42;
			base.item.useAnimation = 8;
			base.item.useTime = 8;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(1, 50, 0, 0);
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/XenoEyeLaser1");
			base.item.autoReuse = true;
			base.item.shoot = 440;
			base.item.shootSpeed = 5f;
			base.item.glowMask = TeslaRifle.customGlowMask;
		}

		public override void ModifyTooltips(List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = new Color?(new Color(0, 255, 200));
				}
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int ShotAmt = 2;
			int spread = 2;
			float spreadMult = 0.3f;
			Vector2 vector2 = default(Vector2);
			for (int i = 0; i < ShotAmt; i++)
			{
				float vX = 8f * speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
				float vY = 8f * speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
				float angle = (float)Math.Atan((double)(vY / vX));
				vector2 = new Vector2(position.X + 75f * (float)Math.Cos((double)angle), position.Y + 75f * (float)Math.Sin((double)angle));
				if ((float)Main.mouseX + Main.screenPosition.X < player.position.X)
				{
					vector2 = new Vector2(position.X - 75f * (float)Math.Cos((double)angle), position.Y - 75f * (float)Math.Sin((double)angle));
				}
				int projectile = Projectile.NewProjectile(vector2.X, vector2.Y, vX, vY, 440, damage, knockBack, Main.myPlayer, 0f, 0f);
				Main.projectile[projectile].ranged = true;
				Main.projectile[projectile].magic = false;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "BluePrints", 1);
			modRecipe.AddIngredient(null, "TeslaManipulatorPrototype", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
