using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.NPCBuffs
{
	public class HitActivation : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("hitted");
			base.Description.SetDefault("\"How are you reading this?\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
