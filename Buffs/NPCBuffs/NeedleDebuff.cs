using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.NPCBuffs
{
	public class NeedleDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Needle'd!");
			base.Description.SetDefault("Losing life");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().needleStab = true;
		}
	}
}
