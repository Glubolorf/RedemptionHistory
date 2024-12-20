using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class LotusBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lotus Empowerment");
			base.Description.SetDefault("\"Increased druidic crit, decreased all other crit\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			DruidDamagePlayer.ModPlayer(player).druidCrit += 20;
			player.magicCrit -= 40;
			player.meleeCrit -= 40;
			player.rangedCrit -= 40;
			player.thrownCrit -= 40;
		}
	}
}
