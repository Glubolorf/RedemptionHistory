using System;
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
			base.npc.damage = 100;
			base.npc.defense = 20;
			base.npc.lifeMax = 650000;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(2, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.boss = true;
			base.npc.noTileCollide = true;
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
			potionType = 58;
			RedeWorld.downedPatientZero = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
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
			this.startTimer++;
			if (this.startTimer == 1)
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
			if (this.startTimer == 80)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (this.startTimer == 90)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (this.startTimer == 109)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (this.startTimer == 121)
			{
				Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (this.startTimer > 500)
			{
				this.beginFight = true;
			}
			if (this.startTimer > 500 && this.startTimer <= 580)
			{
				this.openEye = true;
			}
			if (this.startTimer > 580)
			{
				this.openEye = false;
				this.lookAround = true;
			}
			if (this.beginFight)
			{
				this.fightTimer++;
				if (this.fightTimer == 260)
				{
					this.lookAround = false;
					this.attackLaser1 = true;
				}
				if (this.fightTimer == 520)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num2 = 8f;
					Vector2 vector2;
					vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num3 = 40;
					int num4 = base.mod.ProjectileType("PatientBlast");
					float num5 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 580)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num6 = 8f;
					Vector2 vector3;
					vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num7 = 40;
					int num8 = base.mod.ProjectileType("PatientBlast");
					float num9 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num9) * (double)num6 * -1.0), (float)(Math.Sin((double)num9) * (double)num6 * -1.0), num8, num7, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 620)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num10 = 8f;
					Vector2 vector4;
					vector4..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num11 = 40;
					int num12 = base.mod.ProjectileType("PatientBlast");
					float num13 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num13) * (double)num10 * -1.0), (float)(Math.Sin((double)num13) * (double)num10 * -1.0), num12, num11, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 660)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num14 = 8f;
					Vector2 vector5;
					vector5..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num15 = 40;
					int num16 = base.mod.ProjectileType("PatientBlast");
					float num17 = (float)Math.Atan2((double)(vector5.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector5.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector5.X, vector5.Y, (float)(Math.Cos((double)num17) * (double)num14 * -1.0), (float)(Math.Sin((double)num17) * (double)num14 * -1.0), num16, num15, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 820)
				{
					this.lookAround = false;
					this.attackLaser2 = true;
				}
				if (this.fightTimer == 1180)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num18 = 8f;
					Vector2 vector6;
					vector6..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num19 = 40;
					int num20 = base.mod.ProjectileType("PatientBlast");
					float num21 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num21) * (double)num18 * -1.0), (float)(Math.Sin((double)num21) * (double)num18 * -1.0), num20, num19, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 1240)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num22 = 8f;
					Vector2 vector7;
					vector7..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num23 = 40;
					int num24 = base.mod.ProjectileType("PatientBlast");
					float num25 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num25) * (double)num22 * -1.0), (float)(Math.Sin((double)num25) * (double)num22 * -1.0), num24, num23, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 1300)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num26 = 8f;
					Vector2 vector8;
					vector8..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num27 = 40;
					int num28 = base.mod.ProjectileType("PatientBlast");
					float num29 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0), (float)(Math.Sin((double)num29) * (double)num26 * -1.0), num28, num27, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 1360)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num30 = 8f;
					Vector2 vector9;
					vector9..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num31 = 40;
					int num32 = base.mod.ProjectileType("PatientBlast");
					float num33 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num33) * (double)num30 * -1.0), (float)(Math.Sin((double)num33) * (double)num30 * -1.0), num32, num31, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer >= 1480)
				{
					this.fightTimer = 180;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (base.npc.life <= 550000 && !this.phase2Done && !this.laserBeam)
			{
				this.phase2 = true;
			}
			if (this.phase2)
			{
				this.fightTimer = 0;
				this.beginFight = false;
				this.fightTimer2++;
				if (this.fightTimer2 == 60)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer2 == 80)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer2 == 95)
				{
					this.blink = false;
					this.lookAround = true;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer2 == 110)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer2 == 600)
				{
					this.lookAround = false;
					this.attackLaser3 = true;
				}
				if (this.fightTimer2 == 1350)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num34 = 8f;
					Vector2 vector10;
					vector10..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num35 = 40;
					int num36 = base.mod.ProjectileType("PatientBlast");
					float num37 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num37) * (double)num34 * -1.0), (float)(Math.Sin((double)num37) * (double)num34 * -1.0), num36, num35, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num37) * (double)num34 * -1.0) + -1f, (float)(Math.Sin((double)num37) * (double)num34 * -1.0) + -1f, num36, num35, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num37) * (double)num34 * -1.0) + 1f, (float)(Math.Sin((double)num37) * (double)num34 * -1.0) + 1f, num36, num35, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 1450)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num38 = 8f;
					Vector2 vector11;
					vector11..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num39 = 40;
					int num40 = base.mod.ProjectileType("PatientBlast");
					float num41 = (float)Math.Atan2((double)(vector11.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector11.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num41) * (double)num38 * -1.0), (float)(Math.Sin((double)num41) * (double)num38 * -1.0), num40, num39, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num41) * (double)num38 * -1.0) + -1f, (float)(Math.Sin((double)num41) * (double)num38 * -1.0) + -1f, num40, num39, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)num41) * (double)num38 * -1.0) + 1f, (float)(Math.Sin((double)num41) * (double)num38 * -1.0) + 1f, num40, num39, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 1550)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num42 = 8f;
					Vector2 vector12;
					vector12..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num43 = 40;
					int num44 = base.mod.ProjectileType("PatientBlast");
					float num45 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num45) * (double)num42 * -1.0), (float)(Math.Sin((double)num45) * (double)num42 * -1.0), num44, num43, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num45) * (double)num42 * -1.0) + -1f, (float)(Math.Sin((double)num45) * (double)num42 * -1.0) + -1f, num44, num43, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num45) * (double)num42 * -1.0) + 1f, (float)(Math.Sin((double)num45) * (double)num42 * -1.0) + 1f, num44, num43, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 1800)
				{
					this.lookAround = false;
					this.attackLaser1 = true;
				}
				if (this.fightTimer2 == 1920)
				{
					this.lookAround = false;
					this.attackLaser2 = true;
				}
				if (this.fightTimer2 == 2300)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num46 = 8f;
					Vector2 vector13;
					vector13..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num47 = 40;
					int num48 = base.mod.ProjectileType("PatientBlast");
					float num49 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num49) * (double)num46 * -1.0), (float)(Math.Sin((double)num49) * (double)num46 * -1.0), num48, num47, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num49) * (double)num46 * -1.0) + -1f, (float)(Math.Sin((double)num49) * (double)num46 * -1.0) + -1f, num48, num47, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num49) * (double)num46 * -1.0) + 1f, (float)(Math.Sin((double)num49) * (double)num46 * -1.0) + 1f, num48, num47, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 2360)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num50 = 8f;
					Vector2 vector14;
					vector14..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num51 = 40;
					int num52 = base.mod.ProjectileType("PatientBlast");
					float num53 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num53) * (double)num50 * -1.0), (float)(Math.Sin((double)num53) * (double)num50 * -1.0), num52, num51, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num53) * (double)num50 * -1.0) + -1f, (float)(Math.Sin((double)num53) * (double)num50 * -1.0) + -1f, num52, num51, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num53) * (double)num50 * -1.0) + 1f, (float)(Math.Sin((double)num53) * (double)num50 * -1.0) + 1f, num52, num51, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 2420)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num54 = 8f;
					Vector2 vector15;
					vector15..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num55 = 40;
					int num56 = base.mod.ProjectileType("PatientBlast");
					float num57 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num57) * (double)num54 * -1.0), (float)(Math.Sin((double)num57) * (double)num54 * -1.0), num56, num55, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num57) * (double)num54 * -1.0) + -1f, (float)(Math.Sin((double)num57) * (double)num54 * -1.0) + -1f, num56, num55, 0f, 0, 0f, 0f);
					Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num57) * (double)num54 * -1.0) + 1f, (float)(Math.Sin((double)num57) * (double)num54 * -1.0) + 1f, num56, num55, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 >= 2550)
				{
					this.fightTimer2 = 500;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (base.npc.life <= 350000 && !this.phase3Done && !this.laserBeam)
			{
				this.phase3 = true;
				this.phase2Done = true;
			}
			if (this.phase3)
			{
				this.fightTimer = 0;
				this.fightTimer2 = 0;
				this.phase2Done = true;
				this.phase2 = false;
				this.fightTimer3++;
				if (this.fightTimer3 == 35)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer3 == 60)
				{
					this.blink = true;
					this.lookAround = false;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer3 == 80)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer3 == 95)
				{
					this.blink = false;
					this.lookAround = true;
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer3 == 110)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth"), 0, 0f, 0f, 0f, 0f, 255);
				}
				if (this.fightTimer3 == 700)
				{
					this.lookAround = false;
					this.attackLaser4 = true;
				}
				if (this.fightTimer3 == 1290)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num58 = 8f;
					Vector2 vector16;
					vector16..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num59 = 40;
					int num60 = base.mod.ProjectileType("PatientBlast");
					float num61 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)num61) * (double)num58 * -1.0), (float)(Math.Sin((double)num61) * (double)num58 * -1.0), num60, num59, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1340)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num62 = 8f;
					Vector2 vector17;
					vector17..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num63 = 40;
					int num64 = base.mod.ProjectileType("PatientBlast");
					float num65 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)num65) * (double)num62 * -1.0), (float)(Math.Sin((double)num65) * (double)num62 * -1.0), num64, num63, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1380)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num66 = 8f;
					Vector2 vector18;
					vector18..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num67 = 40;
					int num68 = base.mod.ProjectileType("PatientBlast");
					float num69 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)num69) * (double)num66 * -1.0), (float)(Math.Sin((double)num69) * (double)num66 * -1.0), num68, num67, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1410)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num70 = 8f;
					Vector2 vector19;
					vector19..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num71 = 40;
					int num72 = base.mod.ProjectileType("PatientBlast");
					float num73 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)num73) * (double)num70 * -1.0), (float)(Math.Sin((double)num73) * (double)num70 * -1.0), num72, num71, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1430)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num74 = 8f;
					Vector2 vector20;
					vector20..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num75 = 40;
					int num76 = base.mod.ProjectileType("PatientBlast");
					float num77 = (float)Math.Atan2((double)(vector20.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector20.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)num77) * (double)num74 * -1.0), (float)(Math.Sin((double)num77) * (double)num74 * -1.0), num76, num75, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 1600)
				{
					this.lookAround = false;
					this.attackLaser2 = true;
				}
				if (this.fightTimer3 == 1790)
				{
					this.lookAround = false;
					this.attackLaser1 = true;
				}
				if (this.fightTimer3 == 1980)
				{
					this.lookAround = false;
					this.attackLaser2 = true;
				}
				if (this.fightTimer3 == 2300)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num78 = 8f;
					Vector2 vector21;
					vector21..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num79 = 40;
					int num80 = base.mod.ProjectileType("PatientBlast");
					float num81 = (float)Math.Atan2((double)(vector21.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector21.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)num81) * (double)num78 * -1.0), (float)(Math.Sin((double)num81) * (double)num78 * -1.0), num80, num79, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 2400)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num82 = 8f;
					Vector2 vector22;
					vector22..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num83 = 40;
					int num84 = base.mod.ProjectileType("PatientBlast");
					float num85 = (float)Math.Atan2((double)(vector22.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector22.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector22.X, vector22.Y, (float)(Math.Cos((double)num85) * (double)num82 * -1.0), (float)(Math.Sin((double)num85) * (double)num82 * -1.0), num84, num83, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 2500)
				{
					Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num86 = 8f;
					Vector2 vector23;
					vector23..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num87 = 40;
					int num88 = base.mod.ProjectileType("PatientBlast");
					float num89 = (float)Math.Atan2((double)(vector23.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector23.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector23.X, vector23.Y, (float)(Math.Cos((double)num89) * (double)num86 * -1.0), (float)(Math.Sin((double)num89) * (double)num86 * -1.0), num88, num87, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 >= 2650)
				{
					this.fightTimer3 = 500;
				}
				if (NPC.CountNPCS(base.mod.NPCType("HiveGrowth2")) <= 1 && Main.rand.Next(300) == 0)
				{
					Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("HiveGrowth2"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
			if (this.attackLaser1)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.attackLaser1Timer++;
				if (this.attackLaser1Timer >= 0 && this.attackLaser1Timer < 83)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(20f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-20f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 20f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -20f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser1Timer == 2 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser1Timer >= 87 && this.attackLaser1Timer < 170)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(20f, 0f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-20f, 0f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 20f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -20f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser1Timer >= 190)
				{
					this.attackLaser1 = false;
					this.laserBeam = false;
					this.attackLaser1Timer = 0;
					this.lookAround = true;
				}
			}
			if (this.attackLaser2)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.attackLaser2Timer++;
				if (this.attackLaser2Timer >= 0 && this.attackLaser2Timer < 83)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser2Timer == 2 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser2Timer >= 87 && this.attackLaser2Timer < 170)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 10f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 10f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, -10f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, -10f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser2Timer >= 190)
				{
					this.attackLaser2 = false;
					this.laserBeam = false;
					this.attackLaser2Timer = 0;
					this.lookAround = true;
				}
			}
			if (this.attackLaser3)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.attackLaser3Timer++;
				if (this.attackLaser3Timer >= 0 && this.attackLaser3Timer < 83)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 2 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 87 && this.attackLaser3Timer < 170)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, -10f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 60 && this.attackLaser3Timer < 143)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 62 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 147 && this.attackLaser3Timer < 230)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, -5f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 120 && this.attackLaser3Timer < 203)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 122 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 207 && this.attackLaser3Timer < 290)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(10f, 0f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 180 && this.attackLaser3Timer < 263)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 182 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 267 && this.attackLaser3Timer < 350)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(5f, 5f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 240 && this.attackLaser3Timer < 323)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 242 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 327 && this.attackLaser3Timer < 410)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 10f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 300 && this.attackLaser3Timer < 383)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 302 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 387 && this.attackLaser3Timer < 470)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, 5f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 360 && this.attackLaser3Timer < 443)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 362 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 447 && this.attackLaser3Timer < 530)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-10f, 0f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 420 && this.attackLaser3Timer < 503)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser2"), 0, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer == 422 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.5f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser3Timer >= 507 && this.attackLaser3Timer < 590)
				{
					Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(-5f, -5f), base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
				}
				if (this.attackLaser3Timer >= 650)
				{
					this.attackLaser3 = false;
					this.laserBeam = false;
					this.attackLaser3Timer = 0;
					this.lookAround = true;
				}
			}
			if (this.attackLaser4)
			{
				this.laserBeam = true;
				this.lookAround = false;
				this.attackLaser4Timer++;
				if (this.attackLaser4Timer >= 0 && this.attackLaser4Timer < 83)
				{
					int num90 = 4;
					for (int j = 0; j < num90; j++)
					{
						int num91 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num91].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)j / (float)num90 * 6.28f);
					}
				}
				if (this.attackLaser4Timer == 2 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser4Timer >= 87 && this.attackLaser4Timer < 140)
				{
					int num92 = 4;
					for (int k = 0; k < num92; k++)
					{
						int num93 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num93].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)k / (float)num92 * 6.28f);
					}
				}
				if (this.attackLaser4Timer >= 100 && this.attackLaser4Timer < 163)
				{
					int num94 = 6;
					for (int l = 0; l < num94; l++)
					{
						int num95 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num95].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)l / (float)num94 * 6.28f);
					}
				}
				if (this.attackLaser4Timer == 102 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser4Timer >= 167 && this.attackLaser4Timer < 200)
				{
					int num96 = 6;
					for (int m = 0; m < num96; m++)
					{
						int num97 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num97].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)m / (float)num96 * 6.28f);
					}
				}
				if (this.attackLaser4Timer >= 200 && this.attackLaser4Timer < 243)
				{
					int num98 = 8;
					for (int n = 0; n < num98; n++)
					{
						int num99 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser2"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num99].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)num98 * 6.28f);
					}
				}
				if (this.attackLaser4Timer == 202 && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/MegaLaser1").WithVolume(0.9f).WithPitchVariance(0f), (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackLaser4Timer >= 247 && this.attackLaser4Timer < 267)
				{
					int num100 = 8;
					for (int num101 = 0; num101 < num100; num101++)
					{
						int num102 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 0f, base.mod.ProjectileType("PatientLaser"), 40, 0f, 255, 0f, 0f);
						Main.projectile[num102].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)num101 / (float)num100 * 6.28f);
					}
				}
				if (this.attackLaser4Timer >= 340)
				{
					this.attackLaser4 = false;
					this.laserBeam = false;
					this.attackLaser4Timer = 0;
					this.lookAround = true;
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

		private int startTimer;

		private bool beginFight;

		private int fightTimer;

		private int spamTimer;

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

		private bool attackLaser1;

		private int attackLaser1Timer;

		private bool attackLaser2;

		private int attackLaser2Timer;

		private bool phase2Done;

		private bool phase2;

		private int fightTimer2;

		private bool attackLaser3;

		private int attackLaser3Timer;

		private bool phase3;

		private int fightTimer3;

		private bool phase3Done;

		private bool attackLaser4;

		private int attackLaser4Timer;

		private bool sludgyThing;

		private bool bigBody;

		private int bodyCounter;

		private int bodyFrame;

		private int sludgeFrame;

		private int sludgeCounter;
	}
}
