using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	[AutoloadBossHead]
	public class EaglecrestGolemPZ : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Golem");
			Main.npcFrameCount[base.npc.type] = 20;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 320000;
			base.npc.damage = 140;
			base.npc.defense = 40;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 8, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 80;
			base.npc.height = 80;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			if (RedeConfigClient.Instance.AntiAntti)
			{
				this.music = 5;
				return;
			}
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossForest1");
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 3544;
			RedeWorld.downedEaglecrestGolemPZ = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("StonePuppet"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("EaglecrestGlove"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientPowerStave"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientPowerCore"), Main.rand.Next(9, 18), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			this.Target();
			this.DespawnHandler();
			Player player = Main.player[base.npc.target];
			for (int i = this.oldPos.Length - 1; i > 0; i--)
			{
				this.oldPos[i] = this.oldPos[i - 1];
				this.oldrot[i] = this.oldrot[i - 1];
			}
			this.oldPos[0] = base.npc.Center;
			this.oldrot[0] = base.npc.rotation;
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.velocity.Y == 0f && !this.roll)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 84;
					if (base.npc.frame.Y > 924)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.slash)
				{
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f))
					{
						this.slashCounter++;
						if (this.slashCounter > 3)
						{
							this.slashFrame++;
							this.slashCounter = 0;
						}
						if (this.slashFrame >= 9)
						{
							this.slashFrame = 0;
						}
					}
					else
					{
						this.slashCounter++;
						if (this.slashCounter > 4)
						{
							this.slashFrame++;
							this.slashCounter = 0;
						}
						if (this.slashFrame >= 9)
						{
							this.slashFrame = 0;
						}
					}
				}
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 500f && Main.rand.Next(100) == 0 && !this.slash && !this.roll)
				{
					this.slash = true;
					base.npc.netUpdate = true;
				}
				if (this.slash)
				{
					if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f))
					{
						base.npc.ai[1] += 1f;
						base.npc.velocity.X = 0f;
						if (base.npc.ai[1] == 1f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityTrash, "Slash!", true, true);
						}
						if (base.npc.ai[1] == 21f)
						{
							Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed = 10f;
							Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage = 55;
							int type = base.mod.ProjectileType("GolemSlashPro2");
							float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
							int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num54].netUpdate = true;
						}
						if (base.npc.ai[1] >= 27f)
						{
							this.slash = false;
							base.npc.ai[1] = 0f;
							this.slashCounter = 0;
							this.slashFrame = 0;
							base.npc.netUpdate = true;
						}
					}
					else
					{
						base.npc.ai[1] += 1f;
						base.npc.velocity.X = 0f;
						if (base.npc.ai[1] == 1f && !RedeConfigClient.Instance.NoCombatText)
						{
							CombatText.NewText(base.npc.getRect(), Colors.RarityTrash, "Slash!", true, true);
						}
						if (base.npc.ai[1] == 28f)
						{
							Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed2 = 9f;
							Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
							int damage2 = 55;
							int type2 = base.mod.ProjectileType("GolemSlashPro2");
							float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num55].netUpdate = true;
						}
						if (base.npc.ai[1] >= 36f)
						{
							this.slash = false;
							base.npc.ai[1] = 0f;
							this.slashCounter = 0;
							this.slashFrame = 0;
							base.npc.netUpdate = true;
						}
					}
				}
			}
			else
			{
				this.hop = true;
			}
			if (this.roll)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frame.Y < 1008)
				{
					base.npc.frame.Y = 1008;
				}
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc2 = base.npc;
					npc2.frame.Y = npc2.frame.Y + 84;
					if (base.npc.frame.Y > 1596)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 1008;
					}
				}
			}
			if (!this.slash)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 500f && !this.roll && !this.slash)
			{
				this.roll = true;
				base.npc.netUpdate = true;
			}
			if (this.roll)
			{
				this.Move(new Vector2(0f, 0f));
				base.npc.noTileCollide = true;
				base.npc.noGravity = true;
				base.npc.defense = 90;
				if (base.npc.ai[2] >= 900f)
				{
					this.roll = false;
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			else
			{
				base.npc.noGravity = false;
				base.npc.noTileCollide = false;
				base.npc.defense = 40;
				base.npc.aiStyle = -1;
				this.aiType = 0;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && base.npc.life >= (int)((float)base.npc.lifeMax * 0.1f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.09f, 8f, 30, 12, 60, true, 10, 60, false, null, false);
				}
				else if (base.npc.life < (int)((float)base.npc.lifeMax * 0.1f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.11f, 12f, 40, 20, 60, true, 10, 60, false, null, false);
				}
				else if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.07f, 5f, 26, 8, 60, true, 10, 60, false, null, false);
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.9f) && !this.summon1)
			{
				int Minion = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion].netUpdate = true;
				int Minion2 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion2].netUpdate = true;
				int Minion3 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion3].netUpdate = true;
				int Minion4 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion4].netUpdate = true;
				this.summon1 = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && !this.summon2)
			{
				int Minion5 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion5].netUpdate = true;
				int Minion6 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion6].netUpdate = true;
				int Minion7 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion7].netUpdate = true;
				int Minion8 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion8].netUpdate = true;
				int Minion9 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion9].netUpdate = true;
				this.summon2 = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && !this.summon3)
			{
				int Minion10 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion10].netUpdate = true;
				int Minion11 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion11].netUpdate = true;
				int Minion12 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion12].netUpdate = true;
				int Minion13 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion13].netUpdate = true;
				int Minion14 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion14].netUpdate = true;
				this.summon3 = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.1f) && !this.summon4)
			{
				int Minion15 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion15].netUpdate = true;
				int Minion16 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion16].netUpdate = true;
				int Minion17 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion17].netUpdate = true;
				int Minion18 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPilePZ"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion18].netUpdate = true;
				this.summon4 = true;
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void Move(Vector2 offset)
		{
			if (this.roll)
			{
				this.speed = 14f;
			}
			Vector2 move = this.player.Center + offset - base.npc.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 18f;
			move = (base.npc.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.npc.velocity = move;
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -20f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
					return;
				}
			}
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestGolemPZHop");
			Texture2D slashAni = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestGolemPZSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (this.roll)
			{
				new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
				for (int i = this.oldPos.Length - 1; i >= 0; i--)
				{
					float alpha = 1f - (float)(i + 1) / (float)(this.oldPos.Length + 2);
					spriteBatch.Draw(texture, this.oldPos[i] - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * (0.5f * alpha), this.oldrot[i], Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				}
			}
			if (!this.hop && !this.slash)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.roll && !this.slash)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = num214 * this.hopFrame;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.slash && !this.roll)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y - 13f);
				int num215 = slashAni.Height / 9;
				int y7 = num215 * this.slashFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, slashAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 35; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 4.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 4.6f;
		}

		private bool roll;

		private bool hop;

		private int hopFrame;

		private bool slash;

		private int slashFrame;

		private int slashCounter;

		private bool summon1;

		private bool summon2;

		private bool summon3;

		private bool summon4;

		private Player player;

		private float speed;

		private Vector2[] oldPos = new Vector2[3];

		private float[] oldrot = new float[3];
	}
}
