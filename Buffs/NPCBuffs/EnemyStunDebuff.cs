using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.NPCBuffs
{
	public class EnemyStunDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Stunned!");
			base.Description.SetDefault("\"You are stunned!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			if (!npc.boss)
			{
				npc.velocity.X = 0f;
				if (!npc.noGravity && npc.velocity.Y < 0f)
				{
					npc.velocity.Y = 0f;
				}
				else
				{
					npc.velocity.Y = 0f;
				}
				npc.position = npc.oldPosition;
			}
			npc.defense -= 12;
		}
	}
}
