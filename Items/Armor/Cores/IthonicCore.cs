using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Cores
{
	public class IthonicCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ithonic Core");
			base.Tooltip.SetDefault("'The oceans will guide you'\n[c/64ff64:-Strengths-]\n5% decreased mana cost\nYou can swim and breathe for longer underwater\nYou are stronger in water, increasing all stats\nStanding in water will increase life regen\n[c/ff6464:-Weaknesses-]\nYou can't regen life in the Underworld\nYou will lose life way more rapidly in lava");
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
			player.manaCost *= 0.95f;
			player.accFlipper = true;
			player.accDivingHelm = true;
			if (player.ZoneUnderworldHeight && !player.lavaImmune)
			{
				player.bleed = true;
			}
			if (player.lavaWet && !player.lavaImmune)
			{
				player.lifeRegen -= 100;
			}
			if (player.wet && !player.lavaWet)
			{
				modPlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
				player.statDefense += 4;
				player.statLifeMax2 += 50;
				player.lifeRegen += 15;
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
