using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class FeverDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Fever");
			base.Description.SetDefault("\"You feel extremely ill.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.manaSick = true;
			player.venom = true;
			player.blind = true;
			player.bleed = true;
			player.chilled = true;
		}
	}
}
