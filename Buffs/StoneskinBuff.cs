﻿using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class StoneskinBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Stoneskin");
			base.Description.SetDefault("");
			Main.buffNoSave[base.Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().stoneskin = true;
		}
	}
}