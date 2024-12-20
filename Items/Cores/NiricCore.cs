using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class NiricCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Niric Core");
			base.Tooltip.SetDefault("'Feel the cold of the Arctic Realm'\n[c/64ff64:-Strengths-]\n5% increased ranged damage\nYou are immune to all types of coldness\nYou are stronger in the Snow Biome, increasing all stats\nYou gain great power in a blizzard\n[c/ff6464:-Weaknesses-]\nYou can't regen life in the desert\nYou will slowly lose life in the Underworld");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.rare = 1;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer modPlayer = DruidDamagePlayer.ModPlayer(player);
			player.rangedDamage *= 1.05f;
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
			player.buffImmune[44] = true;
			if (player.ZoneUnderworldHeight && !player.lavaImmune)
			{
				player.bleed = true;
				player.lifeRegen += -5;
			}
			if (player.ZoneDesert || player.ZoneUndergroundDesert)
			{
				player.bleed = true;
			}
			if (player.ZoneSnow)
			{
				modPlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
				player.statDefense += 4;
				player.statLifeMax2 += 50;
				player.lifeRegen += 5;
			}
			if (player.ZoneSnow && Main.raining)
			{
				modPlayer.druidDamage *= 1.15f;
				player.magicDamage *= 1.15f;
				player.meleeDamage *= 1.15f;
				player.minionDamage *= 1.15f;
				player.rangedDamage *= 1.15f;
				player.thrownDamage *= 1.15f;
				player.statDefense += 8;
				player.statLifeMax2 += 50;
				player.lifeRegen += 5;
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "EmptyCore", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
