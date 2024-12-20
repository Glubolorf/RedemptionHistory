using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class DirtPile : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Suspicious Dirt Pile");
		}

		public override void SetDefaults()
		{
			base.npc.width = 56;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 50;
			base.npc.HitSound = SoundID.NPCHit11;
			base.npc.DeathSound = SoundID.NPCHit11;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
		}

		public override void NPCLoot()
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			}
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 34, (int)base.npc.position.Y + 20, base.mod.NPCType("Newb"), 0, 0f, 0f, 0f, 0f, 255);
			}
			RedeWorld.foundNewb = true;
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 2, Main.rand.Next(2, 8), false, 0, false, false);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 0, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((!RedeWorld.foundNewb && !NPC.AnyNPCs(base.mod.NPCType("DirtPile"))) ? 0.004f : 0f);
		}
	}
}
