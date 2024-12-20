using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class BobTheBlob : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bob the Blob");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 38;
			base.npc.height = 38;
			base.npc.friendly = false;
			base.npc.damage = 200;
			base.npc.defense = 0;
			base.npc.lifeMax = 50000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(0, 15, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 1;
			this.aiType = 85;
			this.animationType = 302;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[dustIndex3].velocity *= 1.6f;
		}

		private int speedTimer;
	}
}
