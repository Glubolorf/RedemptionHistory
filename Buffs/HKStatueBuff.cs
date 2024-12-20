using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class HKStatueBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("...");
			base.Description.SetDefault("\"You feel like you're being watched...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
