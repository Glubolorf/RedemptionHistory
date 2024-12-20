using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class FireForceBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Fire Force");
			base.Description.SetDefault("\"Burning crystals empower your body\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 8;
			player.jumpBoost = true;
		}
	}
}
