using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class InfectedZombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Zombie");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 34;
			base.npc.height = 46;
			base.npc.friendly = false;
			base.npc.damage = 40;
			base.npc.defense = 16;
			base.npc.lifeMax = 120;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 60f;
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 3;
			this.aiType = 3;
			this.animationType = 3;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<InfectedZombieBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedZombieGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedZombieGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedZombieGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedZombieGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * (RedeWorld.downedXenomiteCrystal ? 0.03f : 0f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}
	}
}
