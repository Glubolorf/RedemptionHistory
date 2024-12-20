using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ChainedDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Chained!");
			base.Description.SetDefault("\"You can't escape!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.velocity = Vector2.Zero;
			player.maxFallSpeed = 0f;
			player.position = player.oldPosition;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().chained = true;
		}
	}
}
