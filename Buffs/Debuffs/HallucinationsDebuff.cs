using System;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class HallucinationsDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Hallucinations");
			base.Description.SetDefault("\"You start seeing things that aren't there...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()) || player.HasBuff(ModContent.BuffType<ParanoiaDebuff>()) || player.HasBuff(ModContent.BuffType<PsychosisDebuff>()) || player.HasBuff(ModContent.BuffType<InsanityDebuff>()))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
