using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class ForestNymphsCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Nymph's Core");
			base.Tooltip.SetDefault("'Become one with nature'\n[c/64ff64:-Strengths-]\n5% increased druidic damage\nYou will photosynesis at day, increasing life regen and max life\nGreatly increased life regen while in water\nAll stats are increased while it's raining\n[c/ff6464:-Weaknesses-]\nYou are weaker at night, decreasing damage and defence\nYou can't regen life when in the corruption/crimson");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 7));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 28;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.rare = 1;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			if ((Main.dayTime && player.ZoneOverworldHeight) || player.ZoneSkyHeight)
			{
				player.lifeRegen += 5;
				player.statLifeMax2 += 50;
			}
			if (!Main.dayTime)
			{
				druidDamagePlayer.druidDamage *= 0.95f;
				player.magicDamage *= 0.95f;
				player.meleeDamage *= 0.95f;
				player.minionDamage *= 0.95f;
				player.rangedDamage *= 0.95f;
				player.thrownDamage *= 0.95f;
				player.statDefense -= 4;
			}
			if (player.wet)
			{
				player.lifeRegen += 15;
			}
			if (player.ZoneCrimson)
			{
				player.bleed = true;
			}
			if (player.ZoneCorrupt)
			{
				player.bleed = true;
			}
			if (Main.raining)
			{
				druidDamagePlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
				player.statDefense += 4;
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
