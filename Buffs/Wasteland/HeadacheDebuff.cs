using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class HeadacheDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Headache");
			base.Description.SetDefault("\"Sudden and pounding pain from the inside of your skull.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.blackout = true;
			player.confused = true;
			player.dazed = true;
		}
	}
}
