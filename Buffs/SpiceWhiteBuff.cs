using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SpiceWhiteBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Salty Spices");
			base.Description.SetDefault("\"Increases damage reduction\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.endurance += 0.1f;
		}
	}
}
