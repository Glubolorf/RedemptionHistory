using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class FreezeForceBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Freeze Force");
			base.Description.SetDefault("\"Freezing crystals empower your body\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 16;
			player.jumpBoost = true;
		}
	}
}
