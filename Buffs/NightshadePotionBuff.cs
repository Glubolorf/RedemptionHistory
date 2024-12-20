using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NightshadePotionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nightshade");
			base.Description.SetDefault("\"Gain stealth and increased damage reduction at night. Does nothing at day\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (!Main.dayTime)
			{
				player.endurance += 0.08f;
				player.shroomiteStealth = true;
			}
		}
	}
}
