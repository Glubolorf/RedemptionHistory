using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NecroticGouge : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Necrotic Gouge");
			base.Description.SetDefault("");
			Main.buffNoSave[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().necroGouge = true;
		}
	}
}
