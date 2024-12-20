using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch1");
			this.bossBag = base.mod.ItemType("VlitchCleaverBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 80; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.9f;
				}
				for (int j = 0; j < 45; j++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num2].velocity *= 1.8f;
				}
				for (int k = 0; k < 25; k++)
				{
					int num3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusDagger"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusLance"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(12, 24), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchBattery"), Main.rand.Next(1, 3), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedVlitch1)
			{
				RedeWorld.redemptionPoints++;
				CombatText.NewText(this.player.getRect(), Color.Gold, "+1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player = Main.player[i];
					if (player.active)
					{
						for (int j = 0; j < player.inventory.Length; j++)
						{
							if (player.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> The first Vlitch Overlord is gone, only... 2 more to go? Maybe?", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedVlitch1 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk1 && !NPC.AnyNPCs(base.mod.NPCType("VlitchWormHead")) && !NPC.AnyNPCs(base.mod.NPCType("OmegaOblitDamaged")))
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), base.mod.ProjectileType("GirusTalking1"), 0, 0f, 255, 0f, 0f);
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
			if (Config.classicRedeVC)
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
			if (Main.dayTime)
			{
				base.npc.timeLeft = 0;
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.ai[1] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
			{
				this.customAI[3] += 1f;
				if (this.customAI[3] == 600f)
				{
					Main.NewText("Wait... Why aren't my minions targetting you?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (this.customAI[3] == 800f)
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
				if (!Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("CleaverClone1"), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned1 = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.55f) && !this.cloneSummoned2)
			{
				if (!Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("CleaverClone1"), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned2 = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.35f) && !this.cloneSummoned3)
			{
				if (!Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("CleaverClone1"), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned3 = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && !this.cloneSummoned4)
			{
				if (!Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.IndianRed, "Phantom Cleaver!", true, false);
				}
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("CleaverClone1"), 0, 0f, 0f, 0f, 0f, 255);
				this.cloneSummoned4 = true;
				base.npc.netUpdate = true;
			}
			this.customAI[0] += 1f;
			if (base.npc.life > (int)((float)base.npc.lifeMax * 0.55f))
			{
				if (this.customAI[0] == 100f || this.customAI[0] == 120f || this.customAI[0] == 140f)
				{
					float num = 10f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num2 = 40;
					int num3 = base.mod.ProjectileType("OmegaBlast");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num5 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
					Main.projectile[num5].netUpdate = true;
				}
				if (this.customAI[0] == 320f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num6 = 8;
					for (int i = 0; i < num6; i++)
					{
						int num7 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num7].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num6 * 6.28f);
						Main.projectile[num7].netUpdate = true;
					}
				}
				if (this.customAI[0] == 400f || this.customAI[0] == 460f || this.customAI[0] == 520f)
				{
					float num8 = 10f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num9 = 40;
					int num10 = base.mod.ProjectileType("OmegaBlast");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float num11 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num12 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num11) * (double)num8 * -1.0), (float)(Math.Sin((double)num11) * (double)num8 * -1.0), num10, num9, 0f, 0, 0f, 0f);
					int num13 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num11) * (double)num8 * -1.0) + 1f, (float)(Math.Sin((double)num11) * (double)num8 * -1.0) + 1f, num10, num9, 0f, 0, 0f, 0f);
					int num14 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num11) * (double)num8 * -1.0) + -1f, (float)(Math.Sin((double)num11) * (double)num8 * -1.0) + -1f, num10, num9, 0f, 0, 0f, 0f);
					Main.projectile[num12].netUpdate = true;
					Main.projectile[num13].netUpdate = true;
					Main.projectile[num14].netUpdate = true;
				}
				if (this.customAI[0] >= 700f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num15 = 16;
					for (int j = 0; j < num15; j++)
					{
						int num16 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num16].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)j / (float)num15 * 6.28f);
						Main.projectile[num16].netUpdate = true;
					}
					this.customAI[0] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.55f))
			{
				if (this.customAI[0] == 100f || this.customAI[0] == 110f || this.customAI[0] == 120f || this.customAI[0] == 130f || this.customAI[0] == 140f)
				{
					float num17 = 13f;
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num18 = 40;
					int num19 = base.mod.ProjectileType("OmegaBlast");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float num20 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num21 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num20) * (double)num17 * -1.0), (float)(Math.Sin((double)num20) * (double)num17 * -1.0), num19, num18, 0f, 0, 0f, 0f);
					Main.projectile[num21].netUpdate = true;
				}
				if (this.customAI[0] == 320f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num22 = 8;
					for (int k = 0; k < num22; k++)
					{
						int num23 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num23].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)k / (float)num22 * 6.28f);
						Main.projectile[num23].netUpdate = true;
					}
				}
				if (this.customAI[0] == 380f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num24 = 8;
					for (int l = 0; l < num24; l++)
					{
						int num25 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num25].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)l / (float)num24 * 6.28f);
						Main.projectile[num25].netUpdate = true;
					}
				}
				if (this.customAI[0] == 400f || this.customAI[0] == 430f || this.customAI[0] == 460f || this.customAI[0] == 490f || this.customAI[0] == 510f)
				{
					float num26 = 13f;
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num27 = 40;
					int num28 = base.mod.ProjectileType("OmegaBlast");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
					float num29 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					int num30 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0), (float)(Math.Sin((double)num29) * (double)num26 * -1.0), num28, num27, 0f, 0, 0f, 0f);
					int num31 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0) + 1f, (float)(Math.Sin((double)num29) * (double)num26 * -1.0) + 1f, num28, num27, 0f, 0, 0f, 0f);
					int num32 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0) + -1f, (float)(Math.Sin((double)num29) * (double)num26 * -1.0) + -1f, num28, num27, 0f, 0, 0f, 0f);
					Main.projectile[num30].netUpdate = true;
					Main.projectile[num31].netUpdate = true;
					Main.projectile[num32].netUpdate = true;
				}
				if (this.customAI[0] >= 700f)
				{
					Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num33 = 16;
					for (int m = 0; m < num33; m++)
					{
						int num34 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("OmegaBlast"), 40, 3f, 255, 0f, 0f);
						Main.projectile[num34].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(9f, 0f), (float)m / (float)num33 * 6.28f);
						Main.projectile[num34].netUpdate = true;
					}
					this.customAI[0] = 0f;
				}
			}
			if (base.npc.life > (int)((float)base.npc.lifeMax * 0.55f))
			{
				base.npc.ai[1] += 1f;
			}
			if (base.npc.ai[1] % 200f == 80f && NPC.CountNPCS(base.mod.NPCType("CorruptedProbe")) <= 1)
			{
				int num35 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 120, base.mod.NPCType("CorruptedProbe"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num35].netUpdate = true;
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.55f))
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 250f)
			{
				float num36 = 10f;
				Vector2 vector5;
				vector5..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num37 = 100;
				int num38 = base.mod.ProjectileType("VlitchCleaverPro");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
				float num39 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
				int num40 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num39) * (double)num36 * -1.0), (float)(Math.Sin((double)num39) * (double)num36 * -1.0), num38, num37, 0f, 0, 0f, 0f);
				Main.projectile[num40].netUpdate = true;
				base.npc.ai[2] = 0f;
			}
			if (base.npc.ai[2] % 200f == 80f && NPC.CountNPCS(base.mod.NPCType("CorruptedBlade")) <= 2)
			{
				int num41 = NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 120, base.mod.NPCType("CorruptedBlade"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num41].netUpdate = true;
			}
			if (base.npc.life <= 2000)
			{
				base.npc.ai[3] += 1f;
			}
			if (base.npc.ai[3] >= 100f)
			{
				float num42 = 10f;
				Vector2 vector6;
				vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num43 = 100;
				int num44 = base.mod.ProjectileType("VlitchCleaverPro");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
				float num45 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
				int num46 = Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num45) * (double)num42 * -1.0), (float)(Math.Sin((double)num45) * (double)num42 * -1.0), num44, num43, 0f, 0, 0f, 0f);
				Main.projectile[num46].netUpdate = true;
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
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
					{
						Main.NewText("No matter, I guess I'll take action...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
						base.npc.netUpdate = true;
					}
					else
					{
						Main.NewText("Guess its time to take action...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
						base.npc.netUpdate = true;
					}
					float num47 = 150f;
					float num48 = 1.26f;
					for (int n = 0; n < 10; n++)
					{
						Vector2 vector7 = base.npc.Center + num47 * Utils.ToRotationVector2((float)n * num48);
						int num49 = NPC.NewNPC((int)vector7.X, (int)vector7.Y, base.mod.NPCType("CleaverDagger"), 0, (float)base.npc.whoAmI, 0f, (float)n, 0f, 255);
						Main.npc[num49].netUpdate = true;
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
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_Glow");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_OLD");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_Glow_OLD");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			if (Config.classicRedeVC)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture2.Height / 5;
				int num2 = num * this.oldFrame;
				Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				Main.spriteBatch.Draw(texture3, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture2.Width, num)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			else
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
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
	}
}
