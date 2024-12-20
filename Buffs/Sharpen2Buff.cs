using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class Sharpen2Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sharpened II");
			base.Description.SetDefault("\"Melee weapons have high armor penetration\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.armorPenetration += 16;
		}
	}
}
