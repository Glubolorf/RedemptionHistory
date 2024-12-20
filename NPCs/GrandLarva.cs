using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class GrandLarva : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Grand Larva");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 18;
			base.npc.defense = 1;
			base.npc.lifeMax = 50;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 428;
			base.npc.catchItem = (short)base.mod.ItemType("GrandLarvaBait");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				NPC.NewNPC((int)base.npc.position.X + 22, (int)base.npc.position.Y + 10, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 16, (int)base.npc.position.Y + 10, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 12, (int)base.npc.position.Y + 6, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 24, (int)base.npc.position.Y + 12, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 28, (int)base.npc.position.Y + 14, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 8, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 10, (int)base.npc.position.Y + 14, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * 0.02f;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
	}
}
