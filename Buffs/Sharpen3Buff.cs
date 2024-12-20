using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class Sharpen3Buff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Sharpened III");
			base.Description.SetDefault("\"Melee weapons have extreme armor penetration\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.armorPenetration += 32;
		}
	}
}
