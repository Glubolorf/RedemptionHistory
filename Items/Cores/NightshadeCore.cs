using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class NightshadeCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nightshade Core");
			base.Tooltip.SetDefault("'Become one with the night'\n[c/64ff64:-Strengths-]\n5% increased minion damage\nThe night cloaks you\nYou are stronger at night, increasing all stats\n[c/ff6464:-Weaknesses-]\nYou are weakened at day\nYou can't regen life at day");
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
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.minionDamage *= 1.05f;
			if (Main.dayTime)
			{
				druidDamagePlayer.druidDamage *= 0.95f;
				player.magicDamage *= 0.95f;
				player.meleeDamage *= 0.95f;
				player.minionDamage *= 0.95f;
				player.rangedDamage *= 0.95f;
				player.thrownDamage *= 0.95f;
				player.statDefense -= 4;
				player.statLifeMax2 -= 50;
				player.bleed = true;
				return;
			}
			druidDamagePlayer.druidDamage *= 1.05f;
			player.magicDamage *= 1.05f;
			player.meleeDamage *= 1.05f;
			player.minionDamage *= 1.05f;
			player.rangedDamage *= 1.05f;
			player.thrownDamage *= 1.05f;
			player.statDefense += 4;
			player.statLifeMax2 += 50;
			player.lifeRegen += 5;
			player.shroomiteStealth = true;
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
