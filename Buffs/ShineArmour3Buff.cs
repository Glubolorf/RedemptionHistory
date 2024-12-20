using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ShineArmour3Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shined Armor III");
			base.Description.SetDefault("\"Greatly increased defense\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense += 16;
		}
	}
}
