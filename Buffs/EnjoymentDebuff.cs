using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class EnjoymentDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Enjoyment");
			base.Description.SetDefault("\"Stats greatly decreased due to radioactivity\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>(base.mod).enjoyment = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>(base.mod).enjoyment = true;
		}
	}
}
