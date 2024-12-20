using System;
using Microsoft.Xna.Framework;
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
			player.wingTime = 0f;
			player.velocity = Vector2.Zero;
			player.position = player.oldPosition;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.velocity = Vector2.Zero;
			npc.position = npc.oldPosition;
		}
	}
}
