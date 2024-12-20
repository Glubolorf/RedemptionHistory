using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.SeedOfInfection
{
	[AutoloadBossHead]
	public class SoI : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Seed of Infection");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.width = 76;
			base.npc.height = 76;
			base.npc.friendly = false;
			base.npc.damage = 36;
			base.npc.defense = 10;
			base.npc.lifeMax = 6500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.boss = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.alpha = 0;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			this.bossBag = base.mod.ItemType("XenomiteCrystalBag");
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 188;
			if (!RedeWorld.downedXenomiteCrystal)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> You've awoken the infection now. But don't worry, I'm sure we can handle it!", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedXenomiteCrystal = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y), new Vector2(0f, 0f), base.mod.ProjectileType("SoIDeath"), 0, 0f, Main.myPlayer, 0f, 0f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("SeedOfInfectionTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteGlaive"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectedMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteYoyo"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenoCanister"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteShard"), Main.rand.Next(12, 22), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			if (!player.active || player.dead)
			{
				base.npc.TargetClosest(false);
				player = Main.player[base.npc.target];
				if (!player.active || player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 10.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 116;
				if (base.npc.frame.Y == 464 && Main.rand.Next(4) != 0)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
				if (base.npc.frame.Y > 580)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[3] == 1f)
			{
				this.frameCounters++;
				if (this.frameCounters > 5)
				{
					this.freakOutFrame++;
					this.frameCounters = 0;
				}
				if (this.freakOutFrame >= 6)
				{
					this.freakOutFrame = 0;
				}
			}
			Vector2 ChargePos = new Vector2(player.Center.X, player.Center.Y);
			Vector2 HighPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 240f) : (player.Center.X + 240f), player.Center.Y - 50f);
			new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 240f) : (player.Center.X + 240f), player.Center.Y + 50f);
			new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 140f) : (player.Center.X + 140f), player.Center.Y);
			Vector2 FarPos = new Vector2((player.Center.X > base.npc.Center.X) ? (player.Center.X - 320f) : (player.Center.X + 320f), player.Center.Y - 25f);
			Vector2 TopPos = new Vector2(player.Center.X, player.Center.Y - 120f);
			if (base.npc.ai[0] == 0f)
			{
				this.frameCounters = 0;
				base.npc.ai[3] = 0f;
				base.npc.ai[0] = 1f;
				base.npc.ai[2] = 0f;
				base.npc.ai[1] = (float)Main.rand.Next(8);
				base.npc.netUpdate = true;
				return;
			}
			if (base.npc.ai[0] == 1f)
			{
				switch ((int)base.npc.ai[1])
				{
				case 0:
					this.chargeSpeed = 7f;
					base.npc.rotation += 0.09f;
					this.MoveToVector2(ChargePos);
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] >= 200f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 1:
					this.chargeSpeed = 5f;
					base.npc.rotation += 0.09f;
					this.MoveToVector2(HighPos);
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 80f)
					{
						float Speed = 8f;
						Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage = 12;
						int type = base.mod.ProjectileType("ShardShot1");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + -1f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + -1f, type, damage, 0f, 0, 0f, 0f);
						int num56 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + 1f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + 1f, type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
						Main.projectile[num55].netUpdate = true;
						Main.projectile[num56].netUpdate = true;
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							int num57 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + -2f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + -1f, type, damage, 0f, 0, 0f, 0f);
							int num58 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + 2f, (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + 1f, type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num57].netUpdate = true;
							Main.projectile[num58].netUpdate = true;
						}
					}
					if (base.npc.ai[2] >= 120f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 2:
					this.chargeSpeed = 8f;
					base.npc.rotation += 0.09f;
					if (base.npc.ai[2] < 70f)
					{
						this.MoveToVector2(TopPos);
					}
					else
					{
						base.npc.velocity *= 0f;
					}
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 90f)
					{
						if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
						{
							for (int i = 0; i < 12; i++)
							{
								int p = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("ShardShot2"), 12, 3f, 255, 0f, 0f);
								Main.projectile[p].netUpdate = true;
							}
						}
						else
						{
							for (int j = 0; j < 8; j++)
							{
								int p2 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), base.mod.ProjectileType("ShardShot2"), 12, 3f, 255, 0f, 0f);
								Main.projectile[p2].netUpdate = true;
							}
						}
					}
					if (base.npc.ai[2] >= 150f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 3:
					if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f))
					{
						base.npc.ai[1] = (float)Main.rand.Next(8);
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					this.chargeSpeed = 9f;
					base.npc.rotation += 0.09f;
					this.MoveToVector2(FarPos);
					base.npc.ai[2] += 1f;
					if (base.npc.ai[2] == 100f || base.npc.ai[2] == 150f || base.npc.ai[2] == 200f)
					{
						float Speed2 = 12f;
						Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage2 = 13;
						int type2 = base.mod.ProjectileType("ToxicSludge1");
						float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
						int num59 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), type2, damage2, 0f, 0, 0f, 0f);
						int num60 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), type2, damage2, 0f, 0, 0f, 0f);
						Main.projectile[num59].netUpdate = true;
						Main.projectile[num60].netUpdate = true;
						if (Main.rand.Next(2) == 0)
						{
							int num61 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num61].netUpdate = true;
						}
					}
					if (Main.expertMode && base.npc.ai[1] == 250f)
					{
						float Speed3 = 10f;
						Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage3 = 13;
						int type3 = base.mod.ProjectileType("ToxicSludge1");
						float rotation3 = (float)Math.Atan2((double)(vector10.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector10.X - (player.position.X + (float)player.width * 0.5f)));
						int num62 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), type3, damage3, 0f, 0, 0f, 0f);
						int num63 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), type3, damage3, 0f, 0, 0f, 0f);
						Main.projectile[num62].netUpdate = true;
						Main.projectile[num63].netUpdate = true;
						if (Main.rand.Next(2) == 0)
						{
							int num64 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-1, 1), type3, damage3, 0f, 0, 0f, 0f);
							Main.projectile[num64].netUpdate = true;
						}
					}
					if (base.npc.ai[2] >= 300f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 4:
					if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f))
					{
						base.npc.ai[1] = (float)Main.rand.Next(8);
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					this.chargeSpeed = 6f;
					base.npc.rotation += 0.09f;
					this.MoveToVector2(ChargePos);
					base.npc.ai[2] += 1f;
					if ((base.npc.ai[1] >= 20f || base.npc.ai[1] <= 240f) && Main.rand.Next(10) == 0)
					{
						int p3 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-6 + Main.rand.Next(0, 11)), (float)(-6 + Main.rand.Next(0, 11)), base.mod.ProjectileType("XenomiteShot1"), 10, 3f, 255, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
					}
					if (base.npc.ai[2] >= 300f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 5:
					if (base.npc.ai[2] == 0f)
					{
						if (NPC.CountNPCS(base.mod.NPCType("SeedGrowth")) <= 2)
						{
							base.npc.ai[2] = 1f;
							return;
						}
						base.npc.ai[1] = (float)Main.rand.Next(8);
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					else
					{
						base.npc.rotation += 0.12f;
						base.npc.velocity *= 0f;
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 30f || base.npc.ai[2] == 60f || base.npc.ai[2] == 90f)
						{
							Main.PlaySound(SoundID.NPCDeath13, (int)base.npc.position.X, (int)base.npc.position.Y);
							int Minion = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("SeedGrowth"), 0, 0f, 0f, 0f, 0f, 255);
							Main.npc[Minion].netUpdate = true;
						}
						if (base.npc.ai[2] >= 130f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
					}
					break;
				case 6:
					if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.25f))
					{
						base.npc.ai[1] = (float)Main.rand.Next(8);
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					base.npc.ai[3] = 1f;
					base.npc.rotation += this.chargeSpeed / 40f;
					base.npc.ai[2] += 1f;
					base.npc.velocity *= 0f;
					if ((base.npc.ai[2] >= 30f || base.npc.ai[2] <= 300f) && Main.rand.Next(10) == 0)
					{
						int p4 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 15)), (float)(-14 + Main.rand.Next(0, 27)), base.mod.ProjectileType("ToxicSludge1"), 11, 3f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
					}
					if (base.npc.ai[2] < 160f)
					{
						this.chargeSpeed += 0.04f;
					}
					else
					{
						this.chargeSpeed -= 0.04f;
					}
					if (base.npc.ai[2] >= 320f)
					{
						base.npc.ai[3] = 0f;
						base.npc.ai[0] = 0f;
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
						return;
					}
					break;
				case 7:
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
					{
						this.chargeSpeed = 9f;
						base.npc.rotation += 0.09f;
						if (base.npc.ai[2] < 70f)
						{
							this.MoveToVector2(TopPos);
						}
						else
						{
							base.npc.velocity *= 0f;
						}
						base.npc.ai[2] += 1f;
						if (base.npc.ai[2] == 70f)
						{
							Main.PlaySound(SoundID.Item103, base.npc.position);
							int p5 = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, 0f, 10f, base.mod.ProjectileType("SeedLaser"), 13, 3f, 255, 0f, 0f);
							Main.projectile[p5].netUpdate = true;
						}
						if (base.npc.ai[2] >= 100f)
						{
							base.npc.ai[3] = 0f;
							base.npc.ai[0] = 0f;
							base.npc.ai[2] = 0f;
							base.npc.netUpdate = true;
							return;
						}
					}
					else
					{
						base.npc.ai[1] = (float)Main.rand.Next(8);
						base.npc.ai[2] = 0f;
						base.npc.netUpdate = true;
					}
					break;
				default:
					return;
				}
			}
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = this.chargeSpeed;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (base.npc.ai[1] != 0f)
			{
				if (length < 100f)
				{
					moveSpeed *= 0.5f;
				}
				if (length < 50f)
				{
					moveSpeed *= 0.5f;
				}
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.noKnockback = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D freakOutAni = base.mod.GetTexture("NPCs/Bosses/SeedOfInfection/SoI_FreakingOut");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.ai[3] == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			if (base.npc.ai[3] == 1f)
			{
				int num214 = freakOutAni.Height / 6;
				int y6 = num214 * this.freakOutFrame;
				Main.spriteBatch.Draw(freakOutAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, freakOutAni.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)freakOutAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private Player player;

		public int freakOutFrame;

		public int frameCounters;

		public float chargeSpeed;
	}
}
