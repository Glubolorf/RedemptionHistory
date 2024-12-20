using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Cooldowns
{
	public class SoulScrollCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Soul Scroll Cooldown");
			base.Description.SetDefault("\"You cannot use the Soul Scroll\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
