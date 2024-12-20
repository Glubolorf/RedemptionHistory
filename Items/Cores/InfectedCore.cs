using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Cores
{
	public class InfectedCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Core");
			base.Tooltip.SetDefault("'Carnivorous crystals grow inside you'\n[c/64ff64:-Strengths-]\n+50 increased max life\nYou are immune to the Infection, Radioactive Fallout and Radiation Poisoning\nYou are a lot stronger in the Wasteland, greatly increasing all stats\nYou deal damage to any enemy that hits you\n[c/ff6464:-Weaknesses-]\nMovement speed is reduced, unless you are in the Wasteland\nYou are constantly blind, unless you are in the Wasteland");
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
			RedePlayer modPlayer2 = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.statLifeMax2 += 50;
			player.thorns = 1f;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff")] = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff2")] = true;
			player.buffImmune[base.mod.BuffType("RadioactiveFalloutDebuff")] = true;
			if (modPlayer2.ZoneXeno || modPlayer2.ZoneEvilXeno)
			{
				modPlayer.druidDamage *= 1.1f;
				player.magicDamage *= 1.1f;
				player.meleeDamage *= 1.1f;
				player.minionDamage *= 1.1f;
				player.rangedDamage *= 1.1f;
				player.thrownDamage *= 1.1f;
				player.statDefense += 18;
				player.statLifeMax2 += 200;
				player.lifeRegen += 10;
				player.nightVision = true;
				player.moveSpeed += 50f;
				player.jumpBoost = true;
				player.blind = false;
			}
			else
			{
				player.blind = true;
				player.moveSpeed *= 0.85f;
			}
			player.GetModPlayer<RedePlayer>().irradiatedEffect = 0;
			player.GetModPlayer<RedePlayer>().irradiatedLevel = 0;
			player.GetModPlayer<RedePlayer>().irradiatedTimer = 0;
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
