using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DruidsBlessing : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Druid's Blessing");
			base.Description.SetDefault("\"The power of the Creation Druid blesses you\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 8;
			player.jumpBoost = true;
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.magicDamage *= 1.06f;
			player.meleeDamage *= 1.06f;
			player.minionDamage *= 1.06f;
			player.rangedDamage *= 1.06f;
			player.thrownDamage *= 1.06f;
			druidDamagePlayer.druidDamage *= 1.06f;
			player.endurance += 0.06f;
			player.moveSpeed += 25f;
		}
	}
}
