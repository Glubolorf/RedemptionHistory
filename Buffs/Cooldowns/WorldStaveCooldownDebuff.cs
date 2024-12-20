using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Cooldowns
{
	public class WorldStaveCooldownDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("World Stave Cooldown");
			base.Description.SetDefault("\"You cannot use world staves\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
