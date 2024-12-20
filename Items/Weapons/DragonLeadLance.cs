﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DragonLeadLance : ModItem
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
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				DragonLeadLance.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = DragonLeadLance.customGlowMask;
			base.DisplayName.SetDefault("Dragon Slayer's Greatlance");
			base.Tooltip.SetDefault("'Pierces dragon's hearts...'\nBurns enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 44;
			base.item.useStyle = 5;
			base.item.useAnimation = 37;
			base.item.useTime = 37;
			base.item.shootSpeed = 2.6f;
			base.item.knockBack = 7f;
			base.item.width = 54;
			base.item.height = 54;
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = base.mod.ProjectileType<DragonLeadLancePro>();
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = false;
			base.item.glowMask = DragonLeadLance.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 8);
			modRecipe.AddIngredient(175, 2);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 551)
			{
				damage *= 50;
			}
			if (target.type >= 87 && target.type <= 92)
			{
				damage *= 50;
			}
			if (target.type == 558)
			{
				damage *= 50;
			}
			if (target.type == 559)
			{
				damage *= 50;
			}
			if (target.type == 560)
			{
				damage *= 50;
			}
			if (target.type >= 454 && target.type <= 459)
			{
				damage *= 50;
			}
			if (target.type == 170)
			{
				damage *= 50;
			}
			if (target.type == 180)
			{
				damage *= 50;
			}
			if (target.type == 171)
			{
				damage *= 50;
			}
			if (target.type == 370)
			{
				damage *= 50;
			}
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public static short customGlowMask;
	}
}