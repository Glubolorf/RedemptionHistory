﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomiteHamaxe : ModItem
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
				XenomiteHamaxe.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = XenomiteHamaxe.customGlowMask;
			base.DisplayName.SetDefault("Xenomite Hamaxe");
		}

		public override void SetDefaults()
		{
			base.item.damage = 55;
			base.item.melee = true;
			base.item.width = 62;
			base.item.height = 60;
			base.item.useTime = 18;
			base.item.useAnimation = 26;
			base.item.axe = 35;
			base.item.hammer = 100;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = 575000;
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.glowMask = XenomiteHamaxe.customGlowMask;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 30);
			modRecipe.AddIngredient(null, "StarliteBar", 5);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, base.mod.DustType("XenoDust"), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}

		public static short customGlowMask;
	}
}
