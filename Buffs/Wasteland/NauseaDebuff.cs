using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class NauseaDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Nausea");
			base.Description.SetDefault("\"You feel sick.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.manaSick = true;
			player.confused = true;
			player.moveSpeed *= 0.5f;
			player.venom = true;
			player.blind = true;
			player.stinky = true;
		}
	}
}
