using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Cooldowns
{
	public class SwordRemoteCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sword Remote Cooldown");
			base.Description.SetDefault("\"You cannot use the Sword Remote\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
