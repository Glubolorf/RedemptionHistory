using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class LivingBloom : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Bloom");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 56;
			base.npc.defense = 1;
			base.npc.damage = 7;
			base.npc.lifeMax = 22;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 1;
			this.aiType = 1;
			this.animationType = 302;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("LivingBloomBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/LivingBloomGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/LivingBloomGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/LivingBloomGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/LivingBloomGore3"), 1f);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().forestFriendly;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySlime.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 2 && !RedeWorld.downedPatientZero) ? 0.1f : 0f);
		}
	}
}
