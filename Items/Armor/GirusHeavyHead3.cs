﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GirusHeavyHead3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Techmancer Hood");
			base.Tooltip.SetDefault("6% increased magic damage\n6% increased magic crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.06f;
			player.magicCrit += 6;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("GirusHeavyBody3") && legs.type == base.mod.ItemType("GirusHeavyLeggings3");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Enemies are more likely to target you, boosted mana stats and 8% damage reduction.\nSummons a tiny Jellyfish Drone\nAny hostile projectile within the aura gets zapped, destroying it\nAny enemy within the aura gets zapped, dealing damage and stunning it for a second\nBosses and enemies with no knockback don't get stunned";
			player.AddBuff(11, 2, true);
			player.manaCost *= 0.85f;
			player.manaRegen += 5;
			player.statManaMax2 += 100;
			player.endurance += 0.08f;
			player.aggro += 10;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(base.mod.BuffType("MageDroneBuff")) == -1)
				{
					player.AddBuff(base.mod.BuffType("MageDroneBuff"), 3600, true);
				}
				if (player.ownedProjectileCounts[base.mod.ProjectileType("MageDrone")] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, base.mod.ProjectileType("MageDrone"), 100, 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteHead", 1);
			modRecipe.AddIngredient(null, "VlitchScale", 10);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
