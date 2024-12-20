using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class PillSickness : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Pill Sickness");
			base.Description.SetDefault("\"You feel sick\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.blind = true;
			player.chilled = true;
		}
	}
}
