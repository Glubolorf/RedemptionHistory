using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class UndeadExecutioner1 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Undead Executioner");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 34;
			base.npc.height = 42;
			base.npc.friendly = false;
			base.npc.damage = 25;
			base.npc.defense = 8;
			base.npc.lifeMax = 80;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 50f;
			base.npc.knockBackResist = 0.25f;
			base.npc.aiStyle = 3;
			this.aiType = 3;
			this.animationType = 3;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/UndeadExeGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/UndeadExeGore2"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Main.rand.Next(2) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 20, (int)base.npc.position.Y + 30, ModContent.NPCType<LostSoul2>(), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			if (!this.change)
			{
				int num = Main.rand.Next(3);
				if (num == 0)
				{
					base.npc.SetDefaults(ModContent.NPCType<UndeadExecutioner2>(), -1f);
					this.change = true;
				}
				if (num == 1)
				{
					base.npc.SetDefaults(ModContent.NPCType<UndeadExecutioner3>(), -1f);
					this.change = true;
				}
				if (num == 2)
				{
					this.change = true;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * (RedeWorld.downedTheKeeper ? 0.04f : 0f);
		}

		private bool change;
	}
}
