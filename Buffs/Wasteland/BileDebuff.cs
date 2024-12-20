using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class BileDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Burning Acid");
			base.Description.SetDefault("\"Reduced defence.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().bileDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().bileDebuff = true;
		}
	}
}
