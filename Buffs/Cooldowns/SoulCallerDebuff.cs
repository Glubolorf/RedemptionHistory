using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Cooldowns
{
	public class SoulCallerDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lunatic Vision");
			base.Description.SetDefault("\"You cannot use the Ancient Worshipper's Talisman\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
