using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ShineArmour2Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shined Armor II");
			base.Description.SetDefault("\"Increased defense\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 8;
		}
	}
}
