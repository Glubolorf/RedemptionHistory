using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ShadeboundBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shadebound");
			base.Description.SetDefault("\"The Spirit Realm cries to you\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}
	}
}
