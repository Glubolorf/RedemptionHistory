using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AncientForcefieldBuff2 : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Forcefield");
			base.Description.SetDefault("\"You are being protected by ancient powers\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player);
			player.allDamage *= 1.06f;
			player.endurance += 0.06f;
		}
	}
}
