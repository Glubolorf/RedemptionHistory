﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class ForestGolemWounded : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Golem");
			Main.npcFrameCount[base.npc.type] = 20;
		}

		public override void SetDefaults()
		{
			base.npc.width = 60;
			base.npc.height = 56;
			base.npc.damage = 15;
			base.npc.friendly = false;
			base.npc.defense = 7;
			base.npc.lifeMax = 40;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 20f;
			base.npc.knockBackResist = 0.4f;
			base.npc.aiStyle = 3;
			this.aiType = 482;
			this.animationType = 482;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("ForestGolemBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGore8"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ForestGolemGoreWood"), 1f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNight.Chance * 0.02f;
		}
	}
}