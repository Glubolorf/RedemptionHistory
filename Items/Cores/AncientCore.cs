using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class AncientCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Core");
			base.Tooltip.SetDefault("'Roam the caverns of the ancient world'\n[c/64ff64:-Strengths-]\n5% increased mining speed\nYou will emit an aura of light\nYou are stronger underground, increasing all stats and vision\n[c/ff6464:-Weaknesses-]\nYou have less vision on the surface, and will lose life");
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
			player.pickSpeed *= 1.05f;
			player.AddBuff(11, 2, true);
			if (player.ZoneOverworldHeight || player.ZoneSkyHeight)
			{
				player.blind = true;
				player.lifeRegen -= 15;
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
			player.nightVision = true;
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
