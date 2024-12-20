using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AncientForcefieldBuff : ModBuff
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
			player.statDefense += 6;
			player.endurance += 0.06f;
		}
	}
}
