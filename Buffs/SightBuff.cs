using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SightBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Aura of Sight");
			base.Description.SetDefault("\"Vision increased, you can detect creatures and traps\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.dangerSense = true;
			player.detectCreature = true;
			player.nightVision = true;
		}
	}
}
