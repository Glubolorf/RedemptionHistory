﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class WraithSlayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wraith Slayer");
			base.Tooltip.SetDefault("Deals triple damage to ghost-like enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 27;
			base.item.melee = true;
			base.item.width = 80;
			base.item.height = 80;
			base.item.useTime = 11;
			base.item.useAnimation = 11;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ForgottenSword", 1);
			modRecipe.AddIngredient(null, "LostSoul", 4);
			modRecipe.AddIngredient(521, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 84 || target.type == 179 || target.type == 83 || target.type == 533 || target.type == 288 || target.type == 182 || target.type == 316 || target.type == 140 || target.type == 82 || target.type == 253 || target.type == 330 || target.type == 533)
			{
				damage *= 3;
			}
			if (target.type == base.mod.NPCType("TheKeeper") || target.type == base.mod.NPCType("AAAA") || target.type == base.mod.NPCType("DarkSoul1") || target.type == base.mod.NPCType("DarkSoul2") || target.type == base.mod.NPCType("DarkSoul3") || target.type == base.mod.NPCType("SkullDigger") || target.type == base.mod.NPCType("WanderingSoul"))
			{
				damage *= 3;
			}
		}
	}
}