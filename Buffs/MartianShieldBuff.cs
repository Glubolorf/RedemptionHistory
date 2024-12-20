using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class MartianShieldBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Martian Shield");
			base.Description.SetDefault("\"Protected by an alien forcefield\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
		}
	}
}
