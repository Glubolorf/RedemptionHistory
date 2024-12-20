using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class TimestopDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Time Shift");
			base.Description.SetDefault("\"Time has slowed down!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.velocity.X = 0.5f;
			player.velocity.Y = 0.5f;
		}
	}
}
