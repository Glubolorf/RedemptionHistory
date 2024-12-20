using System;
using System.IO;
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
			if (!RedeWorld.labAccess2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel2"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk2"), 1, false, 0, false, false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.beginFight);
				writer.Write(this.phase2Done);
				writer.Write(this.specialAttack1);
				writer.Write(this.phase2);
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
				this.beginFight = reader.ReadBool();
				this.phase2Done = reader.ReadBool();
				this.specialAttack1 = reader.ReadBool();
				this.phase2 = reader.ReadBool();
				this.phase3 = reader.ReadBool();
				this.phase3Done = reader.ReadBool();
			}
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
					vector..ctor(16f, -100f);
					base.npc.Center = base.npc.position + vector;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.ai[0] <= 120f)
			{
				base.npc.aiStyle = -1;
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[0] > 120f)
			{
				this.beginFight = true;
				base.npc.dontTakeDamage = false;
				base.npc.netUpdate = true;
			}
			if (this.beginFight)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 50f)
				{
					float num = 6f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num2 = 40;
					int num3 = base.mod.ProjectileType("XenoShard2");
					float num4 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num5 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
					Main.projectile[num5].netUpdate = true;
				}
				if (base.npc.ai[1] == 55f)
				{
					float num6 = 6f;
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num7 = 40;
					int num8 = base.mod.ProjectileType("XenoShard2");
					float num9 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num10 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num9) * (double)num6 * -1.0), (float)(Math.Sin((double)num9) * (double)num6 * -1.0), num8, num7, 0f, 0, 0f, 0f);
					Main.projectile[num10].netUpdate = true;
				}
				if (base.npc.ai[1] == 60f)
				{
					float num11 = 6f;
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num12 = 40;
					int num13 = base.mod.ProjectileType("XenoShard2");
					float num14 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					int num15 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num14) * (double)num11 * -1.0), (float)(Math.Sin((double)num14) * (double)num11 * -1.0), num13, num12, 0f, 0, 0f, 0f);
					Main.projectile[num15].netUpdate = true;
				}
				if (base.npc.ai[1] == 90f)
				{
					int num16 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(0f, -8f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num16].netUpdate = true;
				}
				if (base.npc.ai[1] == 93f)
				{
					int num17 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(6f, -6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num17].netUpdate = true;
				}
				if (base.npc.ai[1] == 96f)
				{
					int num18 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(8f, 0f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num18].netUpdate = true;
				}
				if (base.npc.ai[1] == 99f)
				{
					int num19 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(6f, 6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num19].netUpdate = true;
				}
				if (base.npc.ai[1] == 102f)
				{
					int num20 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(0f, 8f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num20].netUpdate = true;
				}
				if (base.npc.ai[1] == 105f)
				{
					int num21 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-6f, 6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num21].netUpdate = true;
				}
				if (base.npc.ai[1] == 108f)
				{
					int num22 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-8f, 0f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num22].netUpdate = true;
				}
				if (base.npc.ai[1] == 111f)
				{
					int num23 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-6f, -6f), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num23].netUpdate = true;
				}
				if (base.npc.ai[1] == 199f)
				{
					for (int i = 0; i < 25; i++)
					{
						int num24 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num24].velocity *= 3f;
						Main.dust[num24].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 200f && Main.netMode != 1)
				{
					Vector2 vector5;
					vector5..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector5;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 201f)
				{
					for (int j = 0; j < 25; j++)
					{
						int num25 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num25].velocity *= 3f;
						Main.dust[num25].noGravity = true;
					}
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= 17000 && !this.phase2Done)
			{
				this.phase2 = true;
				base.npc.netUpdate = true;
			}
			if (this.phase2)
			{
				base.npc.ai[1] = 0f;
				this.beginFight = false;
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 50f)
				{
					float num26 = 9f;
					Vector2 vector6;
					vector6..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num27 = 40;
					int num28 = base.mod.ProjectileType("XenoShard2");
					float num29 = (float)Math.Atan2((double)(vector6.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector6.X - (player.position.X + (float)player.width * 0.5f)));
					int num30 = Projectile.NewProjectile(vector6.X, vector6.Y, (float)(Math.Cos((double)num29) * (double)num26 * -1.0), (float)(Math.Sin((double)num29) * (double)num26 * -1.0), num28, num27, 0f, 0, 0f, 0f);
					Main.projectile[num30].netUpdate = true;
				}
				if (base.npc.ai[2] == 55f)
				{
					float num31 = 9f;
					Vector2 vector7;
					vector7..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num32 = 40;
					int num33 = base.mod.ProjectileType("XenoShard2");
					float num34 = (float)Math.Atan2((double)(vector7.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector7.X - (player.position.X + (float)player.width * 0.5f)));
					int num35 = Projectile.NewProjectile(vector7.X, vector7.Y, (float)(Math.Cos((double)num34) * (double)num31 * -1.0), (float)(Math.Sin((double)num34) * (double)num31 * -1.0), num33, num32, 0f, 0, 0f, 0f);
					Main.projectile[num35].netUpdate = true;
				}
				if (base.npc.ai[2] == 60f)
				{
					float num36 = 9f;
					Vector2 vector8;
					vector8..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num37 = 40;
					int num38 = base.mod.ProjectileType("XenoShard2");
					float num39 = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num40 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)num39) * (double)num36 * -1.0), (float)(Math.Sin((double)num39) * (double)num36 * -1.0), num38, num37, 0f, 0, 0f, 0f);
					Main.projectile[num40].netUpdate = true;
				}
				if (base.npc.ai[2] == 65f)
				{
					float num41 = 9f;
					Vector2 vector9;
					vector9..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num42 = 40;
					int num43 = base.mod.ProjectileType("XenoShard2");
					float num44 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
					int num45 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)num44) * (double)num41 * -1.0), (float)(Math.Sin((double)num44) * (double)num41 * -1.0), num43, num42, 0f, 0, 0f, 0f);
					Main.projectile[num45].netUpdate = true;
				}
				if (base.npc.ai[2] == 70f)
				{
					float num46 = 9f;
					Vector2 vector10;
					vector10..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num47 = 40;
					int num48 = base.mod.ProjectileType("XenoShard2");
					float num49 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
					int num50 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)num49) * (double)num46 * -1.0), (float)(Math.Sin((double)num49) * (double)num46 * -1.0), num48, num47, 0f, 0, 0f, 0f);
					Main.projectile[num50].netUpdate = true;
				}
				if (base.npc.ai[2] == 90f || base.npc.ai[2] == 93f || base.npc.ai[2] == 96f || base.npc.ai[2] == 99f || base.npc.ai[2] == 102f || base.npc.ai[2] == 105f || base.npc.ai[2] == 108f || base.npc.ai[2] == 111f)
				{
					int num51 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num51].netUpdate = true;
				}
				if (base.npc.ai[2] == 149f)
				{
					for (int k = 0; k < 25; k++)
					{
						int num52 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num52].velocity *= 3f;
						Main.dust[num52].noGravity = true;
					}
				}
				if (base.npc.ai[2] == 150f && Main.netMode != 1)
				{
					Vector2 vector11;
					vector11..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector11;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[2] >= 151f)
				{
					for (int l = 0; l < 25; l++)
					{
						int num53 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num53].velocity *= 3f;
						Main.dust[num53].noGravity = true;
					}
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life <= 10000 && !this.phase3Done)
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
				if (base.npc.ai[3] == 50f)
				{
					float num54 = 11f;
					Vector2 vector12;
					vector12..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num55 = 40;
					int num56 = base.mod.ProjectileType("XenoShard2");
					float num57 = (float)Math.Atan2((double)(vector12.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector12.X - (player.position.X + (float)player.width * 0.5f)));
					int num58 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)num57) * (double)num54 * -1.0), (float)(Math.Sin((double)num57) * (double)num54 * -1.0), num56, num55, 0f, 0, 0f, 0f);
					Main.projectile[num58].netUpdate = true;
				}
				if (base.npc.ai[3] == 55f)
				{
					float num59 = 11f;
					Vector2 vector13;
					vector13..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num60 = 40;
					int num61 = base.mod.ProjectileType("XenoShard2");
					float num62 = (float)Math.Atan2((double)(vector13.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector13.X - (player.position.X + (float)player.width * 0.5f)));
					int num63 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)num62) * (double)num59 * -1.0), (float)(Math.Sin((double)num62) * (double)num59 * -1.0), num61, num60, 0f, 0, 0f, 0f);
					Main.projectile[num63].netUpdate = true;
				}
				if (base.npc.ai[3] == 60f)
				{
					float num64 = 11f;
					Vector2 vector14;
					vector14..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num65 = 40;
					int num66 = base.mod.ProjectileType("XenoShard2");
					float num67 = (float)Math.Atan2((double)(vector14.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector14.X - (player.position.X + (float)player.width * 0.5f)));
					int num68 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)num67) * (double)num64 * -1.0), (float)(Math.Sin((double)num67) * (double)num64 * -1.0), num66, num65, 0f, 0, 0f, 0f);
					Main.projectile[num68].netUpdate = true;
				}
				if (base.npc.ai[3] == 65f)
				{
					float num69 = 11f;
					Vector2 vector15;
					vector15..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num70 = 40;
					int num71 = base.mod.ProjectileType("XenoShard2");
					float num72 = (float)Math.Atan2((double)(vector15.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector15.X - (player.position.X + (float)player.width * 0.5f)));
					int num73 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)num72) * (double)num69 * -1.0), (float)(Math.Sin((double)num72) * (double)num69 * -1.0), num71, num70, 0f, 0, 0f, 0f);
					Main.projectile[num73].netUpdate = true;
				}
				if (base.npc.ai[3] == 70f)
				{
					float num74 = 11f;
					Vector2 vector16;
					vector16..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num75 = 40;
					int num76 = base.mod.ProjectileType("XenoShard2");
					float num77 = (float)Math.Atan2((double)(vector16.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector16.X - (player.position.X + (float)player.width * 0.5f)));
					int num78 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)num77) * (double)num74 * -1.0), (float)(Math.Sin((double)num77) * (double)num74 * -1.0), num76, num75, 0f, 0, 0f, 0f);
					Main.projectile[num78].netUpdate = true;
				}
				if (base.npc.ai[3] == 75f)
				{
					float num79 = 11f;
					Vector2 vector17;
					vector17..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num80 = 40;
					int num81 = base.mod.ProjectileType("XenoShard2");
					float num82 = (float)Math.Atan2((double)(vector17.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector17.X - (player.position.X + (float)player.width * 0.5f)));
					int num83 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)num82) * (double)num79 * -1.0), (float)(Math.Sin((double)num82) * (double)num79 * -1.0), num81, num80, 0f, 0, 0f, 0f);
					Main.projectile[num83].netUpdate = true;
				}
				if (base.npc.ai[3] == 80f)
				{
					float num84 = 11f;
					Vector2 vector18;
					vector18..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num85 = 40;
					int num86 = base.mod.ProjectileType("XenoShard2");
					float num87 = (float)Math.Atan2((double)(vector18.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector18.X - (player.position.X + (float)player.width * 0.5f)));
					int num88 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)num87) * (double)num84 * -1.0), (float)(Math.Sin((double)num87) * (double)num84 * -1.0), num86, num85, 0f, 0, 0f, 0f);
					Main.projectile[num88].netUpdate = true;
				}
				if (base.npc.ai[3] == 85f)
				{
					float num89 = 11f;
					Vector2 vector19;
					vector19..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num90 = 40;
					int num91 = base.mod.ProjectileType("XenoShard2");
					float num92 = (float)Math.Atan2((double)(vector19.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector19.X - (player.position.X + (float)player.width * 0.5f)));
					int num93 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)num92) * (double)num89 * -1.0), (float)(Math.Sin((double)num92) * (double)num89 * -1.0), num91, num90, 0f, 0, 0f, 0f);
					Main.projectile[num93].netUpdate = true;
				}
				if (base.npc.ai[3] == 79f)
				{
					for (int m = 0; m < 25; m++)
					{
						int num94 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num94].velocity *= 3f;
						Main.dust[num94].noGravity = true;
					}
				}
				if (base.npc.ai[3] == 80f && Main.netMode != 1)
				{
					Vector2 vector20;
					vector20..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector20;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 81f)
				{
					for (int n = 0; n < 25; n++)
					{
						int num95 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num95].velocity *= 3f;
						Main.dust[num95].noGravity = true;
					}
				}
				if (base.npc.ai[3] == 90f || base.npc.ai[3] == 93f || base.npc.ai[3] == 96f || base.npc.ai[3] == 99f || base.npc.ai[3] == 102f || base.npc.ai[3] == 105f || base.npc.ai[3] == 108f || base.npc.ai[3] == 111f)
				{
					int num96 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 40, 3f, 255, 0f, 0f);
					Main.projectile[num96].netUpdate = true;
				}
				if (base.npc.ai[3] == 149f)
				{
					for (int num97 = 0; num97 < 25; num97++)
					{
						int num98 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num98].velocity *= 3f;
						Main.dust[num98].noGravity = true;
					}
				}
				if (base.npc.ai[3] == 150f && Main.netMode != 1)
				{
					Vector2 vector21;
					vector21..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector21;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] == 151f)
				{
					for (int num99 = 0; num99 < 25; num99++)
					{
						int num100 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num100].velocity *= 3f;
						Main.dust[num100].noGravity = true;
					}
				}
				if (base.npc.ai[3] == 250f)
				{
					float num101 = 9f;
					Vector2 vector22;
					vector22..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num102 = 40;
					int num103 = base.mod.ProjectileType("XenoShard3");
					float num104 = (float)Math.Atan2((double)(vector22.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector22.X - (player.position.X + (float)player.width * 0.5f)));
					int num105 = Projectile.NewProjectile(vector22.X, vector22.Y, (float)(Math.Cos((double)num104) * (double)num101 * -1.0), (float)(Math.Sin((double)num104) * (double)num101 * -1.0), num103, num102, 0f, 0, 0f, 0f);
					Main.projectile[num105].netUpdate = true;
				}
				if (base.npc.ai[3] == 299f)
				{
					for (int num106 = 0; num106 < 25; num106++)
					{
						int num107 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num107].velocity *= 3f;
						Main.dust[num107].noGravity = true;
					}
				}
				if (base.npc.ai[3] == 300f && Main.netMode != 1)
				{
					Vector2 vector23;
					vector23..ctor((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + vector23;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[3] >= 301f)
				{
					for (int num108 = 0; num108 < 25; num108++)
					{
						int num109 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num109].velocity *= 3f;
						Main.dust[num109].noGravity = true;
					}
					base.npc.ai[3] = 0f;
					base.npc.netUpdate = true;
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

		private bool beginFight;

		private bool phase2Done;

		private bool specialAttack1;

		private bool phase2;

		private bool phase3;

		private bool phase3Done;

		public float[] customAI = new float[1];
	}
}
