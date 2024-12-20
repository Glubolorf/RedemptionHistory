using System;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.HM
{
	public class LivingBlackGloop : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Black Gloop");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 32;
			base.npc.friendly = false;
			base.npc.damage = 75;
			base.npc.defense = 0;
			base.npc.lifeMax = 5000;
			base.npc.takenDamageMultiplier = 2f;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 60f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 1;
			this.aiType = 16;
			this.animationType = 16;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<LivingBlackGloopBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/LivingBlackGloopGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/LivingBlackGloopGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/LivingBlackGloopGore3"), 1f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * ((Main.hardMode && NPC.downedPlantBoss && RedeWorld.downedSoI) ? 0.03f : 0f);
		}
	}
}
