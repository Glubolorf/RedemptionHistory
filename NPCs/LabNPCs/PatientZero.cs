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
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 1.9f;
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
					Vector2 newPos = new Vector2(50f, -183f);
					base.npc.Center = base.npc.position + newPos;
					base.npc.netUpdate = true;
				}
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 74, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			if (base.npc.alpha > 0)
			{
				base.npc.alpha -= 4;
			}
			if (base.npc.ai[0] == 80f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
			if (base.npc.ai[0] == 90f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int minion2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion2].netUpdate = true;
			}
			if (base.npc.ai[0] == 109f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int minion3 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion3].netUpdate = true;
			}
			if (base.npc.ai[0] == 121f)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				int minion4 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion4].netUpdate = true;
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
					float Speed = 8f;
					Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage = 50;
					int type = base.mod.ProjectileType("PatientBlast");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
				}
				if (base.npc.ai[1] == 580f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed2 = 8f;
					Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage2 = 50;
					int type2 = base.mod.ProjectileType("PatientBlast");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
				}
				if (base.npc.ai[1] == 620f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed3 = 8f;
					Vector2 vector10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage3 = 50;
					int type3 = base.mod.ProjectileType("PatientBlast");
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
				}
				if (base.npc.ai[1] == 660f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed4 = 8f;
					Vector2 vector11 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage4 = 50;
					int type4 = base.mod.ProjectileType("PatientBlast");
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
					Main.projectile[num57].netUpdate = true;
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
					float Speed5 = 8f;
					Vector2 vector12 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage5 = 50;
					int type5 = base.mod.ProjectileType("PatientBlast");
					float rotation5 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					int num58 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
					Main.projectile[num58].netUpdate = true;
				}
				if (base.npc.ai[1] == 1240f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed6 = 8f;
					Vector2 vector13 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage6 = 50;
					int type6 = base.mod.ProjectileType("PatientBlast");
					float rotation6 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					int num59 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
					Main.projectile[num59].netUpdate = true;
				}
				if (base.npc.ai[1] == 1300f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed7 = 8f;
					Vector2 vector14 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage7 = 50;
					int type7 = base.mod.ProjectileType("PatientBlast");
					float rotation7 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					int num60 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation7) * (double)Speed7 * -1.0), (float)(Math.Sin((double)rotation7) * (double)Speed7 * -1.0), type7, damage7, 0f, 0, 0f, 0f);
					Main.projectile[num60].netUpdate = true;
				}
				if (base.npc.ai[1] == 1360f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed8 = 8f;
					Vector2 vector15 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage8 = 50;
					int type8 = base.mod.ProjectileType("PatientBlast");
					float rotation8 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
					int num61 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation8) * (double)Speed8 * -1.0), (float)(Math.Sin((double)rotation8) * (double)Speed8 * -1.0), type8, damage8, 0f, 0, 0f, 0f);
					Main.projectile[num61].netUpdate = true;
				}
				if (base.npc.ai[1] >= 1480f)
				{
					base.npc.ai[1] = 180f;
					base.npc.netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion5 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion5].netUpdate = true;
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
					int minion6 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion6].netUpdate = true;
				}
				if (base.npc.ai[2] == 80f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion7 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion7].netUpdate = true;
				}
				if (base.npc.ai[2] == 95f)
				{
					this.blink = false;
					this.lookAround = true;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion8 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion8].netUpdate = true;
				}
				if (base.npc.ai[2] == 110f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion9 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion9].netUpdate = true;
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
					float Speed9 = 8f;
					Vector2 vector16 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage9 = 50;
					int type9 = base.mod.ProjectileType("PatientBlast");
					float rotation9 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					int num62 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation9) * (double)Speed9 * -1.0), (float)(Math.Sin((double)rotation9) * (double)Speed9 * -1.0), type9, damage9, 0f, 0, 0f, 0f);
					int num63 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation9) * (double)Speed9 * -1.0) + -1f, (float)(Math.Sin((double)rotation9) * (double)Speed9 * -1.0) + -1f, type9, damage9, 0f, 0, 0f, 0f);
					int num64 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation9) * (double)Speed9 * -1.0) + 1f, (float)(Math.Sin((double)rotation9) * (double)Speed9 * -1.0) + 1f, type9, damage9, 0f, 0, 0f, 0f);
					Main.projectile[num62].netUpdate = true;
					Main.projectile[num63].netUpdate = true;
					Main.projectile[num64].netUpdate = true;
				}
				if (base.npc.ai[2] == 1450f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed10 = 8f;
					Vector2 vector17 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage10 = 50;
					int type10 = base.mod.ProjectileType("PatientBlast");
					float rotation10 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
					int num65 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0), (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0), type10, damage10, 0f, 0, 0f, 0f);
					int num66 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0) + -1f, (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0) + -1f, type10, damage10, 0f, 0, 0f, 0f);
					int num67 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0) + 1f, (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0) + 1f, type10, damage10, 0f, 0, 0f, 0f);
					Main.projectile[num65].netUpdate = true;
					Main.projectile[num66].netUpdate = true;
					Main.projectile[num67].netUpdate = true;
				}
				if (base.npc.ai[2] == 1550f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed11 = 8f;
					Vector2 vector18 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage11 = 50;
					int type11 = base.mod.ProjectileType("PatientBlast");
					float rotation11 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
					int num68 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)rotation11) * (double)Speed11 * -1.0), (float)(Math.Sin((double)rotation11) * (double)Speed11 * -1.0), type11, damage11, 0f, 0, 0f, 0f);
					int num69 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)rotation11) * (double)Speed11 * -1.0) + -1f, (float)(Math.Sin((double)rotation11) * (double)Speed11 * -1.0) + -1f, type11, damage11, 0f, 0, 0f, 0f);
					int num70 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)rotation11) * (double)Speed11 * -1.0) + 1f, (float)(Math.Sin((double)rotation11) * (double)Speed11 * -1.0) + 1f, type11, damage11, 0f, 0, 0f, 0f);
					Main.projectile[num68].netUpdate = true;
					Main.projectile[num69].netUpdate = true;
					Main.projectile[num70].netUpdate = true;
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
					float Speed12 = 8f;
					Vector2 vector19 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage12 = 50;
					int type12 = base.mod.ProjectileType("PatientBlast");
					float rotation12 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
					int num71 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0), (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0), type12, damage12, 0f, 0, 0f, 0f);
					int num72 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0) + -1f, (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0) + -1f, type12, damage12, 0f, 0, 0f, 0f);
					int num73 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0) + 1f, (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0) + 1f, type12, damage12, 0f, 0, 0f, 0f);
					Main.projectile[num71].netUpdate = true;
					Main.projectile[num72].netUpdate = true;
					Main.projectile[num73].netUpdate = true;
				}
				if (base.npc.ai[2] == 2360f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed13 = 8f;
					Vector2 vector20 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage13 = 50;
					int type13 = base.mod.ProjectileType("PatientBlast");
					float rotation13 = (float)Math.Atan2((double)(vector20.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector20.X - (player.position.X + (float)player.width * 0.5f)));
					int num74 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)rotation13) * (double)Speed13 * -1.0), (float)(Math.Sin((double)rotation13) * (double)Speed13 * -1.0), type13, damage13, 0f, 0, 0f, 0f);
					int num75 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)rotation13) * (double)Speed13 * -1.0) + -1f, (float)(Math.Sin((double)rotation13) * (double)Speed13 * -1.0) + -1f, type13, damage13, 0f, 0, 0f, 0f);
					int num76 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)rotation13) * (double)Speed13 * -1.0) + 1f, (float)(Math.Sin((double)rotation13) * (double)Speed13 * -1.0) + 1f, type13, damage13, 0f, 0, 0f, 0f);
					Main.projectile[num74].netUpdate = true;
					Main.projectile[num75].netUpdate = true;
					Main.projectile[num76].netUpdate = true;
				}
				if (base.npc.ai[2] == 2420f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed14 = 8f;
					Vector2 vector21 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage14 = 50;
					int type14 = base.mod.ProjectileType("PatientBlast");
					float rotation14 = (float)Math.Atan2((double)(vector21.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector21.X - (player.position.X + (float)player.width * 0.5f)));
					int num77 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0), (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0), type14, damage14, 0f, 0, 0f, 0f);
					int num78 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0) + -1f, (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0) + -1f, type14, damage14, 0f, 0, 0f, 0f);
					int num79 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0) + 1f, (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0) + 1f, type14, damage14, 0f, 0, 0f, 0f);
					Main.projectile[num77].netUpdate = true;
					Main.projectile[num78].netUpdate = true;
					Main.projectile[num79].netUpdate = true;
				}
				if (base.npc.ai[2] >= 2550f)
				{
					base.npc.ai[2] = 500f;
					base.npc.netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion10 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion10].netUpdate = true;
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
					int minion11 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion11].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion12 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion12].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion13 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion13].netUpdate = true;
				}
				if (base.npc.ai[3] == 95f)
				{
					this.blink = false;
					this.lookAround = true;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion14 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion14].netUpdate = true;
				}
				if (base.npc.ai[3] == 110f)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion15 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion15].netUpdate = true;
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
					float Speed15 = 8f;
					Vector2 vector22 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage15 = 50;
					int type15 = base.mod.ProjectileType("PatientBlast");
					float rotation15 = (float)Math.Atan2((double)(vector22.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector22.X - (player.position.X + (float)player.width * 0.5f)));
					int num80 = Projectile.NewProjectile(vector22.X, vector22.Y, (float)(Math.Cos((double)rotation15) * (double)Speed15 * -1.0), (float)(Math.Sin((double)rotation15) * (double)Speed15 * -1.0), type15, damage15, 0f, 0, 0f, 0f);
					Main.projectile[num80].netUpdate = true;
				}
				if (base.npc.ai[3] == 1340f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed16 = 8f;
					Vector2 vector23 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage16 = 50;
					int type16 = base.mod.ProjectileType("PatientBlast");
					float rotation16 = (float)Math.Atan2((double)(vector23.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector23.X - (player.position.X + (float)player.width * 0.5f)));
					int num81 = Projectile.NewProjectile(vector23.X, vector23.Y, (float)(Math.Cos((double)rotation16) * (double)Speed16 * -1.0), (float)(Math.Sin((double)rotation16) * (double)Speed16 * -1.0), type16, damage16, 0f, 0, 0f, 0f);
					Main.projectile[num81].netUpdate = true;
				}
				if (base.npc.ai[3] == 1380f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed17 = 8f;
					Vector2 vector24 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage17 = 50;
					int type17 = base.mod.ProjectileType("PatientBlast");
					float rotation17 = (float)Math.Atan2((double)(vector24.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector24.X - (player.position.X + (float)player.width * 0.5f)));
					int num82 = Projectile.NewProjectile(vector24.X, vector24.Y, (float)(Math.Cos((double)rotation17) * (double)Speed17 * -1.0), (float)(Math.Sin((double)rotation17) * (double)Speed17 * -1.0), type17, damage17, 0f, 0, 0f, 0f);
					Main.projectile[num82].netUpdate = true;
				}
				if (base.npc.ai[3] == 1410f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed18 = 8f;
					Vector2 vector25 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage18 = 50;
					int type18 = base.mod.ProjectileType("PatientBlast");
					float rotation18 = (float)Math.Atan2((double)(vector25.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector25.X - (player.position.X + (float)player.width * 0.5f)));
					int num83 = Projectile.NewProjectile(vector25.X, vector25.Y, (float)(Math.Cos((double)rotation18) * (double)Speed18 * -1.0), (float)(Math.Sin((double)rotation18) * (double)Speed18 * -1.0), type18, damage18, 0f, 0, 0f, 0f);
					Main.projectile[num83].netUpdate = true;
				}
				if (base.npc.ai[3] == 1430f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed19 = 8f;
					Vector2 vector26 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage19 = 50;
					int type19 = base.mod.ProjectileType("PatientBlast");
					float rotation19 = (float)Math.Atan2((double)(vector26.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector26.X - (player.position.X + (float)player.width * 0.5f)));
					int num84 = Projectile.NewProjectile(vector26.X, vector26.Y, (float)(Math.Cos((double)rotation19) * (double)Speed19 * -1.0), (float)(Math.Sin((double)rotation19) * (double)Speed19 * -1.0), type19, damage19, 0f, 0, 0f, 0f);
					Main.projectile[num84].netUpdate = true;
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
					float Speed20 = 8f;
					Vector2 vector27 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage20 = 50;
					int type20 = base.mod.ProjectileType("PatientBlast");
					float rotation20 = (float)Math.Atan2((double)(vector27.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector27.X - (player.position.X + (float)player.width * 0.5f)));
					int num85 = Projectile.NewProjectile(vector27.X, vector27.Y, (float)(Math.Cos((double)rotation20) * (double)Speed20 * -1.0), (float)(Math.Sin((double)rotation20) * (double)Speed20 * -1.0), type20, damage20, 0f, 0, 0f, 0f);
					Main.projectile[num85].netUpdate = true;
				}
				if (base.npc.ai[3] == 2400f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed21 = 8f;
					Vector2 vector28 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage21 = 50;
					int type21 = base.mod.ProjectileType("PatientBlast");
					float rotation21 = (float)Math.Atan2((double)(vector28.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector28.X - (player.position.X + (float)player.width * 0.5f)));
					int num86 = Projectile.NewProjectile(vector28.X, vector28.Y, (float)(Math.Cos((double)rotation21) * (double)Speed21 * -1.0), (float)(Math.Sin((double)rotation21) * (double)Speed21 * -1.0), type21, damage21, 0f, 0, 0f, 0f);
					Main.projectile[num86].netUpdate = true;
				}
				if (base.npc.ai[3] == 2500f)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float Speed22 = 8f;
					Vector2 vector29 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage22 = 50;
					int type22 = base.mod.ProjectileType("PatientBlast");
					float rotation22 = (float)Math.Atan2((double)(vector29.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector29.X - (player.position.X + (float)player.width * 0.5f)));
					int num87 = Projectile.NewProjectile(vector29.X, vector29.Y, (float)(Math.Cos((double)rotation22) * (double)Speed22 * -1.0), (float)(Math.Sin((double)rotation22) * (double)Speed22 * -1.0), type22, damage22, 0f, 0, 0f, 0f);
					Main.projectile[num87].netUpdate = true;
				}
				if (base.npc.ai[3] >= 2650f)
				{
					base.npc.ai[3] = 500f;
					base.npc.netUpdate = true;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					int minion16 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion16].netUpdate = true;
				}
			}
			if (this.customAI[4] == 1f)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.customAI[0] += 1f;
				if (this.customAI[0] >= 0f && this.customAI[0] < 83f)
				{
					int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(20f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int p2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-20f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int p3 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 20f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int p4 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -20f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
					Main.projectile[p2].netUpdate = true;
					Main.projectile[p3].netUpdate = true;
					Main.projectile[p4].netUpdate = true;
				}
				if (this.customAI[0] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[0] >= 87f && this.customAI[0] < 170f)
				{
					int p5 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(20f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int p6 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-20f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int p7 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 20f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int p8 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -20f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p5].netUpdate = true;
					Main.projectile[p6].netUpdate = true;
					Main.projectile[p7].netUpdate = true;
					Main.projectile[p8].netUpdate = true;
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
					int p9 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int p10 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int p11 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					int p12 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
					Main.projectile[p10].netUpdate = true;
					Main.projectile[p11].netUpdate = true;
					Main.projectile[p12].netUpdate = true;
				}
				if (this.customAI[1] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[1] >= 87f && this.customAI[1] < 170f)
				{
					int p13 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int p14 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int p15 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, -10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					int p16 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p13].netUpdate = true;
					Main.projectile[p14].netUpdate = true;
					Main.projectile[p15].netUpdate = true;
					Main.projectile[p16].netUpdate = true;
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
					int p17 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p17].netUpdate = true;
				}
				if (this.customAI[2] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 87f && this.customAI[2] < 170f)
				{
					int p18 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p18].netUpdate = true;
				}
				if (this.customAI[2] >= 60f && this.customAI[2] < 143f)
				{
					int p19 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p19].netUpdate = true;
				}
				if (this.customAI[2] == 62f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 147f && this.customAI[2] < 230f)
				{
					int p20 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p20].netUpdate = true;
				}
				if (this.customAI[2] >= 120f && this.customAI[2] < 203f)
				{
					int p21 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p21].netUpdate = true;
				}
				if (this.customAI[2] == 122f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 207f && this.customAI[2] < 290f)
				{
					int p22 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p22].netUpdate = true;
				}
				if (this.customAI[2] >= 180f && this.customAI[2] < 263f)
				{
					int p23 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p23].netUpdate = true;
				}
				if (this.customAI[2] == 182f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 267f && this.customAI[2] < 350f)
				{
					int p24 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p24].netUpdate = true;
				}
				if (this.customAI[2] >= 240f && this.customAI[2] < 323f)
				{
					int p25 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p25].netUpdate = true;
				}
				if (this.customAI[2] == 242f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 327f && this.customAI[2] < 410f)
				{
					int p26 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p26].netUpdate = true;
				}
				if (this.customAI[2] >= 300f && this.customAI[2] < 383f)
				{
					int p27 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p27].netUpdate = true;
				}
				if (this.customAI[2] == 302f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 387f && this.customAI[2] < 470f)
				{
					int p28 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p28].netUpdate = true;
				}
				if (this.customAI[2] >= 360f && this.customAI[2] < 443f)
				{
					int p29 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p29].netUpdate = true;
				}
				if (this.customAI[2] == 362f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 447f && this.customAI[2] < 530f)
				{
					int p30 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p30].netUpdate = true;
				}
				if (this.customAI[2] >= 420f && this.customAI[2] < 503f)
				{
					int p31 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Main.projectile[p31].netUpdate = true;
				}
				if (this.customAI[2] == 422f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[2] >= 507f && this.customAI[2] < 590f)
				{
					int p32 = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
					Main.projectile[p32].netUpdate = true;
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
					int pieCut = 4;
					for (int j = 0; j < pieCut; j++)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 50, 0f, 255, 0f, 0f);
						Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)pieCut * 6.28f);
						Main.projectile[projID].netUpdate = true;
					}
				}
				if (this.customAI[3] == 2f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[3] >= 87f && this.customAI[3] < 140f)
				{
					int pieCut2 = 4;
					for (int k = 0; k < pieCut2; k++)
					{
						int projID2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
						Main.projectile[projID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)pieCut2 * 6.28f);
						Main.projectile[projID2].netUpdate = true;
					}
				}
				if (this.customAI[3] >= 100f && this.customAI[3] < 163f)
				{
					int pieCut3 = 6;
					for (int l = 0; l < pieCut3; l++)
					{
						int projID3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 50, 0f, 255, 0f, 0f);
						Main.projectile[projID3].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)pieCut3 * 6.28f);
						Main.projectile[projID3].netUpdate = true;
					}
				}
				if (this.customAI[3] == 102f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[3] >= 167f && this.customAI[3] < 200f)
				{
					int pieCut4 = 6;
					for (int m = 0; m < pieCut4; m++)
					{
						int projID4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
						Main.projectile[projID4].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m / (float)pieCut4 * 6.28f);
						Main.projectile[projID4].netUpdate = true;
					}
				}
				if (this.customAI[3] >= 200f && this.customAI[3] < 243f)
				{
					int pieCut5 = 8;
					for (int n = 0; n < pieCut5; n++)
					{
						int projID5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 50, 0f, 255, 0f, 0f);
						Main.projectile[projID5].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)pieCut5 * 6.28f);
						Main.projectile[projID5].netUpdate = true;
					}
				}
				if (this.customAI[3] == 202f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.customAI[3] >= 247f && this.customAI[3] < 267f)
				{
					int pieCut6 = 8;
					for (int m2 = 0; m2 < pieCut6; m2++)
					{
						int projID6 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 50, 0f, 255, 0f, 0f);
						Main.projectile[projID6].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m2 / (float)pieCut6 * 6.28f);
						Main.projectile[projID6].netUpdate = true;
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
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D openAni = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroOpen");
			Texture2D lookAni = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroLookAround");
			Texture2D blinkAni = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroBlink");
			Texture2D laserAni = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroLaser");
			Texture2D bodyAni = base.mod.GetTexture("NPCs/LabNPCs/PatientZeroBody");
			Texture2D sludgeAni = base.mod.GetTexture("NPCs/LabNPCs/SlimeThings");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterC = new Vector2(base.npc.Center.X - 46f, base.npc.Center.Y);
			int num214C = sludgeAni.Height / 2;
			int y6C = num214C * this.sludgeFrame;
			Main.spriteBatch.Draw(sludgeAni, drawCenterC - Main.screenPosition, new Rectangle?(new Rectangle(0, y6C, sludgeAni.Width, num214C)), drawColor, base.npc.rotation, new Vector2((float)sludgeAni.Width / 2f, (float)num214C / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenterB = new Vector2(base.npc.Center.X, base.npc.Center.Y + 26f);
			int num214B = bodyAni.Height / 4;
			int y6B = num214B * this.bodyFrame;
			Main.spriteBatch.Draw(bodyAni, drawCenterB - Main.screenPosition, new Rectangle?(new Rectangle(0, y6B, bodyAni.Width, num214B)), drawColor, base.npc.rotation, new Vector2((float)bodyAni.Width / 2f, (float)num214B / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			if (!this.openEye && !this.lookAround && !this.blink && !this.laserBeam)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.openEye)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = openAni.Height / 4;
				int y6 = num214 * this.openFrame;
				Main.spriteBatch.Draw(openAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, openAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)openAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.lookAround)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = lookAni.Height / 8;
				int y7 = num215 * this.lookFrame;
				Main.spriteBatch.Draw(lookAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, lookAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)lookAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.blink)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = blinkAni.Height / 7;
				int y8 = num216 * this.blinkFrame;
				Main.spriteBatch.Draw(blinkAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, blinkAni.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)blinkAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.laserBeam)
			{
				Vector2 drawCenter4 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num217 = laserAni.Height / 2;
				int y9 = num217 * this.laserFrame;
				Main.spriteBatch.Draw(laserAni, drawCenter4 - Main.screenPosition, new Rectangle?(new Rectangle(0, y9, laserAni.Width, num217)), drawColor, base.npc.rotation, new Vector2((float)laserAni.Width / 2f, (float)num217 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
