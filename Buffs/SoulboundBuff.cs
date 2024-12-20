using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SoulboundBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Soulbound");
			base.Description.SetDefault("\"The Spirit Realm calls to you\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}
	}
}
