using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class LickyLickyCactus : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Devil's Tongue");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 30;
			base.npc.height = 72;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 28;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 0;
			this.animationType = 34;
		}

		public override void NPCLoot()
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 55, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
			}
			NPC.NewNPC((int)base.npc.position.X, (int)base.npc.position.Y + 70, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)base.npc.position.X - 20, (int)base.npc.position.Y + 10, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y, base.mod.NPCType("Fly"), 0, 0f, 0f, 0f, 0f, 255);
		}

		public override void AI()
		{
			this.timer++;
			if (this.timer >= 250)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X + 16f, base.npc.position.Y + 44f), new Vector2(0f, 0f), base.mod.ProjectileType("PollenCloud8"), 8, 3f, 255, 0f, 0f);
				this.timer = 0;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayDesert.Chance * 0.05f;
		}

		public int timer;
	}
}
