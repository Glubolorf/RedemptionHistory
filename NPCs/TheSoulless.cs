using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class TheSoulless : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Soulless");
			Main.npcFrameCount[base.npc.type] = 28;
		}

		public override void SetDefaults()
		{
			base.npc.width = 58;
			base.npc.height = 76;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 2000;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 0;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/Soulless");
		}

		public override void NPCLoot()
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 55, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
			}
			NPC.NewNPC((int)base.npc.position.X, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)base.npc.position.X - 20, (int)base.npc.position.Y + 10, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)base.npc.position.X - 30, (int)base.npc.position.Y + 60, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
			Main.NewText("Ka senkar�ned Nyht il olv Ii�s Syht, Lite�nin ka Lyht exum ka Fryht, Eui ka Myht ut ka Holy Knight.", Color.DarkGray.R, Color.DarkGray.G, Color.DarkGray.B, false);
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 2.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 78;
				if (base.npc.frame.Y > 2106)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((!RedeWorld.downedPatientZero) ? 0.0003f : 0f);
		}
	}
}
