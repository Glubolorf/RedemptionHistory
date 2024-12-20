using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	[AutoloadBossHead]
	public class EaglecrestGolem : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Golem");
			Main.npcFrameCount[base.npc.type] = 20;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 3200;
			base.npc.damage = 30;
			base.npc.defense = 18;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 2, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 80;
			base.npc.height = 80;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossForest1");
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			Player player = Main.player[base.npc.target];
			potionType = 188;
			if (!RedeWorld.downedEaglecrestGolem)
			{
				RedeWorld.redemptionPoints++;
				CombatText.NewText(player.getRect(), Color.Gold, "+1", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Living stones? Never seen that before.", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedEaglecrestGolem = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GolemEye"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientPebble"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientSlingShot"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientStone"), Main.rand.Next(14, 34), false, 0, false, false);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
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
					this.slashCounter++;
					if (this.slashCounter > 5)
					{
						this.slashFrame++;
						this.slashCounter = 0;
					}
					if (this.slashFrame >= 9)
					{
						this.slashFrame = 0;
					}
				}
				float num = base.npc.Distance(Main.player[base.npc.target].Center);
				if (num <= 300f && Main.rand.Next(150) == 0 && !this.slash && !this.roll)
				{
					this.slash = true;
					base.npc.netUpdate = true;
				}
				if (this.slash)
				{
					base.npc.ai[1] += 1f;
					base.npc.velocity.X = 0f;
					if (base.npc.ai[1] == 1f && !Config.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityTrash, "Slash!", true, true);
						base.npc.netUpdate = true;
					}
					if (base.npc.ai[1] == 35f)
					{
						Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num2 = 7f;
						Vector2 vector;
						vector..ctor(base.npc.Center.X, base.npc.Center.Y);
						int num3 = 11;
						int num4 = base.mod.ProjectileType("GolemSlashPro1");
						float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
						int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
						Main.projectile[num6].netUpdate = true;
					}
					if (base.npc.ai[1] >= 45f)
					{
						this.slash = false;
						base.npc.ai[1] = 0f;
						this.slashCounter = 0;
						this.slashFrame = 0;
						base.npc.netUpdate = true;
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
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.07f, 10f, 26, 8, 60, true, 10, 60, false, null, false);
				base.npc.defense = 30;
				if (base.npc.ai[2] >= 900f)
				{
					this.roll = false;
					base.npc.ai[2] = 0f;
					base.npc.netUpdate = true;
				}
			}
			else
			{
				base.npc.defense = 18;
				base.npc.aiStyle = -1;
				this.aiType = 0;
				if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && base.npc.life >= (int)((float)base.npc.lifeMax * 0.1f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.06f, 4f, 20, 5, 60, true, 10, 60, false, null, false);
				}
				else if (base.npc.life < (int)((float)base.npc.lifeMax * 0.1f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.07f, 6f, 26, 6, 60, true, 10, 60, false, null, false);
				}
				else if (base.npc.life >= (int)((float)base.npc.lifeMax * 0.5f))
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.04f, 2f, 16, 4, 60, true, 10, 60, false, null, false);
				}
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.9f) && !this.summon1)
			{
				int num7 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num7].netUpdate = true;
				int num8 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num8].netUpdate = true;
				this.summon1 = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f) && !this.summon2)
			{
				int num9 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num9].netUpdate = true;
				int num10 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num10].netUpdate = true;
				int num11 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num11].netUpdate = true;
				this.summon2 = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.25f) && !this.summon3)
			{
				int num12 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num12].netUpdate = true;
				int num13 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num13].netUpdate = true;
				int num14 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num14].netUpdate = true;
				this.summon3 = true;
				return;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.1f) && !this.summon4)
			{
				int num15 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num15].netUpdate = true;
				int num16 = NPC.NewNPC((int)base.npc.position.X + Main.rand.Next(0, 80), (int)base.npc.position.Y + Main.rand.Next(0, 84), base.mod.NPCType("EaglecrestRockPile"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[num16].netUpdate = true;
				this.summon4 = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestGolemHop");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/EaglecrestGolem/EaglecrestGolemSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.slash)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop && !this.roll && !this.slash)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.slash && !this.roll)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y - 13f);
				int num3 = texture2.Height / 9;
				int num4 = num3 * this.slashFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 35; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].velocity *= 4.6f;
				}
			}
			int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[num2].velocity *= 4.6f;
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
	}
}
