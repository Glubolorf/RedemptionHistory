using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NoitaBombCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Bomb Cooldown");
			base.Description.SetDefault("\"You cannot use the Pommisauva\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
