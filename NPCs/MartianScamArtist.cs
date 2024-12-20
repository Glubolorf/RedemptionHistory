using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class MartianScamArtist : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Martian Scam Artist");
			Main.npcFrameCount[base.npc.type] = 9;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 85;
			base.npc.defense = 35;
			base.npc.lifeMax = 900;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 50f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 3;
			this.aiType = 385;
			this.animationType = 385;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<MartianSABanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MartianSAGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MartianSAGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MartianSAGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.MartianMadness.Chance * 0.04f;
		}
	}
}
