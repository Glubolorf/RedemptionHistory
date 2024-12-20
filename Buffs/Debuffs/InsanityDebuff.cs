using System;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class InsanityDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Insanity");
			base.Description.SetDefault("\"They are coming.\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = 1;
			if (player.statLife > 1)
			{
				player.statLife = 1;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()))
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
