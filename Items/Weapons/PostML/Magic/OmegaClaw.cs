﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class OmegaClaw : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/PostML/Magic/" + base.GetType().Name + "_Glow");
				OmegaClaw.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = OmegaClaw.customGlowMask;
			base.DisplayName.SetDefault("Gloop Gauntlet");
			base.Tooltip.SetDefault("'Slashing!'\nUnleashes a wave dealing extreme damage");
		}

		public override void SetDefaults()
		{
			base.item.magic = true;
			base.item.mana = 10;
			base.item.damage = 350;
			base.item.width = 46;
			base.item.height = 42;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.knockBack = 7f;
			base.item.useStyle = 1;
			base.item.crit = 26;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.shoot = ModContent.ProjectileType<OmegaClawPro>();
			base.item.shootSpeed = 15f;
			base.item.glowMask = OmegaClaw.customGlowMask;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<OmegaWave2>(), damage * 2, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "OblitBrain", 1);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 12);
			modRecipe.AddIngredient(null, "VlitchScale", 18);
			modRecipe.AddIngredient(null, "VlitchBattery", 2);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
