using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
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
			base.npc.damage = 30;
			base.npc.defense = 0;
			base.npc.lifeMax = 1000;
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
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 18, (int)base.npc.position.Y + 16, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 36, (int)base.npc.position.Y + 10, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 28, (int)base.npc.position.Y + 8, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 14, (int)base.npc.position.Y + 28, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 32, (int)base.npc.position.Y + 20, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 42, (int)base.npc.position.Y + 30, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 24, (int)base.npc.position.Y + 10, ModContent.NPCType<SludgyBoi2>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 18, ModContent.NPCType<SludgyBoi2>(), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
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
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 300f && Main.rand.Next(200) == 0)
			{
				int minion = NPC.NewNPC((int)base.npc.position.X + 28, (int)base.npc.position.Y + 8, ModContent.NPCType<SludgyBlob>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || Main.expertMode)
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}
	}
}
