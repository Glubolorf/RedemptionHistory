using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class FatigueDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Fatigue");
			base.Description.SetDefault("\"You are overwhelmed with tiredness.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.blackout = true;
			player.blind = true;
			player.moveSpeed *= 0.4f;
		}
	}
}
