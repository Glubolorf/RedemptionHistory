using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class ThamorCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thamor Core");
			base.Tooltip.SetDefault("'The sands will rise'\n[c/64ff64:-Strengths-]\n5% increased throwing damage\nYou can walk with ease through sandstorms\nYou are stronger in the Desert Biome, increasing all stats\nYou gain great power in a sandstorm\n[c/ff6464:-Weaknesses-]\nYou can't regen life in the snow biome\nAll damage types (Except throwing) are decreased by 50%");
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
			player.thrownDamage *= 1.05f;
			player.buffImmune[194] = true;
			modPlayer.druidDamage *= 0.5f;
			player.magicDamage *= 0.5f;
			player.meleeDamage *= 0.5f;
			player.minionDamage *= 0.5f;
			player.rangedDamage *= 0.5f;
			if (player.ZoneSnow && !player.resistCold)
			{
				player.bleed = true;
			}
			if (player.ZoneDesert || player.ZoneUndergroundDesert)
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
			if (player.ZoneSandstorm)
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
