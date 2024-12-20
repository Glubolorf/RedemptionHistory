using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	[AutoloadBossHead]
	public class TbotMiniboss : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Protector Volt");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 38;
			base.npc.height = 70;
			base.npc.friendly = false;
			base.npc.damage = 130;
			base.npc.defense = 90;
			base.npc.lifeMax = 160000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = (float)Item.buyPrice(0, 5, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = false;
			base.npc.noTileCollide = false;
			base.npc.aiStyle = -1;
			base.npc.boss = true;
			base.npc.netAlways = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
			RedeWorld.downedVolt = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (!RedeWorld.labAccess5)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel5A"), 1, false, 0, false, false);
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.customAI[0]);
				writer.Write(this.landed);
				writer.Write(this.flying);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.customAI[0] = reader.ReadFloat();
				this.landed = reader.ReadBool();
				this.flying = reader.ReadBool();
			}
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			if (!this.stayLeft && !this.stayRight)
			{
				if (this.player.Center.X > base.npc.Center.X)
				{
					base.npc.spriteDirection = 1;
				}
				else
				{
					base.npc.spriteDirection = -1;
				}
			}
			else if (this.stayLeft)
			{
				base.npc.spriteDirection = 1;
			}
			else if (this.stayRight)
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 216)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			Vector2 PosDone = new Vector2(this.Origin.X + 1376f, this.Origin.Y + 1904f);
			Vector2 PosDone2 = new Vector2(this.Origin.X + 816f, this.Origin.Y + 1904f);
			if (base.npc.ai[1] == 0f)
			{
				float[] ai = base.npc.ai;
				int num58 = 2;
				float num59 = ai[num58] + 1f;
				ai[num58] = num59;
				if (num59 >= 80f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[1] += 1f;
				}
			}
			if (base.npc.ai[1] == 1f)
			{
				base.npc.ai[1] += 1f;
				this.MoveVector2 = this.PosPick();
				base.npc.noTileCollide = true;
				base.npc.noGravity = true;
				base.npc.netUpdate = true;
			}
			if (base.npc.immortal)
			{
				if (this.customAI[0] == 0f)
				{
					if (Vector2.Distance(base.npc.Center, PosDone) < 10f)
					{
						base.npc.velocity *= 0f;
						this.landed = false;
						base.npc.noTileCollide = false;
						base.npc.noGravity = false;
						base.npc.netUpdate = true;
						this.customAI[0] += 1f;
					}
					else
					{
						this.MoveToVector2(PosDone);
						base.npc.noGravity = true;
						base.npc.noTileCollide = true;
						base.npc.ai[2] = 0f;
					}
				}
				else if (this.customAI[0] == 1f)
				{
					base.npc.ai[2] += 1f;
					if (RedeWorld.downedVolt)
					{
						this.customAI[0] += 1f;
						if (base.npc.ai[2] == 10f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Enough.", true, false);
						}
						if (base.npc.ai[2] >= 30f)
						{
							this.customAI[0] += 1f;
						}
					}
					else
					{
						if (base.npc.ai[2] == 10f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Wait.", true, false);
						}
						if (base.npc.ai[2] == 100f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "I am recieving a transmission.", true, false);
						}
						if (base.npc.ai[2] == 290f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "...", true, false);
						}
						if (base.npc.ai[2] == 450f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "This appears to have been a misunderstanding, you're allowed through.", true, false);
						}
						if (base.npc.ai[2] == 600f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "My apologies. Move along.", true, false);
						}
						if (base.npc.ai[2] >= 720f)
						{
							this.customAI[0] += 1f;
						}
					}
				}
				else if (this.customAI[0] == 2f)
				{
					if (Vector2.Distance(base.npc.Center, PosDone2) < 10f)
					{
						base.npc.velocity *= 0f;
						this.landed = false;
						base.npc.noTileCollide = false;
						base.npc.noGravity = false;
						base.npc.netUpdate = true;
						this.customAI[0] += 1f;
					}
					else
					{
						this.MoveToVector2(PosDone2);
						base.npc.noGravity = true;
						base.npc.noTileCollide = true;
						base.npc.ai[2] = 0f;
					}
				}
				else if (this.customAI[0] >= 3f)
				{
					if (!RedeWorld.labAccess5)
					{
						Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel5A"), 1, false, 0, false, false);
					}
					RedeWorld.downedVolt = true;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					}
					NPC npc2 = base.npc;
					npc2.position.Y = npc2.position.Y - 30f;
					base.npc.SetDefaults(base.mod.NPCType("ProtectorVoltNPC"), -1f);
				}
			}
			else
			{
				if (base.npc.ai[1] == 2f)
				{
					if (Vector2.Distance(base.npc.Center, this.MoveVector2) < 10f)
					{
						base.npc.velocity *= 0f;
						this.landed = false;
						base.npc.ai[1] += 1f;
						base.npc.netUpdate = true;
					}
					else
					{
						this.MoveToVector2(this.MoveVector2);
					}
				}
				if (base.npc.ai[1] == 3f)
				{
					if (this.flying)
					{
						base.npc.ai[1] += 1f;
						base.npc.netUpdate = true;
						base.npc.ai[3] = (float)Main.rand.Next(6);
					}
					else
					{
						base.npc.noTileCollide = false;
						base.npc.noGravity = false;
						base.npc.ai[1] += 1f;
						base.npc.netUpdate = true;
						base.npc.ai[3] = (float)Main.rand.Next(6);
					}
				}
				if (base.npc.ai[1] == 4f)
				{
					switch ((int)base.npc.ai[3])
					{
					case 0:
						break;
					case 1:
						goto IL_AE2;
					case 2:
					{
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] <= 60f)
						{
							int dustIndex = Dust.NewDust(base.npc.Center, 2, 2, 226, 0f, 0f, 100, default(Color), 1f);
							Main.dust[dustIndex].velocity *= 2.8f;
							Main.dust[dustIndex].noGravity = true;
						}
						if (base.npc.ai[2] % 5f == 0f && base.npc.ai[2] > 90f)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed = 10f;
							Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage = base.npc.damage / 4;
							int type = base.mod.ProjectileType("ElectricZapPro1");
							float rotation = (float)Math.Atan2((double)(vector8.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector8.X - (this.player.position.X + (float)this.player.width * 0.5f)));
							int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num54].netUpdate = true;
							Main.projectile[num54].tileCollide = false;
							Main.projectile[num54].timeLeft = 200;
						}
						float[] ai2 = base.npc.ai;
						int num60 = 2;
						float num59 = ai2[num60] + 1f;
						ai2[num60] = num59;
						if (num59 > 200f)
						{
							base.npc.ai[1] = 1f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							goto IL_16B8;
						}
						goto IL_16B8;
					}
					case 3:
						base.npc.ai[2] += 1f;
						this.customGunRot = true;
						if (base.npc.spriteDirection == 1)
						{
							this.gunRot = 5.7596f;
						}
						else
						{
							this.gunRot = 3.6652f;
						}
						if (base.npc.ai[2] == 60f)
						{
							Main.PlaySound(SoundID.Item61, (int)base.npc.position.X, (int)base.npc.position.Y);
							int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)((base.npc.spriteDirection == 1) ? 4 : -4), -3f, base.mod.ProjectileType("ElectricOrbPro1"), base.npc.damage / 4, 3f, 255, 0f, 0f);
							p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)((base.npc.spriteDirection == 1) ? 6 : -6), -4f, base.mod.ProjectileType("ElectricOrbPro1"), base.npc.damage / 4, 3f, 255, 0f, 0f);
							p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)((base.npc.spriteDirection == 1) ? 8 : -8), -5f, base.mod.ProjectileType("ElectricOrbPro1"), base.npc.damage / 4, 3f, 255, 0f, 0f);
							p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)((base.npc.spriteDirection == 1) ? 10 : -10), -6f, base.mod.ProjectileType("ElectricOrbPro1"), base.npc.damage / 4, 3f, 255, 0f, 0f);
							p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)((base.npc.spriteDirection == 1) ? 12 : -12), -7f, base.mod.ProjectileType("ElectricOrbPro1"), base.npc.damage / 4, 3f, 255, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						if (base.npc.ai[2] > 170f)
						{
							base.npc.ai[1] = 1f;
							base.npc.ai[2] = 0f;
							this.customGunRot = false;
							base.npc.netUpdate = true;
							goto IL_16B8;
						}
						goto IL_16B8;
					case 4:
						base.npc.ai[2] += 1f;
						this.customGunRot = true;
						if (base.npc.spriteDirection == 1)
						{
							this.gunRot = 0f;
						}
						else
						{
							this.gunRot = 3.1416f;
						}
						if (base.npc.ai[2] <= 40f)
						{
							for (int j = 0; j < 2; j++)
							{
								int dustIndex2 = Dust.NewDust(base.npc.Center, 2, 2, 226, 0f, 0f, 100, default(Color), 1.2f);
								Main.dust[dustIndex2].velocity *= 2.8f;
								Main.dust[dustIndex2].noGravity = true;
							}
						}
						if (base.npc.ai[2] == 60f)
						{
							Main.PlaySound(SoundID.Item73, (int)base.npc.position.X, (int)base.npc.position.Y);
							int p2 = Projectile.NewProjectile((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 78f) : (base.npc.Center.X - 78f), base.npc.Center.Y + 10f, 0f, 0f, base.mod.ProjectileType("TeslaBeamSpawn"), base.npc.damage / 4, 1f, Main.myPlayer, (float)((base.npc.spriteDirection == 1) ? 0 : 1), 0f);
							Main.projectile[p2].netUpdate = true;
							if (base.npc.spriteDirection == 1)
							{
								this.stayLeft = true;
							}
							else
							{
								this.stayRight = true;
							}
						}
						if (base.npc.ai[2] >= 180f)
						{
							base.npc.ai[1] = 1f;
							base.npc.ai[2] = 0f;
							this.stayLeft = false;
							this.stayRight = false;
							this.customGunRot = false;
							base.npc.netUpdate = true;
							goto IL_16B8;
						}
						goto IL_16B8;
					case 5:
						if (this.flying)
						{
							goto IL_AE2;
						}
						base.npc.ai[2] += 1f;
						this.customGunRot = true;
						if (base.npc.spriteDirection == 1)
						{
							this.gunRot = 0f;
						}
						else
						{
							this.gunRot = 3.1416f;
						}
						if (base.npc.ai[2] == 2f)
						{
							base.npc.velocity.Y = -10f;
							this.landed = false;
						}
						if (base.npc.ai[2] % 10f == 0f)
						{
							Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
							Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage2 = base.npc.damage / 4;
							int type2 = base.mod.ProjectileType("ElectricZapPro1");
							int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (base.npc.spriteDirection == 1) ? 14f : -14f, 0f, type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num55].netUpdate = true;
							Main.projectile[num55].tileCollide = false;
							Main.projectile[num55].timeLeft = 200;
						}
						if (base.npc.ai[2] > 80f && this.landed)
						{
							base.npc.ai[1] = 1f;
							base.npc.ai[2] = 0f;
							this.customGunRot = false;
							base.npc.netUpdate = true;
							goto IL_16B8;
						}
						goto IL_16B8;
					default:
						base.npc.ai[2] = 0f;
						break;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 20f == 0f)
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed2 = 15f;
						Vector2 vector10 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage3 = base.npc.damage / 4;
						int type3 = base.mod.ProjectileType("ElectricZapPro1");
						float rotation2 = (float)Math.Atan2((double)(vector10.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector10.X - (this.player.position.X + (float)this.player.width * 0.5f)));
						int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
						Main.projectile[num56].netUpdate = true;
						Main.projectile[num56].tileCollide = false;
						Main.projectile[num56].timeLeft = 200;
					}
					if (base.npc.ai[2] >= 80f)
					{
						base.npc.ai[1] = 1f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						goto IL_16B8;
					}
					goto IL_16B8;
					IL_AE2:
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] % 10f == 0f)
					{
						Main.PlaySound(SoundID.Item91, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed3 = 14f;
						Vector2 vector11 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage4 = base.npc.damage / 4;
						int type4 = base.mod.ProjectileType("ElectricZapPro1");
						float rotation3 = (float)Math.Atan2((double)(vector11.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector11.X - (this.player.position.X + (float)this.player.width * 0.5f)));
						int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
						Main.projectile[num57].netUpdate = true;
						Main.projectile[num57].tileCollide = false;
						Main.projectile[num57].timeLeft = 200;
					}
					if (base.npc.ai[2] > 60f)
					{
						base.npc.ai[1] = 1f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
				}
			}
			IL_16B8:
			if (base.npc.collideY && base.npc.velocity.Y > 0f && !this.flying && !this.landed)
			{
				for (int k = 0; k < 40; k++)
				{
					Dust.NewDust(base.npc.BottomLeft, Main.rand.Next(base.npc.width), 1, 31, 0f, 0f, 0, default(Color), 1f);
				}
				this.landed = true;
			}
			if (base.npc.velocity.Y != 0f || this.flying)
			{
				if (base.npc.velocity.X == 0f && base.npc.velocity.Y == 0f)
				{
					if (Main.rand.Next(3) == 0)
					{
						int dustID = Dust.NewDust(new Vector2(base.npc.position.X + 12f, base.npc.position.Y + 68f), 2, 2, 6, 0f, 3f, 100, Color.White, 2f);
						Dust dust = Main.dust[dustID];
						dust.velocity.X = dust.velocity.X * 0f;
						Main.dust[dustID].noGravity = false;
						int dustID2 = Dust.NewDust(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 68f), 2, 2, 6, 0f, 3f, 100, Color.White, 2f);
						Dust dust2 = Main.dust[dustID];
						dust2.velocity.X = dust2.velocity.X * 0f;
						Main.dust[dustID2].noGravity = false;
					}
				}
				else if (Main.rand.Next(3) == 0)
				{
					int dustID3 = Dust.NewDust(new Vector2(base.npc.position.X + 12f, base.npc.position.Y + 68f), 2, 2, 6, 0f, 3f, 100, Color.White, 1.2f);
					Dust dust3 = Main.dust[dustID3];
					dust3.velocity.X = dust3.velocity.X * 0f;
					Main.dust[dustID3].noGravity = false;
					int dustID4 = Dust.NewDust(new Vector2(base.npc.position.X + 28f, base.npc.position.Y + 68f), 2, 2, 6, 0f, 3f, 100, Color.White, 1.2f);
					Dust dust4 = Main.dust[dustID3];
					dust4.velocity.X = dust4.velocity.X * 0f;
					Main.dust[dustID4].noGravity = false;
				}
			}
			if (base.npc.life <= (int)((float)base.npc.lifeMax * 0.01f) && Main.netMode == 0)
			{
				base.npc.immortal = true;
				base.npc.dontTakeDamage = true;
			}
		}

		public override bool CheckDead()
		{
			base.npc.life = 1;
			return false;
		}

		public Vector2 PosPick()
		{
			int PosChoice = Main.rand.Next(10);
			Vector2 Pos = new Vector2(this.Origin.X + 832f, this.Origin.Y + 1904f);
			Vector2 Pos2 = new Vector2(this.Origin.X + 1936f, this.Origin.Y + 1904f);
			Vector2 Pos3 = new Vector2(this.Origin.X + 1376f, this.Origin.Y + 1904f);
			Vector2 Pos4 = new Vector2(this.Origin.X + 752f, this.Origin.Y + 1648f);
			Vector2 Pos5 = new Vector2(this.Origin.X + 2016f, this.Origin.Y + 1648f);
			Vector2 Pos6 = new Vector2(this.Origin.X + 1072f, this.Origin.Y + 1648f);
			Vector2 Pos7 = new Vector2(this.Origin.X + 1664f, this.Origin.Y + 1648f);
			Vector2 APos = new Vector2(this.Origin.X + 1088f, this.Origin.Y + 1824f);
			Vector2 APos2 = new Vector2(this.Origin.X + 1680f, this.Origin.Y + 1824f);
			Vector2 APos3 = new Vector2(this.Origin.X + 1376f, this.Origin.Y + 1616f);
			if (PosChoice == 0)
			{
				this.flying = false;
				return Pos;
			}
			if (PosChoice == 1)
			{
				this.flying = false;
				return Pos2;
			}
			if (PosChoice == 2)
			{
				this.flying = false;
				return Pos3;
			}
			if (PosChoice == 3)
			{
				this.flying = false;
				return Pos4;
			}
			if (PosChoice == 4)
			{
				this.flying = false;
				return Pos5;
			}
			if (PosChoice == 5)
			{
				this.flying = false;
				return Pos6;
			}
			if (PosChoice == 6)
			{
				this.flying = false;
				return Pos7;
			}
			if (PosChoice == 7)
			{
				this.flying = true;
				return APos;
			}
			if (PosChoice == 8)
			{
				this.flying = true;
				return APos2;
			}
			if (PosChoice == 9)
			{
				this.flying = true;
				return APos3;
			}
			return Pos;
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 12f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/LabNPCs/New/TbotMinibossHop");
			Texture2D gunAni = base.mod.GetTexture("NPCs/LabNPCs/New/TbotMinibossGun");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.velocity.Y != 0f || this.flying)
			{
				new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = num214 * this.singleFrame;
				new Vector2((float)hopAni.Width * 0.5f, (float)hopAni.Height * 0.5f);
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(hopAni, this.oldPos[i] - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor * (0.5f * alpha), this.oldrot[i], new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			if (base.npc.velocity.Y == 0f && !this.flying)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.velocity.Y != 0f || this.flying)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = hopAni.Height / 1;
				int y7 = num215 * this.singleFrame;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, hopAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			Vector2 drawCenterG = new Vector2(base.npc.Center.X, base.npc.Center.Y + 6f);
			int numG = gunAni.Height / 4;
			int yG = numG * this.gunFrame;
			spriteBatch.Draw(gunAni, drawCenterG - Main.screenPosition, new Rectangle?(new Rectangle(0, yG, gunAni.Width, numG)), drawColor, this.customGunRot ? this.gunRot : Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)gunAni.Width / 2f, (float)numG / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
			return false;
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

		public Vector2 Origin = new Vector2((float)((int)((float)Main.maxTilesX * 0.55f)), (float)((int)((float)Main.maxTilesY * 0.65f))) * 16f;

		private Player player;

		private Vector2[] oldPos = new Vector2[3];

		private float[] oldrot = new float[3];

		private bool landed;

		private int singleFrame;

		private int gunFrame;

		public float[] customAI = new float[1];

		public Vector2 MoveVector2;

		private bool flying;

		private bool customGunRot;

		private float gunRot;

		private bool stayLeft;

		private bool stayRight;
	}
}
