using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class BlackenedHeartDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Soulless");
			base.Description.SetDefault("\"...\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 -= 400;
			player.lifeRegen -= 400;
			player.blind = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().blackHeart = true;
		}
	}
}
