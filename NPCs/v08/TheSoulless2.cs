﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class TheSoulless2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Soulless");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 52;
			base.npc.height = 66;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 20000;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 0;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/Soulless");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].velocity *= 2.6f;
				}
			}
			int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 1f);
			Main.dust[num2].velocity *= 1.6f;
		}

		public override void NPCLoot()
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(2) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(4) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("ShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(4) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-70, 0), base.mod.NPCType("ShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (Main.rand.Next(4) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-26, 26), (int)base.npc.position.Y + Main.rand.Next(-72, 0), base.mod.NPCType("ShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
				Main.NewText("Qua sudki uque ka rauin vou huto sik'ka hull'. Okijo, uf il comae, okijo.", Color.DarkGray.R, Color.DarkGray.G, Color.DarkGray.B, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("BlackenedHeart"), 1, false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 78;
				if (base.npc.frame.Y > 234)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * (RedeWorld.downedPatientZero ? 0.0003f : 0f);
		}
	}
}