using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SpiceOrangeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Tangy Spices");
			base.Description.SetDefault("\"Your weapon will ignite enemies\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.AddBuff(74, 2, true);
		}
	}
}
