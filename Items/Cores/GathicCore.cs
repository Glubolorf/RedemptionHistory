using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class GathicCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gathic Core");
			base.Tooltip.SetDefault("'Fight with the honor of the Iron Realm'\n[c/64ff64:-Strengths-]\n5% increased melee damage and speed\nYou are immune to Bleeding, Broken Armor and Weak\nThe lower your life is, the more damage you deal\n[c/ff6464:-Weaknesses-]\nYou are constantly drunk\n100% increased mana cost\nIncreased enemy aggro");
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
			player.meleeDamage *= 1.05f;
			player.meleeSpeed *= 1.05f;
			player.buffImmune[33] = true;
			player.buffImmune[36] = true;
			player.buffImmune[30] = true;
			player.AddBuff(25, 2, true);
			player.manaCost += 1f;
			player.aggro += 50;
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.75f)
			{
				druidDamagePlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.5f)
			{
				druidDamagePlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.25f)
			{
				druidDamagePlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.1f)
			{
				druidDamagePlayer.druidDamage *= 1.05f;
				player.magicDamage *= 1.05f;
				player.meleeDamage *= 1.05f;
				player.minionDamage *= 1.05f;
				player.rangedDamage *= 1.05f;
				player.thrownDamage *= 1.05f;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.05f)
			{
				druidDamagePlayer.druidDamage *= 1.1f;
				player.magicDamage *= 1.1f;
				player.meleeDamage *= 1.1f;
				player.minionDamage *= 1.1f;
				player.rangedDamage *= 1.1f;
				player.thrownDamage *= 1.1f;
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
