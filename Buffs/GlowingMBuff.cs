using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GlowingMBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spores: Glowing Mushrooms");
			base.Description.SetDefault("\"Minor increase to all stats\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.lifeRegen += 5;
			player.manaRegen += 5;
			player.statDefense += 2;
			player.magicDamage *= 1.04f;
			player.meleeDamage *= 1.04f;
			player.minionDamage *= 1.04f;
			player.rangedDamage *= 1.04f;
			player.thrownDamage *= 1.04f;
			druidDamagePlayer.druidDamage *= 1.04f;
		}
	}
}
