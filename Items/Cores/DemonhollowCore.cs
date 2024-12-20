using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class DemonhollowCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Demonhollow Core");
			base.Tooltip.SetDefault("'Burn with the fury of the demons'\n[c/64ff64:-Strengths-]\n5% increased magic damage\nYou are immune to lava and most types of heat\nYou are stronger in the Underworld, increasing all stats\nStanding in lava will greatly increase life regen\n[c/ff6464:-Weaknesses-]\nYou can't regen life on the surface\nYou will quickly lose life while in water");
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
			player.magicDamage *= 1.05f;
			player.lavaImmune = true;
			player.buffImmune[24] = true;
			player.buffImmune[67] = true;
			player.buffImmune[153] = true;
			player.buffImmune[44] = true;
			player.buffImmune[39] = true;
			if (player.ZoneOverworldHeight || player.ZoneSkyHeight)
			{
				player.bleed = true;
			}
			if (player.ZoneUnderworldHeight)
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
			if (player.lavaWet)
			{
				player.lifeRegen += 25;
			}
			if (player.wet && !player.lavaWet && !player.gills)
			{
				player.lifeRegen -= 100;
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
