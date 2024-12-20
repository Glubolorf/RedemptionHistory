using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class MoltenGolem : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Molten Golem");
			Main.npcFrameCount[base.npc.type] = 20;
		}

		public override void SetDefaults()
		{
			base.npc.width = 58;
			base.npc.height = 70;
			base.npc.damage = 40;
			base.npc.friendly = false;
			base.npc.defense = 18;
			base.npc.lifeMax = 225;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.05f;
			base.npc.aiStyle = 3;
			base.npc.lavaImmune = true;
			base.npc.buffImmune[24] = true;
			this.aiType = 482;
			this.animationType = 482;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<MoltenGolemBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MoltenGolemGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MoltenGolemGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MoltenGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MoltenGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/MoltenGolemGore4"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (Main.rand.Next(4) == 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(50) == 0)
			{
				int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 188, 35, 3f, 255, 0f, 0f);
				Main.projectile[p].netUpdate = true;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Underworld.Chance * (NPC.downedBoss3 ? 0.05f : 0f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(24, 200, true);
			}
		}
	}
}
