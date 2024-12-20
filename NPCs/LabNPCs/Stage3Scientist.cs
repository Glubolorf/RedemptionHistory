using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	[AutoloadBossHead]
	public class Stage3Scientist : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Stage 3 Infected Scientist");
			Main.npcFrameCount[base.npc.type] = 7;
		}

		public override void SetDefaults()
		{
			base.npc.width = 48;
			base.npc.height = 74;
			base.npc.friendly = false;
			base.npc.damage = 120;
			base.npc.defense = 25;
			base.npc.lifeMax = 24000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 3, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.aiStyle = 0;
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/LabBossMusic");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("XenoDust"), 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedStage3Scientist = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk2"), 1, false, 0, false, false);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 76;
				if (base.npc.frame.Y > 456)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			this.spawnTimer++;
			if (this.spawnTimer == 1)
			{
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
				}
				if (Main.netMode != 1)
				{
					Vector2 vector;
					vector..ctor(16f, -100f);
					base.npc.Center = base.npc.position + vector;
					base.npc.netUpdate = true;
				}
			}
			if (this.spawnTimer <= 120)
			{
				base.npc.aiStyle = -1;
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
			}
			if (this.spawnTimer > 120)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
			}
			if (this.beginFight)
			{
				this.fightTimer++;
				if (this.fightTimer == 50)
				{
					float num = 6f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num2 = 40;
					int num3 = base.mod.ProjectileType("XenoShard2");
					float num4 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 55)
				{
					float num5 = 6f;
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num6 = 40;
					int num7 = base.mod.ProjectileType("XenoShard2");
					float num8 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 60)
				{
					float num9 = 6f;
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num10 = 40;
					int num11 = base.mod.ProjectileType("XenoShard2");
					float num12 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num12) * (double)num9 * -1.0), (float)(Math.Sin((double)num12) * (double)num9 * -1.0), num11, num10, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer == 90)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(0f, -8f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 93)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(6f, -6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 96)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(8f, 0f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 99)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(6f, 6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 102)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(0f, 8f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 105)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-6f, 6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 108)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-8f, 0f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 111)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-6f, -6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer == 199)
				{
					for (int i = 0; i < 25; i++)
					{
						int num13 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num13].velocity *= 3f;
						Main.dust[num13].noGravity = true;
					}
				}
				if (this.fightTimer == 200 && Main.netMode != 1)
				{
					Vector2 vector5;
					vector5..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector5;
					base.npc.netUpdate = true;
				}
				if (this.fightTimer == 201)
				{
					for (int j = 0; j < 25; j++)
					{
						int num14 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num14].velocity *= 3f;
						Main.dust[num14].noGravity = true;
					}
					this.fightTimer = 0;
				}
			}
			if (base.npc.life <= 17000 && !this.phase2Done)
			{
				this.phase2 = true;
			}
			if (this.phase2)
			{
				this.fightTimer = 0;
				this.beginFight = false;
				this.fightTimer2++;
				if (this.fightTimer2 == 50)
				{
					float num15 = 9f;
					Vector2 vector6;
					vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num16 = 40;
					int num17 = base.mod.ProjectileType("XenoShard2");
					float num18 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num18) * (double)num15 * -1.0), (float)(Math.Sin((double)num18) * (double)num15 * -1.0), num17, num16, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 55)
				{
					float num19 = 9f;
					Vector2 vector7;
					vector7..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num20 = 40;
					int num21 = base.mod.ProjectileType("XenoShard2");
					float num22 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num22) * (double)num19 * -1.0), (float)(Math.Sin((double)num22) * (double)num19 * -1.0), num21, num20, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 60)
				{
					float num23 = 9f;
					Vector2 vector8;
					vector8..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num24 = 40;
					int num25 = base.mod.ProjectileType("XenoShard2");
					float num26 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num26) * (double)num23 * -1.0), (float)(Math.Sin((double)num26) * (double)num23 * -1.0), num25, num24, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 65)
				{
					float num27 = 9f;
					Vector2 vector9;
					vector9..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num28 = 40;
					int num29 = base.mod.ProjectileType("XenoShard2");
					float num30 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num30) * (double)num27 * -1.0), (float)(Math.Sin((double)num30) * (double)num27 * -1.0), num29, num28, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 70)
				{
					float num31 = 9f;
					Vector2 vector10;
					vector10..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num32 = 40;
					int num33 = base.mod.ProjectileType("XenoShard2");
					float num34 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num34) * (double)num31 * -1.0), (float)(Math.Sin((double)num34) * (double)num31 * -1.0), num33, num32, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer2 == 90)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 93)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 96)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 99)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 102)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 105)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 108)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 111)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer2 == 149)
				{
					for (int k = 0; k < 25; k++)
					{
						int num35 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num35].velocity *= 3f;
						Main.dust[num35].noGravity = true;
					}
				}
				if (this.fightTimer2 == 150 && Main.netMode != 1)
				{
					Vector2 vector11;
					vector11..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector11;
					base.npc.netUpdate = true;
				}
				if (this.fightTimer2 == 151)
				{
					for (int l = 0; l < 25; l++)
					{
						int num36 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num36].velocity *= 3f;
						Main.dust[num36].noGravity = true;
					}
					this.fightTimer2 = 0;
				}
			}
			if (base.npc.life <= 10000 && !this.phase3Done)
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
				if (this.fightTimer3 == 50)
				{
					float num37 = 11f;
					Vector2 vector12;
					vector12..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num38 = 40;
					int num39 = base.mod.ProjectileType("XenoShard2");
					float num40 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num40) * (double)num37 * -1.0), (float)(Math.Sin((double)num40) * (double)num37 * -1.0), num39, num38, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 55)
				{
					float num41 = 11f;
					Vector2 vector13;
					vector13..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num42 = 40;
					int num43 = base.mod.ProjectileType("XenoShard2");
					float num44 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num44) * (double)num41 * -1.0), (float)(Math.Sin((double)num44) * (double)num41 * -1.0), num43, num42, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 60)
				{
					float num45 = 11f;
					Vector2 vector14;
					vector14..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num46 = 40;
					int num47 = base.mod.ProjectileType("XenoShard2");
					float num48 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num48) * (double)num45 * -1.0), (float)(Math.Sin((double)num48) * (double)num45 * -1.0), num47, num46, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 65)
				{
					float num49 = 11f;
					Vector2 vector15;
					vector15..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num50 = 40;
					int num51 = base.mod.ProjectileType("XenoShard2");
					float num52 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num52) * (double)num49 * -1.0), (float)(Math.Sin((double)num52) * (double)num49 * -1.0), num51, num50, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 70)
				{
					float num53 = 11f;
					Vector2 vector16;
					vector16..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num54 = 40;
					int num55 = base.mod.ProjectileType("XenoShard2");
					float num56 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)num56) * (double)num53 * -1.0), (float)(Math.Sin((double)num56) * (double)num53 * -1.0), num55, num54, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 75)
				{
					float num57 = 11f;
					Vector2 vector17;
					vector17..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num58 = 40;
					int num59 = base.mod.ProjectileType("XenoShard2");
					float num60 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)num60) * (double)num57 * -1.0), (float)(Math.Sin((double)num60) * (double)num57 * -1.0), num59, num58, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 80)
				{
					float num61 = 11f;
					Vector2 vector18;
					vector18..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num62 = 40;
					int num63 = base.mod.ProjectileType("XenoShard2");
					float num64 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)num64) * (double)num61 * -1.0), (float)(Math.Sin((double)num64) * (double)num61 * -1.0), num63, num62, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 85)
				{
					float num65 = 11f;
					Vector2 vector19;
					vector19..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num66 = 40;
					int num67 = base.mod.ProjectileType("XenoShard2");
					float num68 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)num68) * (double)num65 * -1.0), (float)(Math.Sin((double)num68) * (double)num65 * -1.0), num67, num66, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 79)
				{
					for (int m = 0; m < 25; m++)
					{
						int num69 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num69].velocity *= 3f;
						Main.dust[num69].noGravity = true;
					}
				}
				if (this.fightTimer3 == 80 && Main.netMode != 1)
				{
					Vector2 vector20;
					vector20..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector20;
					base.npc.netUpdate = true;
				}
				if (this.fightTimer3 == 81)
				{
					for (int n = 0; n < 25; n++)
					{
						int num70 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num70].velocity *= 3f;
						Main.dust[num70].noGravity = true;
					}
				}
				if (this.fightTimer3 == 90)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 93)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 96)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 99)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 102)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 105)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 108)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 111)
				{
					Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
				}
				if (this.fightTimer3 == 149)
				{
					for (int num71 = 0; num71 < 25; num71++)
					{
						int num72 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num72].velocity *= 3f;
						Main.dust[num72].noGravity = true;
					}
				}
				if (this.fightTimer3 == 150 && Main.netMode != 1)
				{
					Vector2 vector21;
					vector21..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector21;
					base.npc.netUpdate = true;
				}
				if (this.fightTimer3 == 151)
				{
					for (int num73 = 0; num73 < 25; num73++)
					{
						int num74 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num74].velocity *= 3f;
						Main.dust[num74].noGravity = true;
					}
				}
				if (this.fightTimer3 == 250)
				{
					float num75 = 9f;
					Vector2 vector22;
					vector22..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num76 = 40;
					int num77 = base.mod.ProjectileType("XenoShard3");
					float num78 = (float)Math.Atan2((double)(vector22.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector22.X - (player.position.X + (float)player.width * 0.5f)));
					Projectile.NewProjectile(vector22.X, vector22.Y, (float)(Math.Cos((double)num78) * (double)num75 * -1.0), (float)(Math.Sin((double)num78) * (double)num75 * -1.0), num77, num76, 0f, 0, 0f, 0f);
				}
				if (this.fightTimer3 == 299)
				{
					for (int num79 = 0; num79 < 25; num79++)
					{
						int num80 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num80].velocity *= 3f;
						Main.dust[num80].noGravity = true;
					}
				}
				if (this.fightTimer3 == 300 && Main.netMode != 1)
				{
					Vector2 vector23;
					vector23..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector23;
					base.npc.netUpdate = true;
				}
				if (this.fightTimer3 == 301)
				{
					for (int num81 = 0; num81 < 25; num81++)
					{
						int num82 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num82].velocity *= 3f;
						Main.dust[num82].noGravity = true;
					}
					this.fightTimer3 = 0;
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

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.beginFight;
		}

		private Player player;

		private int spawnTimer;

		private bool beginFight;

		private int fightTimer;

		private int floatTimer;

		private bool phase2Done;

		private bool specialAttack1;

		private bool phase2;

		private int fightTimer2;

		private bool phase3;

		private bool phase3Done;

		private int fightTimer3;
	}
}
