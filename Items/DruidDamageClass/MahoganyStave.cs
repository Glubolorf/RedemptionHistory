﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MahoganyStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mahogany Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a spore\nRight-clicking will summon a Nature Pixie [c/94c2ff:(Requires 200 Mana)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Normal\n[c/98dbc3:Special Ability:] Triple-Shot\n[c/98c1db:Buffs:] 10% druidic damage, faster staves");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 7;
			base.item.width = 42;
			base.item.height = 46;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 1, 25);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = 567;
			base.item.shootSpeed = 7f;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.mana = 200;
				base.item.buffType = base.mod.BuffType("NatureGuardianBuff");
				base.item.buffTime = 36000;
				base.item.shoot = base.mod.ProjectileType("NatureGuardian1");
				base.item.shootSpeed = 0f;
				return !player.HasBuff(base.mod.BuffType("NatureGuardian2Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardianBuff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian3Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian4Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian5Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian6Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian7Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian8Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian9Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian10Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian11Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian12Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian13Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian14Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian15Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian16Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian17Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian18Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian19Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian20Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian21Buff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = 567;
			base.item.shootSpeed = 7f;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 24;
				base.item.useAnimation = 24;
			}
			else
			{
				base.item.useTime = 28;
				base.item.useAnimation = 28;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(620, 8);
			modRecipe.AddIngredient(331, 3);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
