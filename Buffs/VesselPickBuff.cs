using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class VesselPickBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shadesurge");
			base.Description.SetDefault("\"Your mining speed is greatly increased\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
		}
	}
}
