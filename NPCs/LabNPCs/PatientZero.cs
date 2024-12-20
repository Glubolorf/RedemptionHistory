using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	[AutoloadBossHead]
	public class PatientZero : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Patient Zero");
		}

		public override void SetDefaults()
		{
			base.npc.width = 114;
			base.npc.height = 90;
			base.npc.friendly = false;
			base.npc.damage = 110;
			base.npc.defense = 80;
			base.npc.lifeMax = 340000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(2, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.noTileCollide = false;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic2");
			this.bossBag = base.mod.ItemType("PZBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 1f);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			potionType = 58;
			if (!RedeWorld.downedPatientZero)
			{
				RedeWorld.redemptionPoints += 3;
				CombatText.NewText(player.getRect(), Color.Gold, "+3", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> We did it! We stopped the Infection! High-five! ... Oh, right.", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedPatientZero = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess7)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel7"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PZTrophy"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk7"), 1, false, 0, false, false);
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PZMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("PZGauntlet"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("SwarmerGun"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XeniumSaber"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("MedicKit1"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("BluePrints"), 1, false, 0, false, false);
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
				writer.Write(this.customAI[4]);
				writer.Write(this.beginFight);
				writer.Write(this.phase2Done);
				writer.Write(this.phase2);
				writer.Write(this.phase2Done);
				writer.Write(this.phase3);
				writer.Write(this.phase3Done);
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
				this.customAI[4] = reader.ReadFloat();
				this.beginFight = reader.ReadBool();
				this.phase2Done = reader.ReadBool();
				this.phase2 = reader.ReadBool();
				this.phase2Done = reader.ReadBool();
				this.phase3 = reader.ReadBool();
				this.phase3Done = reader.ReadBool();
			}
		}

		public override void AI()
		{
			this.bodyCounter++;
			if (this.bodyCounter > 20)
			{
				this.bodyFrame++;
				this.bodyCounter = 0;
			}
			if (this.bodyFrame >= 4)
			{
				this.bodyFrame = 0;
			}
			this.sludgeCounter++;
			if (this.sludgeCounter > 40)
			{
				this.sludgeFrame++;
				this.sludgeCounter = 0;
			}
			if (this.sludgeFrame >= 2)
			{
				this.sludgeFrame = 0;
			}
			if (this.openEye)
			{
				this.openCounter++;
				if (this.openCounter > 20)
				{
					this.openFrame++;
					this.openCounter = 0;
				}
				if (this.openFrame >= 4)
				{
					this.openFrame = 3;
				}
			}
			if (this.lookAround)
			{
				this.lookCounter++;
				if (this.lookCounter > 10)
				{
					this.lookFrame++;
					this.lookCounter = 0;
				}
				if (this.lookFrame >= 8)
				{
					this.lookFrame = 0;
				}
			}
			if (this.blink)
			{
				this.blinkCounter++;
				if (this.blinkCounter > 5)
				{
					this.blinkFrame++;
					this.blinkCounter = 0;
				}
				if (this.blinkFrame >= 7)
				{
					this.blinkFrame = 6;
				}
			}
			if (this.laserBeam)
			{
				this.laserCounter++;
				if (this.laserCounter > 10)
				{
					this.laserFrame++;
					this.laserCounter = 0;
				}
				if (this.laserFrame >= 2)
				{
					this.laserFrame = 0;
				}
			}
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (NPC.AnyNPCs(base.mod.NPCType("HiveGrowth")))
			{
				base.npc.dontTakeDamage = true;
			}
			else if (this.beginFight || this.phase2)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			base.npc.ai[0] += 1f;
			if (base.npc.ai[0] == 1f)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (Main.netMode != 1)
				{
					Vector2 vector;
					vector..ctor(50f, -183f);
					base.npc.Center = base.npc.position + vector;
					base.npc.netUpdate = true;
				}
				for (int i = 0; i < 50; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			if (base.npc.alpha > 0)
			{
				base.npc.alpha -= 4;
			}
			if (base.npc.ai[0] == 80f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num2].netUpdate = true;
			}
			if (base.npc.ai[0] == 90f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int num3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num3].netUpdate = true;
			}
			if (base.npc.ai[0] == 109f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int num4 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num4].netUpdate = true;
			}
			if (base.npc.ai[0] == 121f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int num5 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num5].netUpdate = true;
			}
			if (base.npc.ai[0] > 500f)
			{
				this.beginFight = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] > 500f && base.npc.ai[0] <= 580f)
			{
				this.openEye = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] > 580f)
			{
				this.openEye = false;
				this.lookAround = true;
				base.npc.netUpdate = true;
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 260f)
				{
					this.lookAround = false;
					this.customAI[4] = 1f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 520f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num6 = 8f;
					Vector2 vector2;
					vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num7 = 50;
					int num8 = base.mod.ProjectileType("PatientBlast");
					float num9 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num10 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num9) * (double)num6 * -1.0), (float)(Math.Sin((double)num9) * (double)num6 * -1.0), num8, num7, 0f, 0, 0f, 0f);
					Main.projectile[num10].netUpdate = true;
				}
				if (base.npc.ai[1] == 580f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num11 = 8f;
					Vector2 vector3;
					vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num12 = 50;
					int num13 = base.mod.ProjectileType("PatientBlast");
					float num14 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num15 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num14) * (double)num11 * -1.0), (float)(Math.Sin((double)num14) * (double)num11 * -1.0), num13, num12, 0f, 0, 0f, 0f);
					Main.projectile[num15].netUpdate = true;
				}
				if (base.npc.ai[1] == 620f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num16 = 8f;
					Vector2 vector4;
					vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num17 = 50;
					int num18 = base.mod.ProjectileType("PatientBlast");
					float num19 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					int num20 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num19) * (double)num16 * -1.0), (float)(Math.Sin((double)num19) * (double)num16 * -1.0), num18, num17, 0f, 0, 0f, 0f);
					Main.projectile[num20].netUpdate = true;
				}
				if (base.npc.ai[1] == 660f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num21 = 8f;
					Vector2 vector5;
					vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num22 = 50;
					int num23 = base.mod.ProjectileType("PatientBlast");
					float num24 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
					int num25 = Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num24) * (double)num21 * -1.0), (float)(Math.Sin((double)num24) * (double)num21 * -1.0), num23, num22, 0f, 0, 0f, 0f);
					Main.projectile[num25].netUpdate = true;
				}
				if (base.npc.ai[1] == 820f)
				{
					this.lookAround = false;
					this.customAI[4] = 2f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 1180f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num26 = 8f;
					Vector2 vector6;
					vector6..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num27 = 50;
					int num28 = base.mod.ProjectileType("PatientBlast");
					float num29 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
					int num30 = Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0), (float)(Math.Sin((double)num29) * (double)num26 * -1.0), num28, num27, 0f, 0, 0f, 0f);
					Main.projectile[num30].netUpdate = true;
				}
				if (base.npc.ai[1] == 1240f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num31 = 8f;
					Vector2 vector7;
					vector7..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num32 = 50;
					int num33 = base.mod.ProjectileType("PatientBlast");
					float num34 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
					int num35 = Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num34) * (double)num31 * -1.0), (float)(Math.Sin((double)num34) * (double)num31 * -1.0), num33, num32, 0f, 0, 0f, 0f);
					Main.projectile[num35].netUpdate = true;
				}
				if (base.npc.ai[1] == 1300f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num36 = 8f;
					Vector2 vector8;
					vector8..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num37 = 50;
					int num38 = base.mod.ProjectileType("PatientBlast");
					float num39 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num40 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num39) * (double)num36 * -1.0), (float)(Math.Sin((double)num39) * (double)num36 * -1.0), num38, num37, 0f, 0, 0f, 0f);
					Main.projectile[num40].netUpdate = true;
				}
				if (base.npc.ai[1] == 1360f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num41 = 8f;
					Vector2 vector9;
					vector9..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num42 = 50;
					int num43 = base.mod.ProjectileType("PatientBlast");
					float num44 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num45 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num44) * (double)num41 * -1.0), (float)(Math.Sin((double)num44) * (double)num41 * -1.0), num43, num42, 0f, 0, 0f, 0f);
					Main.projectile[num45].netUpdate = true;
				}
				if (base.npc.ai[1] >= 1480f)
				{
					base.npc.ai[1] = 180f;
					base.npc.netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num46 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num46].netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.6f) && !this.phase2Done && !this.laserBeam)
			{
				this.phase2 = true;
				base.npc.netUpdate = true;
			}
			if (this.phase2)
			{
				base.npc.ai[1] = 0f;
				this.beginFight = false;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 60f)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num47 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num47].netUpdate = true;
				}
				if (base.npc.ai[2] == 80f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num48 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num48].netUpdate = true;
				}
				if (base.npc.ai[2] == 95f)
				{
					this.blink = false;
					this.lookAround = true;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num49 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num49].netUpdate = true;
				}
				if (base.npc.ai[2] == 110f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num50 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num50].netUpdate = true;
				}
				if (base.npc.ai[2] == 600f)
				{
					this.lookAround = false;
					this.customAI[4] = 3f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 1350f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num51 = 8f;
					Vector2 vector10;
					vector10..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num52 = 50;
					int num53 = base.mod.ProjectileType("PatientBlast");
					float num54 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num54) * (double)num51 * -1.0), (float)(Math.Sin((double)num54) * (double)num51 * -1.0), num53, num52, 0f, 0, 0f, 0f);
					int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num54) * (double)num51 * -1.0) + -1f, (float)(Math.Sin((double)num54) * (double)num51 * -1.0) + -1f, num53, num52, 0f, 0, 0f, 0f);
					int num57 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num54) * (double)num51 * -1.0) + 1f, (float)(Math.Sin((double)num54) * (double)num51 * -1.0) + 1f, num53, num52, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
					Main.projectile[num56].netUpdate = true;
					Main.projectile[num57].netUpdate = true;
				}
				if (base.npc.ai[2] == 1450f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num58 = 8f;
					Vector2 vector11;
					vector11..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num59 = 50;
					int num60 = base.mod.ProjectileType("PatientBlast");
					float num61 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					int num62 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num61) * (double)num58 * -1.0), (float)(Math.Sin((double)num61) * (double)num58 * -1.0), num60, num59, 0f, 0, 0f, 0f);
					int num63 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num61) * (double)num58 * -1.0) + -1f, (float)(Math.Sin((double)num61) * (double)num58 * -1.0) + -1f, num60, num59, 0f, 0, 0f, 0f);
					int num64 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num61) * (double)num58 * -1.0) + 1f, (float)(Math.Sin((double)num61) * (double)num58 * -1.0) + 1f, num60, num59, 0f, 0, 0f, 0f);
					Main.projectile[num62].netUpdate = true;
					Main.projectile[num63].netUpdate = true;
					Main.projectile[num64].netUpdate = true;
				}
				if (base.npc.ai[2] == 1550f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num65 = 8f;
					Vector2 vector12;
					vector12..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num66 = 50;
					int num67 = base.mod.ProjectileType("PatientBlast");
					float num68 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					int num69 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num68) * (double)num65 * -1.0), (float)(Math.Sin((double)num68) * (double)num65 * -1.0), num67, num66, 0f, 0, 0f, 0f);
					int num70 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num68) * (double)num65 * -1.0) + -1f, (float)(Math.Sin((double)num68) * (double)num65 * -1.0) + -1f, num67, num66, 0f, 0, 0f, 0f);
					int num71 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num68) * (double)num65 * -1.0) + 1f, (float)(Math.Sin((double)num68) * (double)num65 * -1.0) + 1f, num67, num66, 0f, 0, 0f, 0f);
					Main.projectile[num69].netUpdate = true;
					Main.projectile[num70].netUpdate = true;
					Main.projectile[num71].netUpdate = true;
				}
				if (base.npc.ai[2] == 1800f)
				{
					this.lookAround = false;
					this.customAI[4] = 1f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 1920f)
				{
					this.lookAround = false;
					this.customAI[4] = 2f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] == 2300f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num72 = 8f;
					Vector2 vector13;
					vector13..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num73 = 50;
					int num74 = base.mod.ProjectileType("PatientBlast");
					float num75 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					int num76 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num75) * (double)num72 * -1.0), (float)(Math.Sin((double)num75) * (double)num72 * -1.0), num74, num73, 0f, 0, 0f, 0f);
					int num77 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num75) * (double)num72 * -1.0) + -1f, (float)(Math.Sin((double)num75) * (double)num72 * -1.0) + -1f, num74, num73, 0f, 0, 0f, 0f);
					int num78 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num75) * (double)num72 * -1.0) + 1f, (float)(Math.Sin((double)num75) * (double)num72 * -1.0) + 1f, num74, num73, 0f, 0, 0f, 0f);
					Main.projectile[num76].netUpdate = true;
					Main.projectile[num77].netUpdate = true;
					Main.projectile[num78].netUpdate = true;
				}
				if (base.npc.ai[2] == 2360f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num79 = 8f;
					Vector2 vector14;
					vector14..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num80 = 50;
					int num81 = base.mod.ProjectileType("PatientBlast");
					float num82 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					int num83 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num82) * (double)num79 * -1.0), (float)(Math.Sin((double)num82) * (double)num79 * -1.0), num81, num80, 0f, 0, 0f, 0f);
					int num84 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num82) * (double)num79 * -1.0) + -1f, (float)(Math.Sin((double)num82) * (double)num79 * -1.0) + -1f, num81, num80, 0f, 0, 0f, 0f);
					int num85 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num82) * (double)num79 * -1.0) + 1f, (float)(Math.Sin((double)num82) * (double)num79 * -1.0) + 1f, num81, num80, 0f, 0, 0f, 0f);
					Main.projectile[num83].netUpdate = true;
					Main.projectile[num84].netUpdate = true;
					Main.projectile[num85].netUpdate = true;
				}
				if (base.npc.ai[2] == 2420f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num86 = 8f;
					Vector2 vector15;
					vector15..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num87 = 50;
					int num88 = base.mod.ProjectileType("PatientBlast");
					float num89 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
					int num90 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num89) * (double)num86 * -1.0), (float)(Math.Sin((double)num89) * (double)num86 * -1.0), num88, num87, 0f, 0, 0f, 0f);
					int num91 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num89) * (double)num86 * -1.0) + -1f, (float)(Math.Sin((double)num89) * (double)num86 * -1.0) + -1f, num88, num87, 0f, 0, 0f, 0f);
					int num92 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num89) * (double)num86 * -1.0) + 1f, (float)(Math.Sin((double)num89) * (double)num86 * -1.0) + 1f, num88, num87, 0f, 0, 0f, 0f);
					Main.projectile[num90].netUpdate = true;
					Main.projectile[num91].netUpdate = true;
					Main.projectile[num92].netUpdate = true;
				}
				if (base.npc.ai[2] >= 2550f)
				{
					base.npc.ai[2] = 500f;
					base.npc.netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num93 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num93].netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.3f) && !this.phase3Done && !this.laserBeam)
			{
				this.phase3 = true;
				this.phase2Done = true;
				base.npc.netUpdate = true;
			}
			if (this.phase3)
			{
				base.npc.ai[1] = 0f;
				base.npc.ai[2] = 0f;
				this.phase2Done = true;
				this.phase2 = false;
				base.npc.ai[3] += 1f;
				if (base.npc.ai[3] == 35f)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num94 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num94].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num95 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num95].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num96 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num96].netUpdate = true;
				}
				if (base.npc.ai[3] == 95f)
				{
					this.blink = false;
					this.lookAround = true;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num97 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num97].netUpdate = true;
				}
				if (base.npc.ai[3] == 110f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num98 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num98].netUpdate = true;
				}
				if (base.npc.ai[3] == 700f)
				{
					this.lookAround = false;
					this.customAI[4] = 4f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 1290f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num99 = 8f;
					Vector2 vector16;
					vector16..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num100 = 50;
					int num101 = base.mod.ProjectileType("PatientBlast");
					float num102 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					int num103 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)num102) * (double)num99 * -1.0), (float)(Math.Sin((double)num102) * (double)num99 * -1.0), num101, num100, 0f, 0, 0f, 0f);
					Main.projectile[num103].netUpdate = true;
				}
				if (base.npc.ai[3] == 1340f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num104 = 8f;
					Vector2 vector17;
					vector17..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num105 = 50;
					int num106 = base.mod.ProjectileType("PatientBlast");
					float num107 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
					int num108 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)num107) * (double)num104 * -1.0), (float)(Math.Sin((double)num107) * (double)num104 * -1.0), num106, num105, 0f, 0, 0f, 0f);
					Main.projectile[num108].netUpdate = true;
				}
				if (base.npc.ai[3] == 1380f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num109 = 8f;
					Vector2 vector18;
					vector18..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num110 = 50;
					int num111 = base.mod.ProjectileType("PatientBlast");
					float num112 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
					int num113 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)num112) * (double)num109 * -1.0), (float)(Math.Sin((double)num112) * (double)num109 * -1.0), num111, num110, 0f, 0, 0f, 0f);
					Main.projectile[num113].netUpdate = true;
				}
				if (base.npc.ai[3] == 1410f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num114 = 8f;
					Vector2 vector19;
					vector19..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num115 = 50;
					int num116 = base.mod.ProjectileType("PatientBlast");
					float num117 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
					int num118 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)num117) * (double)num114 * -1.0), (float)(Math.Sin((double)num117) * (double)num114 * -1.0), num116, num115, 0f, 0, 0f, 0f);
					Main.projectile[num118].netUpdate = true;
				}
				if (base.npc.ai[3] == 1430f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num119 = 8f;
					Vector2 vector20;
					vector20..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num120 = 50;
					int num121 = base.mod.ProjectileType("PatientBlast");
					float num122 = (float)Math.Atan2((double)(vector20.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector20.X - (player.position.X + (float)player.width * 0.5f)));
					int num123 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)num122) * (double)num119 * -1.0), (float)(Math.Sin((double)num122) * (double)num119 * -1.0), num121, num120, 0f, 0, 0f, 0f);
					Main.projectile[num123].netUpdate = true;
				}
				if (base.npc.ai[3] == 1600f)
				{
					this.lookAround = false;
					this.customAI[4] = 2f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 1790f)
				{
					this.lookAround = false;
					this.customAI[4] = 1f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 1980f)
				{
					this.lookAround = false;
					this.customAI[4] = 2f;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 2300f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num124 = 8f;
					Vector2 vector21;
					vector21..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num125 = 50;
					int num126 = base.mod.ProjectileType("PatientBlast");
					float num127 = (float)Math.Atan2((double)(vector21.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector21.X - (player.position.X + (float)player.width * 0.5f)));
					int num128 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)num127) * (double)num124 * -1.0), (float)(Math.Sin((double)num127) * (double)num124 * -1.0), num126, num125, 0f, 0, 0f, 0f);
					Main.projectile[num128].netUpdate = true;
				}
				if (base.npc.ai[3] == 2400f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num129 = 8f;
					Vector2 vector22;
					vector22..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num130 = 50;
					int num131 = base.mod.ProjectileType("PatientBlast");
					float num132 = (float)Math.Atan2((double)(vector22.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector22.X - (player.position.X + (float)player.width * 0.5f)));
					int num133 = Projectile.NewProjectile(vector22.X, vector22.Y, (float)(Math.Cos((double)num132) * (double)num129 * -1.0), (float)(Math.Sin((double)num132) * (double)num129 * -1.0), num131, num130, 0f, 0, 0f, 0f);
					Main.projectile[num133].netUpdate = true;
				}
				if (base.npc.ai[3] == 2500f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num134 = 8f;
					Vector2 vector23;
					vector23..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num135 = 50;
					int num136 = base.mod.ProjectileType("PatientBlast");
					float num137 = (float)Math.Atan2((double)(vector23.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector23.X - (player.position.X + (float)player.width * 0.5f)));
					int num138 = Projectile.NewProjectile(vector23.X, vector23.Y, (float)(Math.Cos((double)num137) * (double)num134 * -1.0), (float)(Math.Sin((double)num137) * (double)num134 * -1.0), num136, num135, 0f, 0, 0f, 0f);
					Main.projectile[num138].netUpdate = true;
				}
				if (base.npc.ai[3] >= 2650f)
				{
					base.npc.ai[3] = 500f;
					base.npc.netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int num139 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num139].netUpdate = true;
				}
			}
			if (this.customAI[4] == 1f)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.customAI[0] += 1f;
				if (this.customAI[0] >= 0f && this.customAI[0] < 83f)
				{
					int num140 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(20f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int num141 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-20f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int num142 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 20f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int num143 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -20f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num140].netUpdate = true;
					Main.projectile[num141].netUpdate = true;
					Main.projectile[num142].netUpdate = true;
					Main.projectile[num143].netUpdate = true;
				}
				if (this.customAI[0] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[0] >= 87f && this.customAI[0] < 170f)
				{
					int num144 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(20f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int num145 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-20f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int num146 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 20f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int num147 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -20f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num144].netUpdate = true;
					Main.projectile[num145].netUpdate = true;
					Main.projectile[num146].netUpdate = true;
					Main.projectile[num147].netUpdate = true;
				}
				if (this.customAI[0] >= 190f)
				{
					this.customAI[4] = 0f;
					this.laserBeam = false;
					this.customAI[0] = 0f;
					this.lookAround = true;
					base.npc.netUpdate = true;
				}
			}
			if (this.customAI[4] == 2f)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.customAI[1] += 1f;
				if (this.customAI[1] >= 0f && this.customAI[1] < 83f)
				{
					int num148 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int num149 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int num150 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int num151 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num148].netUpdate = true;
					Main.projectile[num149].netUpdate = true;
					Main.projectile[num150].netUpdate = true;
					Main.projectile[num151].netUpdate = true;
				}
				if (this.customAI[1] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[1] >= 87f && this.customAI[1] < 170f)
				{
					int num152 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int num153 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int num154 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, -10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int num155 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num152].netUpdate = true;
					Main.projectile[num153].netUpdate = true;
					Main.projectile[num154].netUpdate = true;
					Main.projectile[num155].netUpdate = true;
				}
				if (this.customAI[1] >= 190f)
				{
					this.customAI[4] = 0f;
					this.laserBeam = false;
					this.customAI[1] = 0f;
					this.lookAround = true;
					base.npc.netUpdate = true;
				}
			}
			if (this.customAI[4] == 3f)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.customAI[2] += 1f;
				if (this.customAI[2] >= 0f && this.customAI[2] < 83f)
				{
					int num156 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num156].netUpdate = true;
				}
				if (this.customAI[2] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 87f && this.customAI[2] < 170f)
				{
					int num157 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num157].netUpdate = true;
				}
				if (this.customAI[2] >= 60f && this.customAI[2] < 143f)
				{
					int num158 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num158].netUpdate = true;
				}
				if (this.customAI[2] == 62f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 147f && this.customAI[2] < 230f)
				{
					int num159 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num159].netUpdate = true;
				}
				if (this.customAI[2] >= 120f && this.customAI[2] < 203f)
				{
					int num160 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num160].netUpdate = true;
				}
				if (this.customAI[2] == 122f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 207f && this.customAI[2] < 290f)
				{
					int num161 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num161].netUpdate = true;
				}
				if (this.customAI[2] >= 180f && this.customAI[2] < 263f)
				{
					int num162 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num162].netUpdate = true;
				}
				if (this.customAI[2] == 182f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 267f && this.customAI[2] < 350f)
				{
					int num163 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num163].netUpdate = true;
				}
				if (this.customAI[2] >= 240f && this.customAI[2] < 323f)
				{
					int num164 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num164].netUpdate = true;
				}
				if (this.customAI[2] == 242f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 327f && this.customAI[2] < 410f)
				{
					int num165 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num165].netUpdate = true;
				}
				if (this.customAI[2] >= 300f && this.customAI[2] < 383f)
				{
					int num166 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num166].netUpdate = true;
				}
				if (this.customAI[2] == 302f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 387f && this.customAI[2] < 470f)
				{
					int num167 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num167].netUpdate = true;
				}
				if (this.customAI[2] >= 360f && this.customAI[2] < 443f)
				{
					int num168 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num168].netUpdate = true;
				}
				if (this.customAI[2] == 362f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 447f && this.customAI[2] < 530f)
				{
					int num169 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num169].netUpdate = true;
				}
				if (this.customAI[2] >= 420f && this.customAI[2] < 503f)
				{
					int num170 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[num170].netUpdate = true;
				}
				if (this.customAI[2] == 422f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 507f && this.customAI[2] < 590f)
				{
					int num171 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[num171].netUpdate = true;
				}
				if (this.customAI[2] >= 650f)
				{
					this.customAI[4] = 0f;
					this.laserBeam = false;
					this.customAI[2] = 0f;
					this.lookAround = true;
					base.npc.netUpdate = true;
				}
			}
			if (this.customAI[4] == 4f)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.customAI[3] += 1f;
				if (this.customAI[3] >= 0f && this.customAI[3] < 83f)
				{
					int num172 = 4;
					for (int j = 0; j < num172; j++)
					{
						int num173 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 50, 0f, 255, 0f, 0f);
						Main.projectile[num173].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)num172 * 6.28f);
						Main.projectile[num173].netUpdate = true;
					}
				}
				if (this.customAI[3] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[3] >= 87f && this.customAI[3] < 140f)
				{
					int num174 = 4;
					for (int k = 0; k < num174; k++)
					{
						int num175 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
						Main.projectile[num175].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)num174 * 6.28f);
						Main.projectile[num175].netUpdate = true;
					}
				}
				if (this.customAI[3] >= 100f && this.customAI[3] < 163f)
				{
					int num176 = 6;
					for (int l = 0; l < num176; l++)
					{
						int num177 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 50, 0f, 255, 0f, 0f);
						Main.projectile[num177].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)num176 * 6.28f);
						Main.projectile[num177].netUpdate = true;
					}
				}
				if (this.customAI[3] == 102f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[3] >= 167f && this.customAI[3] < 200f)
				{
					int num178 = 6;
					for (int m = 0; m < num178; m++)
					{
						int num179 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
						Main.projectile[num179].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m / (float)num178 * 6.28f);
						Main.projectile[num179].netUpdate = true;
					}
				}
				if (this.customAI[3] >= 200f && this.customAI[3] < 243f)
				{
					int num180 = 8;
					for (int n = 0; n < num180; n++)
					{
						int num181 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 50, 0f, 255, 0f, 0f);
						Main.projectile[num181].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)num180 * 6.28f);
						Main.projectile[num181].netUpdate = true;
					}
				}
				if (this.customAI[3] == 202f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[3] >= 247f && this.customAI[3] < 267f)
				{
					int num182 = 8;
					for (int num183 = 0; num183 < num182; num183++)
					{
						int num184 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
						Main.projectile[num184].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)num183 / (float)num182 * 6.28f);
						Main.projectile[num184].netUpdate = true;
					}
				}
				if (this.customAI[3] >= 340f)
				{
					this.customAI[4] = 0f;
					this.laserBeam = false;
					this.customAI[3] = 0f;
					this.lookAround = true;
					base.npc.netUpdate = true;
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.beginFight;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroOpen");
			Texture2D texture2 = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroLookAround");
			Texture2D texture3 = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroBlink");
			Texture2D texture4 = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroLaser");
			Texture2D texture5 = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroBody");
			Texture2D texture6 = base.mod.GetTexture("NPCs/LabNPCs/SlimeThings");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 vector;
			vector..ctor(base.npc.Center.X - 46f, base.npc.Center.Y);
			int num = texture6.Height / 2;
			int num2 = num * this.sludgeFrame;
			Main.spriteBatch.Draw(texture6, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture6.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture6.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			Vector2 vector2;
			vector2..ctor(base.npc.Center.X, base.npc.Center.Y + 26f);
			int num3 = texture5.Height / 4;
			int num4 = num3 * this.bodyFrame;
			Main.spriteBatch.Draw(texture5, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture5.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture5.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			if (!this.openEye && !this.lookAround && !this.blink && !this.laserBeam)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.openEye)
			{
				Vector2 vector3;
				vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num5 = texture.Height / 4;
				int num6 = num5 * this.openFrame;
				Main.spriteBatch.Draw(texture, vector3 - Main.screenPosition, new Rectangle?(new Rectangle(0, num6, texture.Width, num5)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num5 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.lookAround)
			{
				Vector2 vector4;
				vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num7 = texture2.Height / 8;
				int num8 = num7 * this.lookFrame;
				Main.spriteBatch.Draw(texture2, vector4 - Main.screenPosition, new Rectangle?(new Rectangle(0, num8, texture2.Width, num7)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num7 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.blink)
			{
				Vector2 vector5;
				vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num9 = texture3.Height / 7;
				int num10 = num9 * this.blinkFrame;
				Main.spriteBatch.Draw(texture3, vector5 - Main.screenPosition, new Rectangle?(new Rectangle(0, num10, texture3.Width, num9)), drawColor, base.npc.rotation, new Vector2((float)texture3.Width / 2f, (float)num9 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.laserBeam)
			{
				Vector2 vector6;
				vector6..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num11 = texture4.Height / 2;
				int num12 = num11 * this.laserFrame;
				Main.spriteBatch.Draw(texture4, vector6 - Main.screenPosition, new Rectangle?(new Rectangle(0, num12, texture4.Width, num11)), drawColor, base.npc.rotation, new Vector2((float)texture4.Width / 2f, (float)num11 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool beginFight;

		private bool openEye;

		private bool lookAround;

		private bool blink;

		private bool laserBeam;

		private int openFrame;

		private int lookFrame;

		private int blinkFrame;

		private int laserFrame;

		private int openCounter;

		private int lookCounter;

		private int blinkCounter;

		private int laserCounter;

		private bool phase2Done;

		private bool phase2;

		private bool phase3;

		private bool phase3Done;

		private bool sludgyThing;

		private bool bigBody;

		private int bodyCounter;

		private int bodyFrame;

		private int sludgeFrame;

		private int sludgeCounter;

		public float[] customAI = new float[5];
	}
}
