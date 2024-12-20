using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.NPCBuffs
{
	public class JanitorStun : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Janitor Stun");
			base.Description.SetDefault("\"How are you reading this? This is only for The Janitor\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
