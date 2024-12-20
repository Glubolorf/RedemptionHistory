using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	[AutoloadBossHead]
	public class DarkSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Slime");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 142;
			base.npc.height = 92;
			base.npc.friendly = false;
			base.npc.damage = 100;
			base.npc.defense = 0;
			base.npc.lifeMax = 9500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 400f;
			base.npc.alpha = 50;
			base.npc.buffImmune[20] = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 87;
			this.aiType = 473;
			this.animationType = 16;
			base.npc.boss = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/DSGore7"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 98, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 86, (int)base.npc.position.Y + 62, base.mod.NPCType("Slicer"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			this.timer++;
			if (this.timer == 1)
			{
				string text = "A Dark Slime slumbers...";
				Color rarityPurple = Colors.RarityPurple;
				byte r = rarityPurple.R;
				Color rarityPurple2 = Colors.RarityPurple;
				byte g = rarityPurple2.G;
				Color rarityPurple3 = Colors.RarityPurple;
				Main.NewText(text, r, g, rarityPurple3.B, false);
			}
			if (Main.expertMode)
			{
				if (base.npc.life <= 18999)
				{
					this.timer2++;
					if (this.timer2 == 1)
					{
						string text2 = "The Dark Slime has awoken...";
						Color rarityPurple4 = Colors.RarityPurple;
						byte r2 = rarityPurple4.R;
						Color rarityPurple5 = Colors.RarityPurple;
						byte g2 = rarityPurple5.G;
						Color rarityPurple6 = Colors.RarityPurple;
						Main.NewText(text2, r2, g2, rarityPurple6.B, false);
					}
					if (Main.rand.Next(200) == 0)
					{
						NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSlimeling"), 0, 0f, 0f, 0f, 0f, 255);
						return;
					}
				}
			}
			else if (base.npc.life <= 9499)
			{
				this.timer2++;
				if (this.timer2 == 1)
				{
					string text3 = "The Dark Slime has awoken...";
					Color rarityPurple7 = Colors.RarityPurple;
					byte r3 = rarityPurple7.R;
					Color rarityPurple8 = Colors.RarityPurple;
					byte g3 = rarityPurple8.G;
					Color rarityPurple9 = Colors.RarityPurple;
					Main.NewText(text3, r3, g3, rarityPurple9.B, false);
				}
				if (Main.rand.Next(200) == 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 70, base.mod.NPCType("DarkSlimeling"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			name = "A Dark Slime";
			potionType = 154;
			RedeWorld.downedDarkSlime = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDay.Chance * ((Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !RedeWorld.downedDarkSlime && !NPC.AnyNPCs(base.mod.NPCType("DarkSlime"))) ? 0.003f : 0f);
		}

		private Player player;

		public int timer;

		public int timer2;
	}
}
