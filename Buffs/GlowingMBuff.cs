using System;
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
			DruidDamagePlayer.ModPlayer(player);
			player.lifeRegen++;
			player.manaRegen += 2;
			player.statDefense += 4;
			player.allDamage *= 1.05f;
		}
	}
}
