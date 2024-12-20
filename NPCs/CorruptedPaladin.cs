﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class CorruptedPaladin : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Paladin");
			Main.npcFrameCount[base.npc.type] = 12;
		}

		public override void SetDefaults()
		{
			base.npc.width = 66;
			base.npc.height = 68;
			base.npc.friendly = false;
			base.npc.damage = 250;
			base.npc.defense = 60;
			base.npc.lifeMax = 6500;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = (float)Item.buyPrice(0, 5, 0, 0);
			base.npc.knockBackResist = 0.01f;
			base.npc.aiStyle = 3;
			this.aiType = 290;
			this.animationType = 290;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("CorruptedPaladinBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedPaladinGore7"), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 266, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 266, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((NPC.downedPlantBoss && RedeWorld.downedInfectedEye && Main.hardMode) ? 0.005f : 0f);
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = 0;
			if (base.npc.spriteDirection == 1)
			{
				spriteEffects = 1;
			}
			spriteBatch.Draw(base.mod.GetTexture("NPCs/CorruptedPaladin_Glow"), new Vector2(base.npc.Center.X - Main.screenPosition.X, base.npc.Center.Y - Main.screenPosition.Y), new Rectangle?(base.npc.frame), Color.White, base.npc.rotation, new Vector2((float)base.npc.width * 0.5f, (float)base.npc.height * 0.5f), 1f, spriteEffects, 0f);
		}
	}
}
