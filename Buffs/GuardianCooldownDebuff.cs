using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GuardianCooldownDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nature Guardian Cooldown");
			base.Description.SetDefault("\"You cannot summon Nature Guardians\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
