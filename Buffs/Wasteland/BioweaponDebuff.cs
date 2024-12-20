using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Wasteland
{
	public class BioweaponDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Cellular Destruction");
			base.Description.SetDefault("\"Losing life.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<RedePlayer>().bioweaponDebuff = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().bioweaponDebuff = true;
		}
	}
}
