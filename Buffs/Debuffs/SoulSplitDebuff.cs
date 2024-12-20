using System;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class SoulSplitDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Soul Split");
			base.Description.SetDefault("\"Your soul has fragmented!\"");
			Main.debuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			int CageCount = NPC.CountNPCS(ModContent.NPCType<CagedSoul>());
			bool NoSoul = player.ownedProjectileCounts[ModContent.ProjectileType<PlayerSoulPro>()] <= 0;
			if (CageCount > 0)
			{
				player.statLifeMax2 = (int)((float)player.statLifeMax2 * (1f - (float)CageCount / 10f));
			}
			if (CageCount == 0 && NoSoul)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
