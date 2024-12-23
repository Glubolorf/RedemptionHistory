﻿using System;
using Redemption.Buffs.NPCBuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class KeepersClaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper's Claw");
			base.Tooltip.SetDefault("Hitting enemies inflict Necrotic Gouge, causing slain enemies to burst into tiny dark souls");
		}

		public override void SetDefaults()
		{
			base.item.damage = 36;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 66;
			base.item.useTime = 39;
			base.item.useAnimation = 39;
			base.item.useStyle = 1;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 2, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = false;
			base.item.useTurn = false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<NecroticGouge>(), 600, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
