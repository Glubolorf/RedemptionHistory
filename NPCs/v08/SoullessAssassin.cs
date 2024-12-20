using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class SoullessAssassin : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soulless Assassin");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 30;
			base.npc.height = 42;
			base.npc.damage = 85;
			base.npc.defense = 0;
			base.npc.lifeMax = 12000;
			base.npc.HitSound = SoundID.NPCHit48;
			base.npc.DeathSound = SoundID.NPCDeath50;
			base.npc.value = (float)Item.buyPrice(0, 0, 55, 0);
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = -1;
		}

		public override void AI()
		{
			if (this.stabAttack)
			{
				this.stabCounter++;
				if (this.stabCounter > 5)
				{
					this.stabFrame++;
					this.stabCounter = 0;
				}
				if (this.stabFrame >= 6)
				{
					this.stabFrame = 0;
				}
			}
			Player player = Main.player[base.npc.target];
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.velocity.Y == 0f)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 5.0 || base.npc.frameCounter <= -5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 52;
					if (base.npc.frame.Y > 104)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.stabAttack)
				{
					this.stabCounter++;
					if (this.stabCounter > 5)
					{
						this.stabFrame++;
						this.stabCounter = 0;
					}
					if (this.stabFrame >= 6)
					{
						this.stabFrame = 0;
					}
				}
				float num = base.npc.Distance(Main.player[base.npc.target].Center);
				if (num <= 500f && Main.rand.Next(200) == 0 && !this.stabAttack)
				{
					this.stabAttack = true;
				}
				if (!this.stabAttack)
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.1f, 1.5f, 8, 5, 60, true, 10, 60, false, null, false);
				}
				if (this.stabAttack)
				{
					this.stabTimer++;
					base.npc.velocity.X = 0f;
					if (this.stabTimer == 1 && !Config.NoCombatText)
					{
						int num2 = Main.rand.Next(8);
						if (num2 == 0)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Sikk!", true, true);
						}
						if (num2 == 1)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Sibu!", true, true);
						}
						if (num2 == 2)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cut’nin doz.", true, true);
						}
						if (num2 == 3)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Uf cul’ ut ufe...", true, true);
						}
						if (num2 == 4)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Ka dosmok cul’...", true, true);
						}
						if (num2 == 5)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cult’nin un yei ruk’...", true, true);
						}
						if (num2 == 6)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Consu’nin yei min’...", true, true);
						}
						if (num2 == 7)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Jugh niqui tie...", true, true);
						}
					}
					if (this.stabTimer == 15)
					{
						for (int i = 0; i < 20; i++)
						{
							int num3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num3].velocity *= 1.4f;
						}
						base.npc.netUpdate = true;
					}
					if (this.stabTimer == 17 && Main.netMode != 1)
					{
						if (player.direction == 1)
						{
							Vector2 vector;
							vector..ctor((float)Main.rand.Next(-34, -32), 0f);
							base.npc.Center = Main.player[base.npc.target].Center + vector;
							base.npc.netUpdate = true;
						}
						else
						{
							Vector2 vector2;
							vector2..ctor((float)Main.rand.Next(32, 34), 0f);
							base.npc.Center = Main.player[base.npc.target].Center + vector2;
							base.npc.netUpdate = true;
						}
					}
					if (this.stabTimer == 19)
					{
						for (int j = 0; j < 20; j++)
						{
							int num4 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[num4].velocity *= 1.4f;
						}
						base.npc.netUpdate = true;
					}
					if (this.stabTimer == 24)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num5 = 5f;
						Vector2 vector3;
						vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int num6 = 55;
						int num7 = base.mod.ProjectileType("DamagePro1");
						float num8 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
						int num9 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num8) * (double)num5 * -1.0) + (float)Main.rand.Next(-1, 1), num7, num6, 0f, 0, 0f, 0f);
						Main.projectile[num9].netUpdate = true;
					}
					if (this.stabTimer >= 30)
					{
						this.stabAttack = false;
						this.stabTimer = 0;
						this.stabCounter = 0;
						this.stabFrame = 0;
						return;
					}
				}
			}
			else
			{
				this.hop = true;
			}
		}

		public override void NPCLoot()
		{
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.position.Y + Main.rand.Next(-42, 0), base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.rand.Next(4) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.position.Y + Main.rand.Next(-42, 0), base.mod.NPCType("ShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].velocity *= 2.6f;
				}
			}
			int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 1f);
			Main.dust[num2].velocity *= 1.6f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/SoullessAssassinHop");
			Texture2D texture2 = base.mod.GetTexture("NPCs/v08/SoullessAssassinStab");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.stabAttack && !this.hop)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop && !this.stabAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.stabAttack)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 6;
				int num4 = num3 * this.stabFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool stabAttack;

		private int stabFrame;

		private int stabCounter;

		private int stabTimer;

		private bool change;

		private bool hop;

		private int teleportTimer;

		private int hopFrame;
	}
}
