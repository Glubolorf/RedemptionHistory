﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class SandDustDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Dusted");
			base.Description.SetDefault("\"Defense slightly decreased!\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().sandDust = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().sandDust = true;
		}
	}
}
