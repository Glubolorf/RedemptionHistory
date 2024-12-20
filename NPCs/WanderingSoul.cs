using System;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class WanderingSoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wandering Soul");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 66;
			base.npc.height = 80;
			base.npc.friendly = false;
			base.npc.damage = 22;
			base.npc.defense = 0;
			base.npc.lifeMax = 60;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = (float)Item.buyPrice(0, 0, 1, 0);
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = 22;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 316;
			this.animationType = 316;
			base.npc.alpha = 100;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<WanderingSoulBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 38, (int)base.npc.position.Y + 32, ModContent.NPCType<LostSoul2>(), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * (RedeWorld.downedTheKeeper ? 0.006f : 0f);
		}
	}
}
