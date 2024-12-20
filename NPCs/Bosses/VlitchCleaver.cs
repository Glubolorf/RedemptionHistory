using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items;
using Redemption.Items.Armor;
using Redemption.Items.Placeable;
using Redemption.Items.Weapons;
using Redemption.NPCs.Bosses.OmegaOblit;
using Redemption.NPCs.v08;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class VlitchCleaver : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Cleaver");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.width = 98;
			base.npc.height = 280;
			base.npc.friendly = false;
			base.npc.damage = 250;
			base.npc.defense = 60;
			base.npc.lifeMax = 35000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = 600f;
			base.npc.boss = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			this.animationType = 83;
			this.bossBag = ModContent.ItemType<VlitchCleaverBag>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 80; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
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
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
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
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusMask>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusDagger>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GirusLance>(), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(12, 24), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VlitchBattery>(), Main.rand.Next(1, 3), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedVlitch1)
			{
				RedeWorld.redemptionPoints++;
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == ModContent.ItemType<RedemptionTeller>())
							{
								Main.NewText("<Chalice of Alignment> The first Vlitch Overlord is gone, only... 2 more to go? Maybe?", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gold, "+1", true, false);
					}
				}
			}
			RedeWorld.downedVlitch1 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk1 && !NPC.AnyNPCs(ModContent.NPCType<VlitchWormHead>()) && !NPC.AnyNPCs(ModContent.NPCType<OO>()) && !RedeWorld.girusCloaked && !RedeConfigClient.Instance.NoBossText)
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), ModContent.ProjectileType<GirusTalking1>(), 0, 0f, 255, 0f, 0f);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.5f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.customAI[1]);
				writer.Write(this.customAI[2]);
				writer.Write(this.customAI[3]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.customAI[1] = reader.ReadFloat();
				this.customAI[2] = reader.ReadFloat();
				this.customAI[3] = reader.ReadFloat();
			}
		}

		public override void AI()
		{
			if (!this.title)
			{
				Redemption.ShowTitle(base.npc, 10);
				this.title = true;
			}
			if (RedeConfigClient.Instance.classicRedeVC)
			{
				this.oldCounter++;
				if (this.oldCounter > 10)
				{
					this.oldFrame++;
					this.oldCounter = 0;
				}
				if (this.oldFrame >= 5)
				{
					this.oldFrame = 0;
				}
			}
			if (NPC.AnyNPCs(ModContent.NPCType<CleaverDagger>()))
			{
				base.npc.dontTakeDamage = true;
			}
			else
			{
				base.npc.dontTakeDamage = false;
			}
			if (Main.dayTime)
			{
				base.npc.timeLeft = 0;
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.ai[1] += 1f;
			Player P = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VlitchFlame>(), 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
			{
				this.customAI[3] += 1f;
				if (this.customAI[3] == 600f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("Wait... Why aren't my minions targetting you?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (this.customAI[3] == 800f && !RedeConfigClient.Instance.NoBossText)
				{
					Main.NewText("Oh, because you got that damn exoskeleton on...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (this.customAI[3] == 1300f)
				{
					this.takeAction = true;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.85f) && !this.cloneSummoned1)
			{
				if (!RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<CleaverClone1>(), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned1 = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.55f) && !this.cloneSummoned2)
			{
				if (!RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<CleaverClone1>(), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned2 = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.35f) && !this.cloneSummoned3)
			{
				if (!RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<CleaverClone1>(), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned3 = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && !this.cloneSummoned4)
			{
				if (!RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<CleaverClone1>(), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned4 = true;
				base.npc.netUpdate = true;
			}
			this.customAI[0] += 1f;
			if (base.npc.life > (int)((float)base.npc.lifeMax * 0.55f))
			{
				if (this.customAI[0] == 100f || this.customAI[0] == 120f || this.customAI[0] == 140f)
				{
					float Speed = 10f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 40;
					int type = ModContent.ProjectileType<OmegaBlast>();
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float rotation = (float)Math.Atan2((double)(vector8.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector8.X - (P.position.X + (float)P.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
				}
				if (this.customAI[0] == 320f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut = 8;
					for (int i = 0; i < pieCut; i++)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
						Main.projectile[projID].netUpdate = true;
					}
				}
				if (this.customAI[0] == 400f || this.customAI[0] == 460f || this.customAI[0] == 520f)
				{
					float Speed2 = 10f;
					Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage2 = 40;
					int type2 = ModContent.ProjectileType<OmegaBlast>();
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector9.X - (P.position.X + (float)P.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					int num56 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + 1f, (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + 1f, type2, damage2, 0f, 0, 0f, 0f);
					int num57 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + -1f, (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + -1f, type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
					Main.projectile[num56].netUpdate = true;
					Main.projectile[num57].netUpdate = true;
				}
				if (this.customAI[0] >= 700f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut2 = 16;
					for (int j = 0; j < pieCut2; j++)
					{
						int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)j / (float)pieCut2 * 6.28f);
						Main.projectile[projID2].netUpdate = true;
					}
					this.customAI[0] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.55f))
			{
				if (this.customAI[0] == 100f || this.customAI[0] == 110f || this.customAI[0] == 120f || this.customAI[0] == 130f || this.customAI[0] == 140f)
				{
					float Speed3 = 13f;
					Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage3 = 40;
					int type3 = ModContent.ProjectileType<OmegaBlast>();
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector10.X - (P.position.X + (float)P.width * 0.5f)));
					int num58 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num58].netUpdate = true;
				}
				if (this.customAI[0] == 320f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut3 = 8;
					for (int k = 0; k < pieCut3; k++)
					{
						int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)k / (float)pieCut3 * 6.28f);
						Main.projectile[projID3].netUpdate = true;
					}
				}
				if (this.customAI[0] == 380f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut4 = 8;
					for (int l = 0; l < pieCut4; l++)
					{
						int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)l / (float)pieCut4 * 6.28f);
						Main.projectile[projID4].netUpdate = true;
					}
				}
				if (this.customAI[0] == 400f || this.customAI[0] == 430f || this.customAI[0] == 460f || this.customAI[0] == 490f || this.customAI[0] == 510f)
				{
					float Speed4 = 13f;
					Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage4 = 40;
					int type4 = ModContent.ProjectileType<OmegaBlast>();
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector11.X - (P.position.X + (float)P.width * 0.5f)));
					int num59 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
					int num60 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + 1f, (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + 1f, type4, damage4, 0f, 0, 0f, 0f);
					int num61 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0) + -1f, (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0) + -1f, type4, damage4, 0f, 0, 0f, 0f);
					Main.projectile[num59].netUpdate = true;
					Main.projectile[num60].netUpdate = true;
					Main.projectile[num61].netUpdate = true;
				}
				if (this.customAI[0] >= 700f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int pieCut5 = 16;
					for (int m = 0; m < pieCut5; m++)
					{
						int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, ModContent.ProjectileType<OmegaBlast>(), 40, 3f, 255, 0f, 0f);
						Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)m / (float)pieCut5 * 6.28f);
						Main.projectile[projID5].netUpdate = true;
					}
					this.customAI[0] = 0f;
				}
			}
			if (base.npc.life > (int)((float)base.npc.lifeMax * 0.55f))
			{
				base.npc.ai[1] += 1f;
			}
			if (base.npc.ai[1] % 200f == 80f && NPC.CountNPCS(ModContent.NPCType<CorruptedProbe>()) <= 1)
			{
				int Minion = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 120, ModContent.NPCType<CorruptedProbe>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion].netUpdate = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.55f))
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 250f)
			{
				float Speed5 = 10f;
				Vector2 vector12 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage5 = 100;
				int type5 = ModContent.ProjectileType<VlitchCleaverPro>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
				float rotation5 = (float)Math.Atan2((double)(vector12.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector12.X - (P.position.X + (float)P.width * 0.5f)));
				int num62 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
				Main.projectile[num62].netUpdate = true;
				base.npc.ai[2] = 0f;
			}
			if (base.npc.ai[2] % 200f == 80f && NPC.CountNPCS(ModContent.NPCType<CorruptedBlade>()) <= 2)
			{
				int Minion2 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 120, ModContent.NPCType<CorruptedBlade>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion2].netUpdate = true;
			}
			if (base.npc.life <= 2000)
			{
				base.npc.ai[3] += 1f;
			}
			if (base.npc.ai[3] >= 100f)
			{
				float Speed6 = 10f;
				Vector2 vector13 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage6 = 100;
				int type6 = ModContent.ProjectileType<VlitchCleaverPro>();
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
				float rotation6 = (float)Math.Atan2((double)(vector13.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector13.X - (P.position.X + (float)P.width * 0.5f)));
				int num63 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
				Main.projectile[num63].netUpdate = true;
				base.npc.ai[3] = 0f;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.55f))
			{
				this.takeAction = true;
			}
			if (this.takeAction)
			{
				this.customAI[1] += 1f;
				if (this.customAI[1] == 1f)
				{
					if (!RedeConfigClient.Instance.NoBossText)
					{
						if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
						{
							Main.NewText("No matter, I guess I'll take action...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
							base.npc.netUpdate = true;
						}
						else
						{
							Main.NewText("Guess its time to take action...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
							base.npc.netUpdate = true;
						}
					}
					float distance = 150f;
					float n = 1.26f;
					for (int count = 0; count < 10; count++)
					{
						Vector2 spawn = base.npc.Center + distance * Utils.ToRotationVector2((float)count * n);
						int Minion3 = NPC.NewNPC((int)spawn.X, (int)spawn.Y, ModContent.NPCType<CleaverDagger>(), 0, (float)base.npc.whoAmI, 0f, (float)count, 0f, 255);
						Main.npc[Minion3].netUpdate = true;
					}
				}
				this.customAI[2] += 1f;
				if (this.customAI[2] <= 120f && !this.player.dead)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y * 0.2f;
				}
				if (this.customAI[2] >= 120f)
				{
					if (!this.player.dead)
					{
						NPC npc3 = base.npc;
						npc3.velocity.Y = npc3.velocity.Y * -0.2f;
					}
					if (this.customAI[2] == 240f)
					{
						this.customAI[2] = 0f;
					}
				}
				base.npc.aiStyle = 5;
				this.aiType = 205;
				base.npc.noGravity = true;
				base.npc.noTileCollide = true;
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
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_Glow");
			Texture2D oldAni = base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_OLD");
			Texture2D oldGlowmaskAni = base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_Glow_OLD");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (RedeConfigClient.Instance.classicRedeVC)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y - 22f);
				int num214 = oldAni.Height / 5;
				int y6 = num214 * this.oldFrame;
				Main.spriteBatch.Draw(oldAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, oldAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)oldAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(oldGlowmaskAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, oldAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)oldAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			return false;
		}

		private Player player;

		public float[] customAI = new float[4];

		private bool takeAction;

		private bool cloneSummoned1;

		private bool cloneSummoned2;

		private bool cloneSummoned3;

		private bool cloneSummoned4;

		private int oldFrame;

		private int oldCounter;

		private bool title;
	}
}
