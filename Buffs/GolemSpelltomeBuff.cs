using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GolemSpelltomeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Enchantment");
			base.Description.SetDefault("\"Lures the Sleeping Stones\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}
	}
}
