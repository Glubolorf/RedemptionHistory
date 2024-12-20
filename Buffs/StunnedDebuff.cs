using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class StunnedDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Stunned!");
			base.Description.SetDefault("\"You are stunned!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.velocity.X = player.velocity.X * 0f;
			player.velocity.Y = player.velocity.Y * 0f;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.velocity.X = npc.velocity.X * 0f;
			npc.velocity.Y = npc.velocity.Y * 0f;
		}
	}
}
