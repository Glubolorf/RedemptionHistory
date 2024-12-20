using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class EvilJellyBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Evil Jelly");
			base.Description.SetDefault("\"Lures the Dark Slime\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}
	}
}
