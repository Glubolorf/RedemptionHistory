using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class BarkskinBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Barkskin");
			base.Description.SetDefault("");
			Main.buffNoSave[base.Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().barkskin = true;
		}
	}
}
