using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class SmashedDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Smashed!");
			base.Description.SetDefault("\"\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().smashed = true;
		}
	}
}
