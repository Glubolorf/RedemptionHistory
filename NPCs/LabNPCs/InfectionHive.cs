using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class InfectionHive : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infection Hive");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 42;
			base.npc.height = 38;
			base.npc.friendly = false;
			base.npc.damage = 50;
			base.npc.defense = 0;
			base.npc.lifeMax = 2000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 70; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 18, (int)base.npc.position.Y + 16, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 36, (int)base.npc.position.Y + 10, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 28, (int)base.npc.position.Y + 8, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 14, (int)base.npc.position.Y + 28, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 32, (int)base.npc.position.Y + 20, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 42, (int)base.npc.position.Y + 30, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 24, (int)base.npc.position.Y + 10, base.mod.NPCType("SludgyBoi2"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 18, base.mod.NPCType("SludgyBoi2"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 36;
				if (base.npc.frame.Y > 108)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 300f && Main.rand.Next(200) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 28, (int)base.npc.position.Y + 8, base.mod.NPCType("SludgyBlob"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}
	}
}
