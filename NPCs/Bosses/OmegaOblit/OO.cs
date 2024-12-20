using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Placeable.Trophies;
using Redemption.Items.Usable;
using Redemption.Items.Weapons.PostML.Druid.Seedbags;
using Redemption.Items.Weapons.PostML.Magic;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.NPCs.Bosses.VCleaver;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.OmegaOblit
{
	[AutoloadBossHead]
	public class OO : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Obliterator");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 181000;
			base.npc.damage = 280;
			base.npc.defense = 80;
			base.npc.knockBackResist = 0f;
			base.npc.width = 100;
			base.npc.height = 160;
			base.npc.value = (float)Item.buyPrice(0, 8, 75, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.dontTakeDamage = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[ModContent.BuffType<UltraFlameDebuff>()] = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
			this.bossBag = ModContent.ItemType<OmegaOblitBag>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0 && base.npc.ai[1] >= 17f)
			{
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore1"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore2"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore3"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore4"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore5"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore6"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore7"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore8"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore9"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore10"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore11"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore12"), 1f);
				Gore.NewGore(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/OOGore13"), 1f);
				for (int i = 0; i < 120; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
				for (int j = 0; j < 45; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex2].velocity *= 1.8f;
				}
				for (int k = 0; k < 25; k++)
				{
					int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			if (!RedeWorld.downedVlitch3)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> Good job! All Vlitch Overlords have been... Wait...", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedVlitch3 = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk3 && !NPC.AnyNPCs(ModContent.NPCType<VlitchWormHead>()) && !NPC.AnyNPCs(ModContent.NPCType<VlitchCleaver>()) && !RedeWorld.girusCloaked && !RedeConfigClient.Instance.NoLoreElements)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), ModContent.ProjectileType<GirusTalking3>(), 0, 0f, 255, 0f, 0f);
			}
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (!RedeWorld.downedBlisterface && !RedeWorld.downedVolt && !RedeWorld.downedMACE && !RedeWorld.downedPatientZero)
			{
				damage *= 0.8;
			}
			else
			{
				damage *= 0.4;
			}
			return true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchTrophy>(), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<PlasmaJawser>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<OmegaClaw>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GloopContainer>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<SneakloneRemote>(), 1, false, 0, false, false);
			}
			if (Utils.NextBool(Main.rand, 7))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<OOMask>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(25, 35), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(3, 5), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<OblitBrain>(), 1, false, 0, false, false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.repeat);
				writer.Write(this.ID);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.repeat = reader.ReadInt32();
				this.ID = reader.ReadInt32();
			}
		}

		public int ID
		{
			get
			{
				return (int)base.npc.ai[1];
			}
			set
			{
				base.npc.ai[1] = (float)value;
			}
		}

		private void AttackChoice()
		{
			int attempts = 0;
			while (attempts == 0)
			{
				if (this.CopyList == null || this.CopyList.Count == 0)
				{
					this.CopyList = new List<int>(this.AttackList);
				}
				this.ID = this.CopyList[Main.rand.Next(0, this.CopyList.Count)];
				this.CopyList.Remove(this.ID);
				base.npc.netUpdate = true;
				if ((this.ID != 6 || base.npc.life < (int)((float)base.npc.lifeMax * 0.6f)) && (this.ID != 7 || base.npc.life < (int)((float)base.npc.lifeMax * 0.7f)))
				{
					attempts++;
				}
			}
		}

		public override void AI()
		{
			if (!this.title)
			{
				if (!Main.dedServ)
				{
					Redemption.Inst.TitleCardUIElement.DisplayTitle("Omega Obliterator", 60, 90, 0.8f, 0, new Color?(Color.Red), "3rd Vlitch Overlord", true);
				}
				this.title = true;
			}
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.ai[3] == 0f)
			{
				if (base.npc.frameCounter >= 5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 164;
					if (base.npc.frame.Y > 492)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				this.frameCounters = 0;
			}
			else if (base.npc.ai[3] == 1f)
			{
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.pewFrame++;
					this.frameCounters = 0;
				}
				if (this.pewFrame >= 4)
				{
					this.pewFrame = 0;
				}
			}
			else if (base.npc.ai[3] == 2f)
			{
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.chargeFrame++;
					this.frameCounters = 0;
				}
				if (this.chargeFrame >= 3)
				{
					this.chargeFrame = 0;
				}
			}
			else if (base.npc.ai[3] >= 3f)
			{
				this.frameCounters++;
				if (this.frameCounters >= 5)
				{
					this.damagedFrame++;
					this.frameCounters = 0;
				}
				if (this.damagedFrame >= 4)
				{
					this.damagedFrame = 0;
				}
			}
			if (base.npc.ai[3] == 2f)
			{
				if (base.npc.spriteDirection != 1)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X + 80f, base.npc.position.Y + 26f), 12, 12, 235, 0f, 0f, 0, default(Color), 2f);
					Main.dust[dustIndex].noGravity = true;
					Dust dust = Main.dust[dustIndex];
					dust.velocity.X = 8f;
					dust.velocity.Y = 2f;
				}
				else
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), 12, 12, 235, 0f, 0f, 0, default(Color), 2f);
					Main.dust[dustIndex2].noGravity = true;
					Dust dust2 = Main.dust[dustIndex2];
					dust2.velocity.X = -8f;
					dust2.velocity.Y = 2f;
				}
			}
			else if (base.npc.spriteDirection != 1)
			{
				int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X + 80f, base.npc.position.Y + 26f), 12, 2, 235, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustIndex3].noGravity = true;
				Dust dust3 = Main.dust[dustIndex3];
				dust3.velocity.X = 2f;
				dust3.velocity.Y = 4f;
			}
			else
			{
				int dustIndex4 = Dust.NewDust(new Vector2(base.npc.position.X + 4f, base.npc.position.Y + 26f), 12, 2, 235, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustIndex4].noGravity = true;
				Dust dust4 = Main.dust[dustIndex4];
				dust4.velocity.X = -2f;
				dust4.velocity.Y = 4f;
			}
			Vector2 DefaultPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 240f) : (player.Center.X + 240f), player.Center.Y - 80f);
			Vector2 ShootPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 300f) : (player.Center.X + 300f), player.Center.Y - 10f);
			Vector2 ChargePos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 400f) : (player.Center.X + 400f), player.Center.Y);
			Vector2 DefaultPos2 = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -240 : 240), -40f);
			Vector2 LaserPos = new Vector2((base.npc.spriteDirection != 1) ? (base.npc.position.X + 8f) : (base.npc.position.X + 92f), base.npc.position.Y + 51f);
			Vector2 GunPos = new Vector2((base.npc.spriteDirection != 1) ? (base.npc.Center.X - 38f) : (base.npc.Center.X + 38f), base.npc.Center.Y - 11f);
			Vector2 RandPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? Main.rand.Next(-500, -300) : Main.rand.Next(300, 500)), (float)Main.rand.Next(-400, 200));
			Vector2 BottomPos = new Vector2((float)((player.Center.X > base.npc.Center.X) ? -500 : 500), 400f);
			this.DespawnHandler();
			if (base.npc.ai[0] > 1f && base.npc.ai[0] < 6f)
			{
				if (base.npc.ai[0] > 2f && base.npc.ai[0] != 5f)
				{
					base.npc.dontTakeDamage = false;
				}
				if (!this.barrierSpawn)
				{
					int degrees = 0;
					for (int j = 0; j < 36; j++)
					{
						degrees += 10;
						int N2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<OOShield>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[N2].ai[0] = (float)base.npc.whoAmI;
						Main.npc[N2].ai[1] = (float)degrees;
					}
					this.barrierSpawn = true;
				}
				if (Vector2.Distance(base.npc.Center, player.Center) > 1500f && (base.npc.ai[1] != 10f || base.npc.ai[0] != 5f))
				{
					player.AddBuff(144, 10, true);
				}
			}
			if (base.npc.ai[0] < 2f)
			{
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/silence");
			}
			player.GetModPlayer<ScreenPlayer>().ScreenFocusPosition = base.npc.Center;
			switch ((int)base.npc.ai[0])
			{
			case 0:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (Vector2.Distance(base.npc.Center, DefaultPos) < 100f || base.npc.ai[2] > 200f)
				{
					base.npc.ai[2] = 0f;
					if (RedeWorld.oblitDeath == 2 || RedeConfigClient.Instance.NoLoreElements)
					{
						base.npc.ai[0] = 2f;
					}
					else
					{
						base.npc.ai[0] = 1f;
					}
					base.npc.netUpdate = true;
				}
				else
				{
					base.npc.MoveToVector2(DefaultPos, 11f);
				}
				break;
			case 1:
				base.npc.velocity *= 0.96f;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] >= 30f)
				{
					base.npc.velocity *= 0f;
					player.GetModPlayer<ScreenPlayer>().lockScreen = true;
				}
				if (base.npc.ai[2] == 120f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Yo.", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] < 190f || base.npc.ai[2] > 370f)
				{
					base.npc.LookAtPlayer();
				}
				if (base.npc.ai[2] == 190f && player.active && !player.dead)
				{
					base.npc.ai[3] = 1f;
					int p = Projectile.NewProjectile(LaserPos, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), ModContent.ProjectileType<OmegaMegaBeam>(), 333, 0f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					int p2 = Projectile.NewProjectile(LaserPos, new Vector2(0f, 0f), ModContent.ProjectileType<Lasers>(), 0, 0f, 255, 0f, 0f);
					Main.projectile[p2].netUpdate = true;
				}
				if (base.npc.ai[2] == 238f)
				{
					player.GetModPlayer<ScreenPlayer>().Rumble(112, 20);
				}
				if (base.npc.ai[2] == 350f)
				{
					base.npc.ai[3] = 0f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 400f)
				{
					if (RedeWorld.oblitDeath == 1)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "I guess I can't fool you twice, huh.", true, false);
					}
					else
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "So much for a surprise attack...", true, false);
					}
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 580f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Hang on, I got a call from Girus.", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 740f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "'I wasted too much energy too quickly?'", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 840f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "'I'm an idiot?'", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 940f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "You're scrapping my personality drive?", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 1140f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Ah well, request accepted...", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 1380f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "REBOOTING SYSTEMS... GENERATING BARRIER...", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] > 1500f)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/LabSafeS").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					for (int k = 0; k < 100; k++)
					{
						int dustIndex5 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
						Main.dust[dustIndex5].velocity *= 1.9f;
					}
					RedeWorld.oblitDeath = 2;
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
				break;
			case 2:
				base.npc.LookAtPlayer();
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] < 60f)
				{
					base.npc.Move(DefaultPos2, 9f, 10f, true);
				}
				else
				{
					base.npc.velocity *= 0.96f;
				}
				if (base.npc.ai[2] == 60f && !RedeConfigClient.Instance.NoLoreElements)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "TARGET FOUND...", true, false);
				}
				if (base.npc.ai[2] == 120f && !RedeConfigClient.Instance.NoLoreElements)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "PREPARE FOR OBLITERATION...", true, false);
				}
				if (base.npc.ai[2] >= 260f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 3f;
					base.npc.netUpdate = true;
				}
				break;
			case 3:
				base.npc.LookAtPlayer();
				this.frameCounters = 0;
				base.npc.ai[3] = 0f;
				base.npc.ai[0] = 4f;
				base.npc.ai[2] = 0f;
				this.AttackChoice();
				this.MoveVector2 = RandPos;
				base.npc.netUpdate = true;
				break;
			case 4:
				switch (this.ID)
				{
				case 0:
					if (base.npc.ai[3] != 2f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] == 0f)
					{
						base.npc.velocity *= 0f;
						base.npc.ai[2] = 1f;
						base.npc.netUpdate = true;
					}
					else
					{
						base.npc.velocity *= 0.96f;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 2f)
						{
							base.npc.velocity = -base.npc.DirectionTo(player.Center) * 7f;
						}
						if (base.npc.ai[2] == 40f)
						{
							this.Dash(60, true);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] == 100f)
						{
							base.npc.velocity = -base.npc.DirectionTo(player.Center) * 7f;
						}
						if (base.npc.ai[2] == 140f)
						{
							this.Dash(60, true);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 220f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 1:
					if (base.npc.ai[3] != 2f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, ChargePos) < 200f || base.npc.ai[2] >= 60f)
						{
							base.npc.ai[2] = 200f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Move(DefaultPos2, 15f, 10f, true);
						}
					}
					else
					{
						base.npc.velocity *= 0.96f;
						if (base.npc.ai[2] == 205f)
						{
							base.npc.velocity.X = (float)((player.Center.X > base.npc.Center.X) ? -8 : 8);
						}
						if (base.npc.ai[2] == 235f)
						{
							this.Dash(60, false);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 235f && base.npc.ai[2] % 3f == 0f && base.npc.velocity.Length() > 12f)
						{
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<OmegaBlast>(), 90, new Vector2((base.npc.spriteDirection != 1) ? 4f : -4f, 12f), false, SoundID.Item91, "", 0f, 0f);
							base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<OmegaBlast>(), 90, new Vector2((base.npc.spriteDirection != 1) ? 4f : -4f, -12f), false, SoundID.Item91, "", 0f, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 310f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 2:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, ShootPos) < 200f || base.npc.ai[2] >= 40f)
						{
							base.npc.ai[2] = 200f;
							base.npc.ai[3] = 1f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.ai[3] = 2f;
							base.npc.Move(DefaultPos2, 16f, 10f, true);
						}
					}
					else
					{
						base.npc.velocity *= 0.9f;
						base.npc.ai[3] = 1f;
						if (base.npc.ai[2] == 210f)
						{
							base.npc.Shoot(LaserPos, ModContent.ProjectileType<OmegaPlasmaBall>(), 90, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), true, SoundID.Item1, "Sounds/Custom/BallCreate", 0f, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 230f)
						{
							if (this.repeat >= 3)
							{
								this.repeat = 0;
								base.npc.velocity *= 0f;
								base.npc.ai[0] = 3f;
								base.npc.ai[2] = 0f;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
							else
							{
								this.repeat++;
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 0f;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					break;
				case 3:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, ShootPos) < 500f || base.npc.ai[2] >= 40f)
						{
							if (player.Center.Y > base.npc.Center.Y)
							{
								base.npc.velocity.Y = 20f;
							}
							else
							{
								base.npc.velocity.Y = -20f;
							}
							base.npc.ai[2] = 200f;
							base.npc.ai[3] = 1f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.ai[3] = 2f;
							base.npc.Move(DefaultPos2, 19f, 10f, true);
						}
					}
					else
					{
						NPC npc2 = base.npc;
						npc2.velocity.Y = npc2.velocity.Y * 0.98f;
						NPC npc3 = base.npc;
						npc3.velocity.X = npc3.velocity.X * 0.1f;
						base.npc.ai[3] = 1f;
						if (base.npc.ai[2] > 200f && base.npc.ai[2] % 7f == 0f)
						{
							base.npc.Shoot(LaserPos, ModContent.ProjectileType<OmegaPlasmaBall>(), 90, new Vector2((base.npc.spriteDirection != 1) ? -12f : 12f, 0f), true, SoundID.Item1, "Sounds/Custom/BallCreate", 0f, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 270f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 4:
					base.npc.LookAtPlayer();
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, ShootPos) < 500f || base.npc.ai[2] >= 60f)
						{
							base.npc.ai[2] = 200f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.Move(DefaultPos2, 11f, 10f, true);
						}
					}
					else
					{
						base.npc.velocity *= 0.96f;
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.4f))
						{
							if (base.npc.ai[2] > 200f && base.npc.ai[2] % 4f == 0f && base.npc.ai[2] < 320f)
							{
								base.npc.Shoot(new Vector2(player.Center.X + (float)Main.rand.Next(-600, 600), player.Center.Y + (float)Main.rand.Next(-600, 600)), ModContent.ProjectileType<OOCrosshair>(), 160, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
								base.npc.netUpdate = true;
							}
						}
						else if (base.npc.ai[2] > 200f && base.npc.ai[2] % 8f == 0f && base.npc.ai[2] < 320f)
						{
							base.npc.Shoot(new Vector2(player.Center.X + (float)Main.rand.Next(-600, 600), player.Center.Y + (float)Main.rand.Next(-600, 600)), ModContent.ProjectileType<OOCrosshair>(), 160, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 320f)
						{
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 5:
					if (base.npc.ai[2] < 300f)
					{
						base.npc.LookAtPlayer();
						base.npc.netUpdate = true;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.4f))
					{
						if (((base.npc.life < (int)((float)base.npc.lifeMax * 0.2f)) ? (base.npc.ai[2] % 8f == 0f) : (base.npc.ai[2] % 10f == 0f)) && base.npc.ai[2] < 300f)
						{
							base.npc.Shoot(GunPos, ModContent.ProjectileType<OOLaser>(), 90, RedeHelper.PolarVector(12f, Utils.ToRotation(player.Center - base.npc.Center)), true, SoundID.Item1, "Sounds/Custom/Laser1", 0f, 0f);
							base.npc.netUpdate = true;
						}
					}
					else if (base.npc.ai[2] % 15f == 0f && base.npc.ai[2] < 300f)
					{
						base.npc.Shoot(GunPos, ModContent.ProjectileType<OOLaser>(), 90, RedeHelper.PolarVector(12f, Utils.ToRotation(player.Center - base.npc.Center)), true, SoundID.Item1, "Sounds/Custom/Laser1", 0f, 0f);
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 100f || base.npc.ai[2] >= 30f)
						{
							base.npc.netUpdate = true;
							base.npc.ai[2] = 200f;
						}
						else
						{
							base.npc.ai[3] = 2f;
							base.npc.Move(this.MoveVector2, 20f, 10f, true);
						}
					}
					else if (base.npc.ai[2] >= 200f && base.npc.ai[2] < 300f)
					{
						base.npc.velocity *= 0.9f;
						if (base.npc.ai[2] > 210f)
						{
							if (this.repeat >= 4)
							{
								this.repeat = 0;
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 300f;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
							else
							{
								this.repeat++;
								this.MoveVector2 = RandPos;
								base.npc.ai[2] = 0f;
								base.npc.ai[3] = 0f;
								base.npc.netUpdate = true;
							}
						}
					}
					else
					{
						base.npc.MoveToVector2(ShootPos, 8f);
						if (base.npc.ai[2] == 305f)
						{
							base.npc.Shoot(GunPos, ModContent.ProjectileType<OONormalBeam>(), 180, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), true, SoundID.Item1, "Sounds/Custom/Laser1", (float)base.npc.whoAmI, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 420f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 6:
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, BottomPos) < 200f || base.npc.ai[2] >= 80f)
						{
							base.npc.Shoot(GunPos, ModContent.ProjectileType<OOStunBeam>(), 100, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), true, SoundID.Item1, "Sounds/Custom/BallFire", (float)base.npc.whoAmI, 0f);
							base.npc.velocity *= 0f;
							base.npc.ai[2] = 200f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.LookAtPlayer();
							base.npc.ai[3] = 2f;
							base.npc.Move(BottomPos, 30f, 10f, true);
						}
					}
					else
					{
						if (base.npc.ai[2] < 205f)
						{
							NPC npc4 = base.npc;
							npc4.velocity.Y = npc4.velocity.Y - 2f;
						}
						if (base.npc.ai[2] > 200f && base.npc.ai[2] % 6f == 0f && base.npc.ai[2] < 360f && player.active && !player.dead && player.HasBuff(ModContent.BuffType<StaticStunDebuff>()))
						{
							base.npc.Shoot(new Vector2(player.Center.X + (float)Main.rand.Next(-80, 80), player.Center.Y + (float)Main.rand.Next(-80, 80)), ModContent.ProjectileType<OOCrosshair>(), 160, Vector2.Zero, true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] > 300f)
						{
							base.npc.velocity *= 0.98f;
						}
						if (base.npc.ai[2] > 400f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				case 7:
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] < 200f)
					{
						if (Vector2.Distance(base.npc.Center, ChargePos) < 200f || base.npc.ai[2] >= 80f)
						{
							base.npc.ai[3] = 1f;
							base.npc.Shoot(LaserPos, ModContent.ProjectileType<OONormalBeamR>(), 180, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), true, SoundID.Item1, "Sounds/Custom/Laser1", (float)base.npc.whoAmI, 0f);
							base.npc.Shoot(LaserPos, ModContent.ProjectileType<OONormalBeamR2>(), 180, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), true, SoundID.Item1, "Sounds/Custom/Laser1", (float)base.npc.whoAmI, 0f);
							base.npc.velocity *= 0f;
							base.npc.ai[2] = 200f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.LookAtPlayer();
							base.npc.ai[3] = 2f;
							base.npc.MoveToVector2(ChargePos, 30f);
						}
					}
					else
					{
						base.npc.ai[3] = 1f;
						base.npc.MoveToVector2(ChargePos, 6f);
						if (base.npc.ai[2] > 320f)
						{
							base.npc.velocity *= 0.98f;
						}
						if (base.npc.ai[2] > 380f)
						{
							base.npc.velocity *= 0f;
							base.npc.ai[0] = 3f;
							base.npc.ai[2] = 0f;
							base.npc.ai[3] = 0f;
							base.npc.netUpdate = true;
						}
					}
					break;
				}
				break;
			case 5:
				if (base.npc.ai[1] < 10f)
				{
					base.npc.LookAtPlayer();
					this.frameCounters = 0;
					base.npc.ai[3] = 3f;
					base.npc.ai[0] = 5f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 10f;
					this.repeat = 0;
					this.MoveVector2 = RandPos;
					base.npc.dontTakeDamage = true;
					base.npc.netUpdate = true;
					if (Main.netMode == 2 && base.npc.whoAmI < 200)
					{
						NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				else
				{
					base.npc.dontTakeDamage = true;
					base.npc.ai[3] = 3f;
					int dustIndex6 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex6].noGravity = true;
					Dust dust5 = Main.dust[dustIndex6];
					dust5.velocity.X = 0f;
					dust5.velocity.Y = -5f;
					switch ((int)base.npc.ai[1])
					{
					case 10:
						base.npc.dontTakeDamage = true;
						base.npc.LookAtPlayer();
						base.npc.velocity *= 0.8f;
						if (base.npc.ai[2] == 0f)
						{
							Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
							for (int l = 0; l < 40; l++)
							{
								int dustIndex7 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
								Main.dust[dustIndex7].velocity *= 1.9f;
							}
							for (int m = 0; m < 25; m++)
							{
								int dustIndex8 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
								Main.dust[dustIndex8].velocity *= 1.8f;
							}
							for (int n = 0; n < 15; n++)
							{
								int dustIndex9 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
								Main.dust[dustIndex9].velocity *= 1.8f;
							}
							base.npc.ai[2] = 1f;
							if (Main.netMode == 2 && base.npc.whoAmI < 200)
							{
								NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						else
						{
							if (base.npc.ai[2] < 500f)
							{
								player.GetModPlayer<ScreenPlayer>().lockScreen = true;
							}
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] == 60f && !RedeConfigClient.Instance.NoLoreElements)
							{
								CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "SYSTEM OVERLOAD...", true, false);
							}
							if (base.npc.ai[2] == 180f && !RedeConfigClient.Instance.NoLoreElements)
							{
								CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "GIRUS, I REQUIRE ASSISTANCE...", true, false);
							}
							if (base.npc.ai[2] == 300f && !RedeConfigClient.Instance.NoLoreElements)
							{
								CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "MESSAGE FAILED TO SEND...", true, false);
							}
							if (base.npc.ai[2] == 460f)
							{
								Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
								for (int i2 = 0; i2 < 10; i2++)
								{
									int dustIndex10 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
									Main.dust[dustIndex10].velocity *= 1.9f;
								}
								for (int i3 = 0; i3 < 8; i3++)
								{
									int dustIndex11 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
									Main.dust[dustIndex11].velocity *= 1.8f;
								}
								for (int i4 = 0; i4 < 6; i4++)
								{
									int dustIndex12 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
									Main.dust[dustIndex12].velocity *= 1.8f;
								}
								if (!RedeConfigClient.Instance.NoLoreElements)
								{
									CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "OVERHEATING... OVERHEATING... OVERHEATING...", true, false);
								}
							}
							if (base.npc.ai[2] > 560f)
							{
								base.npc.ai[2] = 0f;
								base.npc.ai[1] = 11f;
								base.npc.netUpdate = true;
							}
						}
						break;
					case 11:
						this.overheatAlpha -= 0.05f;
						if (base.npc.velocity.Length() <= 12f)
						{
							base.npc.LookAtPlayer();
							base.npc.netUpdate = true;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 200f)
						{
							if (Vector2.Distance(base.npc.Center, ChargePos) < 200f || base.npc.ai[2] >= 60f)
							{
								base.npc.ai[2] = 200f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.Move(DefaultPos2, 40f, 10f, true);
							}
						}
						else
						{
							base.npc.velocity *= 0.96f;
							if (base.npc.ai[2] == 205f)
							{
								base.npc.velocity.X = (float)((player.Center.X > base.npc.Center.X) ? -8 : 8);
							}
							if (base.npc.ai[2] == 225f)
							{
								this.Dash(60, false);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 225f && base.npc.ai[2] % 3f == 0f && base.npc.velocity.Length() > 12f)
							{
								base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<OmegaBlast>(), 90, new Vector2((base.npc.spriteDirection != 1) ? 5f : -5f, 12f), false, SoundID.Item91, "", 0f, 0f);
								base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<OmegaBlast>(), 90, new Vector2((base.npc.spriteDirection != 1) ? 5f : -5f, -12f), false, SoundID.Item91, "", 0f, 0f);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 260f)
							{
								if (this.repeat >= 1)
								{
									this.repeat = 0;
									base.npc.velocity *= 0f;
									base.npc.ai[2] = 0f;
									base.npc.ai[1] += 1f;
									base.npc.netUpdate = true;
								}
								else
								{
									this.repeat++;
									base.npc.velocity *= 0f;
									base.npc.ai[2] = 0f;
									base.npc.netUpdate = true;
								}
							}
						}
						break;
					case 12:
						this.overheatAlpha -= 0.05f;
						if (base.npc.ai[2] < 300f)
						{
							base.npc.LookAtPlayer();
							base.npc.netUpdate = true;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] % 5f == 0f && base.npc.ai[2] < 300f)
						{
							base.npc.Shoot(GunPos, ModContent.ProjectileType<OOLaser>(), 90, RedeHelper.PolarVector(12f, Utils.ToRotation(player.Center - base.npc.Center)), true, SoundID.Item1, "Sounds/Custom/Laser1", 0f, 0f);
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] < 200f)
						{
							if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 100f || base.npc.ai[2] >= 20f)
							{
								Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
								for (int i5 = 0; i5 < 40; i5++)
								{
									int dustIndex13 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
									Main.dust[dustIndex13].velocity *= 1.9f;
								}
								for (int i6 = 0; i6 < 25; i6++)
								{
									int dustIndex14 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
									Main.dust[dustIndex14].velocity *= 1.8f;
								}
								for (int i7 = 0; i7 < 15; i7++)
								{
									int dustIndex15 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
									Main.dust[dustIndex15].velocity *= 1.8f;
								}
								base.npc.netUpdate = true;
								base.npc.ai[2] = 200f;
							}
							else
							{
								base.npc.Move(this.MoveVector2, 50f, 10f, true);
							}
						}
						else if (base.npc.ai[2] >= 200f && base.npc.ai[2] < 300f)
						{
							base.npc.velocity *= 0.8f;
							if (base.npc.ai[2] > 210f)
							{
								if (this.repeat >= 4)
								{
									this.repeat = 0;
									base.npc.velocity *= 0f;
									base.npc.ai[2] = 300f;
									base.npc.netUpdate = true;
								}
								else
								{
									this.repeat++;
									this.MoveVector2 = RandPos;
									base.npc.ai[2] = 0f;
									base.npc.netUpdate = true;
								}
							}
						}
						else
						{
							base.npc.MoveToVector2(ShootPos, 10f);
							if (base.npc.ai[2] >= 305f && base.npc.ai[2] % 4f == 0f && base.npc.ai[2] <= 355f)
							{
								base.npc.Shoot(GunPos, ModContent.ProjectileType<OONormalBeamS>(), 180, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, Utils.NextFloat(Main.rand, -3f, 3f)), true, SoundID.Item1, "Sounds/Custom/Laser1", (float)base.npc.whoAmI, 0f);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 420f)
							{
								this.repeat = 0;
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 0f;
								base.npc.ai[1] += 1f;
								base.npc.netUpdate = true;
							}
						}
						break;
					case 13:
						this.overheatAlpha -= 0.05f;
						base.npc.LookAtPlayer();
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 200f)
						{
							if (Vector2.Distance(base.npc.Center, ShootPos) < 500f || base.npc.ai[2] >= 30f)
							{
								base.npc.ai[2] = 200f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.Move(DefaultPos2, 25f, 10f, true);
							}
						}
						else
						{
							base.npc.velocity *= 0.96f;
							if (base.npc.ai[2] > 200f && base.npc.ai[2] % 4f == 0f && base.npc.ai[2] < 320f)
							{
								base.npc.Shoot(new Vector2(player.Center.X + (float)Main.rand.Next(-600, 600), player.Center.Y + (float)Main.rand.Next(-600, 600)), ModContent.ProjectileType<OOCrosshair>(), 160, new Vector2(0f, 0f), true, SoundID.Item1, "Sounds/Custom/Alarm2", (float)base.npc.whoAmI, 0f);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 320f)
							{
								this.repeat = 0;
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 0f;
								base.npc.ai[1] += 1f;
								base.npc.netUpdate = true;
							}
						}
						break;
					case 14:
						this.overheatAlpha -= 0.05f;
						base.npc.LookAtPlayer();
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 200f)
						{
							if (Vector2.Distance(base.npc.Center, ShootPos) < 500f || base.npc.ai[2] >= 10f)
							{
								if (player.Center.Y > base.npc.Center.Y)
								{
									base.npc.velocity.Y = 20f;
								}
								else
								{
									base.npc.velocity.Y = -20f;
								}
								base.npc.ai[2] = 200f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.Move(DefaultPos2, 24f, 10f, true);
							}
						}
						else
						{
							NPC npc5 = base.npc;
							npc5.velocity.Y = npc5.velocity.Y * 0.98f;
							NPC npc6 = base.npc;
							npc6.velocity.X = npc6.velocity.X * 0.1f;
							if (base.npc.ai[2] > 200f && base.npc.ai[2] % 5f == 0f)
							{
								base.npc.Shoot(LaserPos, ModContent.ProjectileType<OmegaPlasmaBall>(), 90, new Vector2((base.npc.spriteDirection != 1) ? -18f : 18f, 0f), true, SoundID.Item1, "Sounds/Custom/BallCreate", 0f, 0f);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 240f)
							{
								this.repeat = 0;
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 0f;
								base.npc.ai[1] += 1f;
								base.npc.netUpdate = true;
							}
						}
						break;
					case 15:
						this.overheatAlpha -= 0.05f;
						if (base.npc.velocity.Length() > 12f)
						{
							base.npc.LookAtPlayer();
							base.npc.netUpdate = true;
						}
						if (base.npc.ai[2] == 0f)
						{
							Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
							for (int i8 = 0; i8 < 40; i8++)
							{
								int dustIndex16 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
								Main.dust[dustIndex16].velocity *= 1.9f;
							}
							for (int i9 = 0; i9 < 25; i9++)
							{
								int dustIndex17 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
								Main.dust[dustIndex17].velocity *= 1.8f;
							}
							for (int i10 = 0; i10 < 15; i10++)
							{
								int dustIndex18 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
								Main.dust[dustIndex18].velocity *= 1.8f;
							}
							base.npc.velocity *= 0f;
							base.npc.ai[2] = 1f;
							base.npc.netUpdate = true;
						}
						else
						{
							base.npc.velocity *= 0.96f;
							base.npc.ai[2] += 1f;
							if (base.npc.ai[2] == 2f)
							{
								base.npc.velocity = -base.npc.DirectionTo(player.Center) * 7f;
							}
							if (base.npc.ai[2] == 30f)
							{
								this.Dash(70, true);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] == 80f)
							{
								base.npc.velocity = -base.npc.DirectionTo(player.Center) * 7f;
							}
							if (base.npc.ai[2] == 110f)
							{
								this.Dash(70, true);
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 170f)
							{
								if (this.repeat >= 2)
								{
									this.repeat = 0;
									base.npc.velocity *= 0f;
									base.npc.ai[2] = 0f;
									base.npc.ai[1] += 1f;
									base.npc.netUpdate = true;
								}
								else
								{
									this.repeat++;
									base.npc.velocity *= 0f;
									base.npc.ai[2] = 0f;
									base.npc.netUpdate = true;
								}
							}
						}
						break;
					case 16:
						this.overheatAlpha -= 0.05f;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] < 200f)
						{
							if (Vector2.Distance(base.npc.Center, ShootPos) < 200f || base.npc.ai[2] >= 60f)
							{
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 200f;
								base.npc.netUpdate = true;
							}
							else
							{
								base.npc.MoveToVector2(ShootPos, 24f);
							}
						}
						else
						{
							if (base.npc.ai[2] < 210f || base.npc.ai[2] > 390f)
							{
								base.npc.LookAtPlayer();
							}
							if (base.npc.ai[2] == 210f && player.active && !player.dead)
							{
								int p3 = Projectile.NewProjectile(LaserPos, new Vector2((base.npc.spriteDirection != 1) ? -10f : 10f, 0f), ModContent.ProjectileType<OmegaMegaBeam>(), 333, 0f, 255, 0f, 0f);
								Main.projectile[p3].netUpdate = true;
								int p4 = Projectile.NewProjectile(LaserPos, new Vector2(0f, 0f), ModContent.ProjectileType<Lasers>(), 0, 0f, 255, 0f, 0f);
								Main.projectile[p4].netUpdate = true;
							}
							if (base.npc.ai[2] == 258f)
							{
								player.GetModPlayer<ScreenPlayer>().Rumble(112, 20);
							}
							if (base.npc.ai[2] == 400f)
							{
								Main.PlaySound(SoundID.Item14, (int)base.npc.position.X, (int)base.npc.position.Y);
								for (int i11 = 0; i11 < 40; i11++)
								{
									int dustIndex19 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.7f);
									Main.dust[dustIndex19].velocity *= 1.9f;
								}
								for (int i12 = 0; i12 < 25; i12++)
								{
									int dustIndex20 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
									Main.dust[dustIndex20].velocity *= 1.8f;
								}
								for (int i13 = 0; i13 < 15; i13++)
								{
									int dustIndex21 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
									Main.dust[dustIndex21].velocity *= 1.8f;
								}
								if (!RedeConfigClient.Instance.NoLoreElements)
								{
									CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "CRITICAL CONDITION REACHED... SELF DESTRUCTING...", true, false);
								}
								base.npc.netUpdate = true;
							}
							if (base.npc.ai[2] > 500f)
							{
								base.npc.velocity *= 0f;
								base.npc.ai[2] = 0f;
								base.npc.ai[1] += 1f;
								base.npc.netUpdate = true;
							}
						}
						break;
					case 17:
						base.npc.dontTakeDamage = false;
						player.ApplyDamageToNPC(base.npc, 9999, 0f, 0, false);
						base.npc.netUpdate = true;
						if (Main.netMode == 2 && base.npc.whoAmI < 200)
						{
							NetMessage.SendData(23, -1, -1, null, base.npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
						}
						break;
					}
				}
				break;
			}
			if (base.npc.velocity.Length() > 12f)
			{
				Vector2 position = base.npc.Center + Vector2.Normalize(base.npc.velocity) * 30f;
				Dust dust6 = Main.dust[Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 235, 0f, 0f, 0, default(Color), 1f)];
				dust6.position = position;
				dust6.velocity = Utils.RotatedBy(base.npc.velocity, 1.5708, default(Vector2)) * 0.33f + base.npc.velocity / 4f;
				dust6.position += Utils.RotatedBy(base.npc.velocity, 1.5708, default(Vector2));
				dust6.fadeIn = 0.5f;
				dust6.noGravity = true;
				Dust dust7 = Main.dust[Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 235, 0f, 0f, 0, default(Color), 1f)];
				dust7.position = position;
				dust7.velocity = Utils.RotatedBy(base.npc.velocity, -1.5708, default(Vector2)) * 0.33f + base.npc.velocity / 4f;
				dust7.position += Utils.RotatedBy(base.npc.velocity, -1.5708, default(Vector2));
				dust7.fadeIn = 0.5f;
				dust7.noGravity = true;
				return;
			}
			if (base.npc.ai[3] == 2f)
			{
				base.npc.ai[3] = 0f;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return base.npc.ai[3] == 2f || (base.npc.ai[0] == 5f && base.npc.velocity.Length() > 12f);
		}

		public override bool CheckDead()
		{
			if (base.npc.ai[1] >= 17f)
			{
				return true;
			}
			base.npc.ai[0] = 5f;
			base.npc.life = 1;
			return false;
		}

		public void Dash(int speed, bool directional)
		{
			Player player = Main.player[base.npc.target];
			Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
			base.npc.ai[3] = 2f;
			if (directional)
			{
				base.npc.velocity = base.npc.DirectionTo(player.Center) * (float)speed;
				return;
			}
			base.npc.velocity.X = (float)((player.Center.X > base.npc.Center.X) ? speed : (-(float)speed));
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (player.active && !player.dead)
			{
				return;
			}
			base.npc.velocity *= 0.96f;
			if (base.npc.ai[0] == 1f && base.npc.ai[2] > 190f && RedeWorld.oblitDeath == 0)
			{
				RedeWorld.oblitDeath = 1;
				base.npc.ai[2] = 0f;
				base.npc.ai[3] = 0f;
				base.npc.ai[0] = 6f;
				return;
			}
			if (base.npc.ai[0] == 6f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 100f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Alright, target eliminated.", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 190f)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Returning to base...", true, false);
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] > 190f)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y - 1f;
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
						return;
					}
				}
			}
			else
			{
				if (base.npc.ai[0] != 7f)
				{
					if (RedeWorld.oblitDeath == 2)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "TARGET OBLITERATED... RETURNING TO GIRUS...", true, false);
					}
					else
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Target eliminated...", true, false);
					}
					base.npc.ai[2] = 0f;
					base.npc.ai[0] = 7f;
				}
				if (base.npc.ai[0] == 7f)
				{
					float[] ai = base.npc.ai;
					int num = 2;
					float num2 = ai[num] + 1f;
					ai[num] = num2;
					if (num2 > 120f)
					{
						NPC npc2 = base.npc;
						npc2.velocity.Y = npc2.velocity.Y - 1f;
					}
				}
				if (base.npc.timeLeft > 10)
				{
					base.npc.timeLeft = 10;
				}
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Glow");
			Texture2D pewAni = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Pew");
			Texture2D pewGlow = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Pew_Glow");
			Texture2D chargeAni = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Charge");
			Texture2D chargeGlow = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Charge_Glow");
			Texture2D damagedAni = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Damaged");
			Texture2D damagedGlow = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Damaged_Glow");
			Texture2D overheatAni = base.mod.GetTexture("NPCs/Bosses/OmegaOblit/OO_Overheat");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			if (base.npc.ai[3] == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			if (base.npc.ai[3] == 1f)
			{
				int num214 = pewAni.Height / 4;
				int y6 = num214 * this.pewFrame;
				Main.spriteBatch.Draw(pewAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, pewAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)pewAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(pewGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, pewAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)pewAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[3] == 2f)
			{
				int num215 = chargeAni.Height / 3;
				int y7 = num215 * this.chargeFrame;
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(chargeAni, this.oldPos[i] - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, chargeAni.Width, num215)), Color.Red * (0.5f * alpha), this.oldrot[i], new Vector2((float)chargeAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
				Main.spriteBatch.Draw(chargeAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, chargeAni.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)chargeAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(chargeGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, chargeAni.Width, num215)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)chargeAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.ai[3] >= 3f)
			{
				int num216 = damagedAni.Height / 4;
				int y8 = num216 * this.damagedFrame;
				Main.spriteBatch.Draw(damagedAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, damagedAni.Width, num216)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)damagedAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(damagedGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, damagedAni.Width, num216)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)damagedAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(overheatAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, damagedAni.Width, num216)), drawColor * ((255f - this.overheatAlpha) / 255f), base.npc.rotation, new Vector2((float)damagedAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public Vector2[] oldPos = new Vector2[5];

		public float[] oldrot = new float[5];

		public Vector2 MoveVector2;

		public int frameCounters;

		public int pewFrame;

		public int chargeFrame;

		public int damagedFrame;

		public int repeat;

		public bool barrierSpawn;

		public float overheatAlpha = 255f;

		private bool title;

		public List<int> AttackList = new List<int>
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7
		};

		public List<int> CopyList;
	}
}
