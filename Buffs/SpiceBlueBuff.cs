using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SpiceBlueBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Glowing Spices");
			base.Description.SetDefault("\"You emit light\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.AddBuff(11, 2, true);
		}
	}
}
