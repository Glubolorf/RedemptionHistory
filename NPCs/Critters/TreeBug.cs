using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Critters;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Critters
{
	public class TreeBug : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tree Bug");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 20;
			base.npc.defense = 0;
			base.npc.lifeMax = 15;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath16;
			base.npc.knockBackResist = 0.2f;
			base.npc.npcSlots = 0f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 219;
			base.npc.friendly = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<TreeBugBanner>();
			base.npc.catchItem = (short)ModContent.ItemType<TreeBugItem>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/TreeBugGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/TreeBugGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/TreeBugGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/TreeBugGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/TreeBugGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool? CanBeHitByItem(Player player, Item item)
		{
			return new bool?(true);
		}

		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
			return new bool?(true);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayGrassCritter.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 2) ? 1.7f : 0f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
	}
}
