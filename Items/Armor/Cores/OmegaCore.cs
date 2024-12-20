using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Cores
{
	public class OmegaCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Core");
			base.Tooltip.SetDefault("'The corrupted machine controls you'\n[c/64ff64:-Strengths-]\n5% damage reduction\nYou transform into an Omega Android\nYou are immune to Bleeding, Confusion, Poisoned, On Fire!, Venom, Burning and Electrified\n8% increase to all damage\nYour exoskeleton allows increased jump height and movement speed\nYou are immune to knockback\n[c/ff6464:-Weaknesses-]\nYou regen less life\nYou will explode in water\nYou will slowly malfunction in rain while on the surface\nYour attack and defense will decrease the lower your life is");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 8));
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
			RedePlayer p = player.GetModPlayer<RedePlayer>();
			DruidDamagePlayer modPlayer = DruidDamagePlayer.ModPlayer(player);
			p.omegaPower = true;
			player.endurance *= 1.05f;
			player.jumpBoost = true;
			player.moveSpeed += 25f;
			player.buffImmune[30] = true;
			player.buffImmune[31] = true;
			player.buffImmune[20] = true;
			player.buffImmune[24] = true;
			player.buffImmune[70] = true;
			player.buffImmune[144] = true;
			player.buffImmune[67] = true;
			player.noKnockback = true;
			player.bleed = true;
			modPlayer.druidDamage *= 1.08f;
			player.magicDamage *= 1.08f;
			player.meleeDamage *= 1.08f;
			player.minionDamage *= 1.08f;
			player.rangedDamage *= 1.08f;
			player.thrownDamage *= 1.08f;
			if (player.wet && !player.lavaWet && !player.honeyWet && !p.hazmatPower && !p.HEVPower)
			{
				player.lifeRegen -= 1000;
				Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 226, 0f, 0f, 100, default(Color), 2f);
			}
			if (Main.raining && player.ZoneOverworldHeight && Framing.GetTileSafely(player.Center).wall == 0 && player.HeldItem.type != 946 && !BasePlayer.HasHelmet(player, 1243, true) && !BasePlayer.HasHelmet(player, 1135, true) && !BasePlayer.HasChestplate(player, 1136, true))
			{
				player.lifeRegen += -10;
				if (Main.rand.Next(10) == 0)
				{
					Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 226, 0f, 0f, 100, default(Color), 2f);
				}
			}
			if (Main.raining && player.ZoneSkyHeight && Framing.GetTileSafely(player.Center).wall == 0 && player.HeldItem.type != 946 && !BasePlayer.HasHelmet(player, 1243, true) && !BasePlayer.HasHelmet(player, 1135, true) && !BasePlayer.HasChestplate(player, 1136, true))
			{
				player.lifeRegen += -10;
				if (Main.rand.Next(10) == 0)
				{
					Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 226, 0f, 0f, 100, default(Color), 2f);
				}
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.75f)
			{
				modPlayer.druidDamage *= 0.96f;
				player.magicDamage *= 0.96f;
				player.meleeDamage *= 0.96f;
				player.minionDamage *= 0.96f;
				player.rangedDamage *= 0.96f;
				player.thrownDamage *= 0.96f;
				player.statDefense -= 2;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.5f)
			{
				modPlayer.druidDamage *= 0.96f;
				player.magicDamage *= 0.96f;
				player.meleeDamage *= 0.96f;
				player.minionDamage *= 0.96f;
				player.rangedDamage *= 0.96f;
				player.thrownDamage *= 0.96f;
				player.statDefense -= 4;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.25f)
			{
				modPlayer.druidDamage *= 0.96f;
				player.magicDamage *= 0.96f;
				player.meleeDamage *= 0.96f;
				player.minionDamage *= 0.96f;
				player.rangedDamage *= 0.96f;
				player.thrownDamage *= 0.96f;
				player.statDefense -= 4;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.1f)
			{
				modPlayer.druidDamage *= 0.96f;
				player.magicDamage *= 0.96f;
				player.meleeDamage *= 0.96f;
				player.minionDamage *= 0.96f;
				player.rangedDamage *= 0.96f;
				player.thrownDamage *= 0.96f;
				player.statDefense -= 4;
			}
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.05f)
			{
				modPlayer.druidDamage *= 0.8f;
				player.magicDamage *= 0.8f;
				player.meleeDamage *= 0.8f;
				player.minionDamage *= 0.8f;
				player.rangedDamage *= 0.8f;
				player.thrownDamage *= 0.8f;
				player.statDefense -= 4;
			}
		}
	}
}
