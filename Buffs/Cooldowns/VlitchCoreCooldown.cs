using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Cooldowns
{
	public class VlitchCoreCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Core Cooldown");
			base.Description.SetDefault("\"Mini-Vlitch Core needs some time to repair\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
