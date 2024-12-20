using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PreHM
{
	public class ForestGolem : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Golem");
			Main.npcFrameCount[base.npc.type] = 21;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 62;
			base.npc.damage = 10;
			base.npc.friendly = false;
			base.npc.defense = 5;
			base.npc.lifeMax = 45;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.4f;
			base.npc.aiStyle = 3;
			this.aiType = 482;
			this.animationType = 482;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<ForestGolemBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGore6"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 2, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/ForestGolemGoreWood"), 1f);
		}

		public override void AI()
		{
			if (!this.change)
			{
				int num = Main.rand.Next(5);
				if (num == 0)
				{
					base.npc.SetDefaults(ModContent.NPCType<ForestGolemBlooming>(), -1f);
					this.change = true;
				}
				if (num == 1)
				{
					base.npc.SetDefaults(ModContent.NPCType<ForestGolemWounded>(), -1f);
					this.change = true;
				}
				if (num >= 2)
				{
					this.change = true;
				}
			}
			if (Main.raining)
			{
				this.regenTimer++;
				if (this.regenTimer >= 40 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
			if (base.npc.wet && !base.npc.lavaWet)
			{
				this.regenTimer++;
				if (this.regenTimer >= 30 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().forestFriendly;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.raining)
			{
				return SpawnCondition.OverworldNight.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 2 && !RedeWorld.downedPatientZero) ? 0.09f : 0f);
			}
			return SpawnCondition.OverworldNight.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 2 && !RedeWorld.downedPatientZero) ? 0.04f : 0f);
		}

		private bool change;

		private int regenTimer;
	}
}
