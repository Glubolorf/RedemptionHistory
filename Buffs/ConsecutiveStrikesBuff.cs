using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ConsecutiveStrikesBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Consecutive Strikes");
			base.Description.SetDefault("\"Continue to hit enemies to get a bonus\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().consecutiveStrikes = true;
		}
	}
}
