using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	[AutoloadBossHead]
	public class Stage3Scientist2 : ModNPC
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
			base.npc.damage = 95;
			base.npc.defense = 25;
			base.npc.lifeMax = 22000;
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
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("XenoDust"), 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
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
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel2A"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FloppyDisk2"), 1, false, 0, false, false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.beginFight);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.beginFight = reader.ReadBool();
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
			Player P = Main.player[base.npc.target];
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
					Vector2 newPos = new Vector2(16f, -100f);
					base.npc.Center = base.npc.position + newPos;
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
			if (this.beginFight && base.npc.ai[2] == 0f)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 50f)
				{
					float Speed = 6f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 30;
					int type = base.mod.ProjectileType("XenoShard2");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector8.X - (P.position.X + (float)P.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
				}
				if (base.npc.ai[1] == 55f)
				{
					float Speed2 = 6f;
					Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage2 = 30;
					int type2 = base.mod.ProjectileType("XenoShard2");
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector9.X - (P.position.X + (float)P.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
				}
				if (base.npc.ai[1] == 60f)
				{
					float Speed3 = 6f;
					Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage3 = 30;
					int type3 = base.mod.ProjectileType("XenoShard2");
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector10.X - (P.position.X + (float)P.width * 0.5f)));
					int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
				}
				if (base.npc.ai[1] == 90f)
				{
					int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(0f, -8f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p].netUpdate = true;
				}
				if (base.npc.ai[1] == 93f)
				{
					int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(6f, -6f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p2].netUpdate = true;
				}
				if (base.npc.ai[1] == 96f)
				{
					int p3 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(8f, 0f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p3].netUpdate = true;
				}
				if (base.npc.ai[1] == 99f)
				{
					int p4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(6f, 6f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p4].netUpdate = true;
				}
				if (base.npc.ai[1] == 102f)
				{
					int p5 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(0f, 8f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p5].netUpdate = true;
				}
				if (base.npc.ai[1] == 105f)
				{
					int p6 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-6f, 6f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p6].netUpdate = true;
				}
				if (base.npc.ai[1] == 108f)
				{
					int p7 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-8f, 0f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p7].netUpdate = true;
				}
				if (base.npc.ai[1] == 111f)
				{
					int p8 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2(-6f, -6f), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p8].netUpdate = true;
				}
				if (base.npc.ai[1] == 199f)
				{
					for (int num57 = 0; num57 < 25; num57++)
					{
						int num58 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num58].velocity *= 3f;
						Main.dust[num58].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 200f && Main.netMode != 1)
				{
					Vector2 newPos2 = new Vector2((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + newPos2;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 201f)
				{
					for (int num59 = 0; num59 < 25; num59++)
					{
						int num60 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num60].velocity *= 3f;
						Main.dust[num60].noGravity = true;
					}
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.65f) && base.npc.ai[2] < 1f)
			{
				base.npc.ai[2] = 1f;
				base.npc.ai[1] = 0f;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[2] == 1f)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 50f)
				{
					float Speed4 = 9f;
					Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage4 = 30;
					int type4 = base.mod.ProjectileType("XenoShard2");
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector11.X - (P.position.X + (float)P.width * 0.5f)));
					int num61 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
					Main.projectile[num61].netUpdate = true;
				}
				if (base.npc.ai[1] == 55f)
				{
					float Speed5 = 9f;
					Vector2 vector12 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage5 = 30;
					int type5 = base.mod.ProjectileType("XenoShard2");
					float rotation5 = (float)Math.Atan2((double)(vector12.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector12.X - (P.position.X + (float)P.width * 0.5f)));
					int num62 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed5 * -1.0), (float)(Math.Sin((double)rotation5) * (double)Speed5 * -1.0), type5, damage5, 0f, 0, 0f, 0f);
					Main.projectile[num62].netUpdate = true;
				}
				if (base.npc.ai[1] == 60f)
				{
					float Speed6 = 9f;
					Vector2 vector13 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage6 = 30;
					int type6 = base.mod.ProjectileType("XenoShard2");
					float rotation6 = (float)Math.Atan2((double)(vector13.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector13.X - (P.position.X + (float)P.width * 0.5f)));
					int num63 = Projectile.NewProjectile(vector13.X, vector13.Y, (float)(Math.Cos((double)rotation6) * (double)Speed6 * -1.0), (float)(Math.Sin((double)rotation6) * (double)Speed6 * -1.0), type6, damage6, 0f, 0, 0f, 0f);
					Main.projectile[num63].netUpdate = true;
				}
				if (base.npc.ai[1] == 65f)
				{
					float Speed7 = 9f;
					Vector2 vector14 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage7 = 30;
					int type7 = base.mod.ProjectileType("XenoShard2");
					float rotation7 = (float)Math.Atan2((double)(vector14.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector14.X - (P.position.X + (float)P.width * 0.5f)));
					int num64 = Projectile.NewProjectile(vector14.X, vector14.Y, (float)(Math.Cos((double)rotation7) * (double)Speed7 * -1.0), (float)(Math.Sin((double)rotation7) * (double)Speed7 * -1.0), type7, damage7, 0f, 0, 0f, 0f);
					Main.projectile[num64].netUpdate = true;
				}
				if (base.npc.ai[1] == 70f)
				{
					float Speed8 = 9f;
					Vector2 vector15 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage8 = 30;
					int type8 = base.mod.ProjectileType("XenoShard2");
					float rotation8 = (float)Math.Atan2((double)(vector15.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector15.X - (P.position.X + (float)P.width * 0.5f)));
					int num65 = Projectile.NewProjectile(vector15.X, vector15.Y, (float)(Math.Cos((double)rotation8) * (double)Speed8 * -1.0), (float)(Math.Sin((double)rotation8) * (double)Speed8 * -1.0), type8, damage8, 0f, 0, 0f, 0f);
					Main.projectile[num65].netUpdate = true;
				}
				if (base.npc.ai[1] == 90f || base.npc.ai[1] == 93f || base.npc.ai[1] == 96f || base.npc.ai[1] == 99f || base.npc.ai[1] == 102f || base.npc.ai[1] == 105f || base.npc.ai[1] == 108f || base.npc.ai[1] == 111f)
				{
					int p9 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-1, 2), (float)(8 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p9].netUpdate = true;
				}
				if (base.npc.ai[1] == 149f)
				{
					for (int num66 = 0; num66 < 25; num66++)
					{
						int num67 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num67].velocity *= 3f;
						Main.dust[num67].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 150f && Main.netMode != 1)
				{
					Vector2 newPos3 = new Vector2((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + newPos3;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 151f)
				{
					for (int num68 = 0; num68 < 25; num68++)
					{
						int num69 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num69].velocity *= 3f;
						Main.dust[num69].noGravity = true;
					}
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.35f) && base.npc.ai[2] < 2f)
			{
				base.npc.ai[2] = 2f;
				base.npc.ai[1] = 0f;
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[2] == 2f)
			{
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 50f)
				{
					float Speed9 = 11f;
					Vector2 vector16 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage9 = 30;
					int type9 = base.mod.ProjectileType("XenoShard2");
					float rotation9 = (float)Math.Atan2((double)(vector16.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector16.X - (P.position.X + (float)P.width * 0.5f)));
					int num70 = Projectile.NewProjectile(vector16.X, vector16.Y, (float)(Math.Cos((double)rotation9) * (double)Speed9 * -1.0), (float)(Math.Sin((double)rotation9) * (double)Speed9 * -1.0), type9, damage9, 0f, 0, 0f, 0f);
					Main.projectile[num70].netUpdate = true;
				}
				if (base.npc.ai[1] == 55f)
				{
					float Speed10 = 11f;
					Vector2 vector17 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage10 = 30;
					int type10 = base.mod.ProjectileType("XenoShard2");
					float rotation10 = (float)Math.Atan2((double)(vector17.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector17.X - (P.position.X + (float)P.width * 0.5f)));
					int num71 = Projectile.NewProjectile(vector17.X, vector17.Y, (float)(Math.Cos((double)rotation10) * (double)Speed10 * -1.0), (float)(Math.Sin((double)rotation10) * (double)Speed10 * -1.0), type10, damage10, 0f, 0, 0f, 0f);
					Main.projectile[num71].netUpdate = true;
				}
				if (base.npc.ai[1] == 60f)
				{
					float Speed11 = 11f;
					Vector2 vector18 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage11 = 30;
					int type11 = base.mod.ProjectileType("XenoShard2");
					float rotation11 = (float)Math.Atan2((double)(vector18.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector18.X - (P.position.X + (float)P.width * 0.5f)));
					int num72 = Projectile.NewProjectile(vector18.X, vector18.Y, (float)(Math.Cos((double)rotation11) * (double)Speed11 * -1.0), (float)(Math.Sin((double)rotation11) * (double)Speed11 * -1.0), type11, damage11, 0f, 0, 0f, 0f);
					Main.projectile[num72].netUpdate = true;
				}
				if (base.npc.ai[1] == 65f)
				{
					float Speed12 = 11f;
					Vector2 vector19 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage12 = 30;
					int type12 = base.mod.ProjectileType("XenoShard2");
					float rotation12 = (float)Math.Atan2((double)(vector19.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector19.X - (P.position.X + (float)P.width * 0.5f)));
					int num73 = Projectile.NewProjectile(vector19.X, vector19.Y, (float)(Math.Cos((double)rotation12) * (double)Speed12 * -1.0), (float)(Math.Sin((double)rotation12) * (double)Speed12 * -1.0), type12, damage12, 0f, 0, 0f, 0f);
					Main.projectile[num73].netUpdate = true;
				}
				if (base.npc.ai[1] == 70f)
				{
					float Speed13 = 11f;
					Vector2 vector20 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage13 = 30;
					int type13 = base.mod.ProjectileType("XenoShard2");
					float rotation13 = (float)Math.Atan2((double)(vector20.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector20.X - (P.position.X + (float)P.width * 0.5f)));
					int num74 = Projectile.NewProjectile(vector20.X, vector20.Y, (float)(Math.Cos((double)rotation13) * (double)Speed13 * -1.0), (float)(Math.Sin((double)rotation13) * (double)Speed13 * -1.0), type13, damage13, 0f, 0, 0f, 0f);
					Main.projectile[num74].netUpdate = true;
				}
				if (base.npc.ai[1] == 75f)
				{
					float Speed14 = 11f;
					Vector2 vector21 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage14 = 30;
					int type14 = base.mod.ProjectileType("XenoShard2");
					float rotation14 = (float)Math.Atan2((double)(vector21.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector21.X - (P.position.X + (float)P.width * 0.5f)));
					int num75 = Projectile.NewProjectile(vector21.X, vector21.Y, (float)(Math.Cos((double)rotation14) * (double)Speed14 * -1.0), (float)(Math.Sin((double)rotation14) * (double)Speed14 * -1.0), type14, damage14, 0f, 0, 0f, 0f);
					Main.projectile[num75].netUpdate = true;
				}
				if (base.npc.ai[1] == 80f)
				{
					float Speed15 = 11f;
					Vector2 vector22 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage15 = 30;
					int type15 = base.mod.ProjectileType("XenoShard2");
					float rotation15 = (float)Math.Atan2((double)(vector22.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector22.X - (P.position.X + (float)P.width * 0.5f)));
					int num76 = Projectile.NewProjectile(vector22.X, vector22.Y, (float)(Math.Cos((double)rotation15) * (double)Speed15 * -1.0), (float)(Math.Sin((double)rotation15) * (double)Speed15 * -1.0), type15, damage15, 0f, 0, 0f, 0f);
					Main.projectile[num76].netUpdate = true;
				}
				if (base.npc.ai[1] == 85f)
				{
					float Speed16 = 11f;
					Vector2 vector23 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage16 = 30;
					int type16 = base.mod.ProjectileType("XenoShard2");
					float rotation16 = (float)Math.Atan2((double)(vector23.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector23.X - (P.position.X + (float)P.width * 0.5f)));
					int num77 = Projectile.NewProjectile(vector23.X, vector23.Y, (float)(Math.Cos((double)rotation16) * (double)Speed16 * -1.0), (float)(Math.Sin((double)rotation16) * (double)Speed16 * -1.0), type16, damage16, 0f, 0, 0f, 0f);
					Main.projectile[num77].netUpdate = true;
				}
				if (base.npc.ai[1] == 79f)
				{
					for (int num78 = 0; num78 < 25; num78++)
					{
						int num79 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num79].velocity *= 3f;
						Main.dust[num79].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 80f && Main.netMode != 1)
				{
					Vector2 newPos4 = new Vector2((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + newPos4;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 81f)
				{
					for (int num80 = 0; num80 < 25; num80++)
					{
						int num81 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num81].velocity *= 3f;
						Main.dust[num81].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 90f || base.npc.ai[1] == 93f || base.npc.ai[1] == 96f || base.npc.ai[1] == 99f || base.npc.ai[1] == 102f || base.npc.ai[1] == 105f || base.npc.ai[1] == 108f || base.npc.ai[1] == 111f)
				{
					int p10 = Projectile.NewProjectile(new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2)), new Vector2((float)Main.rand.Next(-2, 3), (float)(10 + Main.rand.Next(-2, 0))), base.mod.ProjectileType("XenoShard2"), 30, 3f, 255, 0f, 0f);
					Main.projectile[p10].netUpdate = true;
				}
				if (base.npc.ai[1] == 149f)
				{
					for (int num82 = 0; num82 < 25; num82++)
					{
						int num83 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num83].velocity *= 3f;
						Main.dust[num83].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 150f && Main.netMode != 1)
				{
					Vector2 newPos5 = new Vector2((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + newPos5;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] == 151f)
				{
					for (int num84 = 0; num84 < 25; num84++)
					{
						int num85 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num85].velocity *= 3f;
						Main.dust[num85].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 250f)
				{
					float Speed17 = 5f;
					Vector2 vector24 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage17 = 30;
					int type17 = base.mod.ProjectileType("XenoShard3");
					float rotation17 = (float)Math.Atan2((double)(vector24.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector24.X - (P.position.X + (float)P.width * 0.5f)));
					int num86 = Projectile.NewProjectile(vector24.X, vector24.Y, (float)(Math.Cos((double)rotation17) * (double)Speed17 * -1.0), (float)(Math.Sin((double)rotation17) * (double)Speed17 * -1.0), type17, damage17, 0f, 0, 0f, 0f);
					Main.projectile[num86].netUpdate = true;
				}
				if (base.npc.ai[1] == 299f)
				{
					for (int num87 = 0; num87 < 25; num87++)
					{
						int num88 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num88].velocity *= 3f;
						Main.dust[num88].noGravity = true;
					}
				}
				if (base.npc.ai[1] == 300f && Main.netMode != 1)
				{
					Vector2 newPos6 = new Vector2((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-270, -170));
					base.npc.Center = Main.player[base.npc.target].Center + newPos6;
					base.npc.netUpdate = true;
				}
				if (base.npc.ai[1] >= 301f)
				{
					for (int num89 = 0; num89 < 25; num89++)
					{
						int num90 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 74, 0f, 0f, 100, default(Color), 2.5f);
						Main.dust[num90].velocity *= 3f;
						Main.dust[num90].noGravity = true;
					}
					base.npc.ai[1] = 0f;
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
					return;
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
	}
}
