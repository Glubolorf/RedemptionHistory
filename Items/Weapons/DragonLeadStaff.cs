﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DragonLeadStaff : ModItem
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
				DragonLeadStaff.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = DragonLeadStaff.customGlowMask;
			base.DisplayName.SetDefault("Dragon Slayer's Greatstaff");
			base.Tooltip.SetDefault("'It's dangerous to play with fire...'\nCasts a Molten Blast that bounces erratically on tiles");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 40;
			base.item.magic = true;
			base.item.mana = 12;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 8, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("MoltenBlastPro1");
			base.item.shootSpeed = 24f;
			base.item.glowMask = DragonLeadStaff.customGlowMask;
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
			if (target.type == 551 || target.type == 558 || target.type == 559 || target.type == 560 || target.type == 170 || target.type == 180 || target.type == 171 || target.type == 370 || target.type == base.mod.NPCType("GreenPigron"))
			{
				damage *= 50;
			}
			if (target.type >= 87 && target.type <= 92)
			{
				damage *= 50;
			}
			if (target.type >= 454 && target.type <= 459)
			{
				damage *= 50;
			}
		}

		public static short customGlowMask;
	}
}
