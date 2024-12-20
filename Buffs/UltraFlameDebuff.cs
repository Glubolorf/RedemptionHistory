using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class UltraFlameDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Melting!");
			base.Description.SetDefault("\"You are slowly melting\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>(base.mod).ultraFlames = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>(base.mod).ultraFlames = true;
		}
	}
}
