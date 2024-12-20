using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DreamsongBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Dreamsong");
			base.Description.SetDefault("\"Your vision is increased in the Soulless Caverns\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().dreamSong = true;
		}
	}
}
