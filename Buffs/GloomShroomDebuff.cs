using System;
using Redemption.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GloomShroomDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spores: Gloom Shroom");
			base.Description.SetDefault("\"Gloom spores are consuming your skin\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -5;
			player.bleed = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<RedeGlobalNPC>().gloomShroom = true;
		}
	}
}
