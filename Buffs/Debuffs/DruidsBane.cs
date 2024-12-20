using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class DruidsBane : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Druid's Bane");
			base.Description.SetDefault("\"The power of the Creation Druid compels you\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().druidBane = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().druidBane = true;
		}
	}
}
