using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.NPCBuffs
{
	public class Electrified : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Electrified");
			Main.debuff[base.Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().electrified = true;
		}
	}
}
