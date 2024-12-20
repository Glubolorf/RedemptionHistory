﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class GreatSkiesStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Great Skies Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nLunges the user forward with great force");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 57;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 38;
			base.item.useAnimation = 38;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 11f;
			base.item.value = Item.sellPrice(0, 2, 25, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
		}

		public override bool UseItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("GSSBuff"), 20, true);
			Vector2 MyVelocity = Utils.SafeNormalize(Main.MouseWorld - player.Center, -Vector2.UnitY) * 8f;
			player.velocity += MyVelocity;
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
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(320, 10);
			modRecipe.AddIngredient(575, 20);
			modRecipe.AddIngredient(9, 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
