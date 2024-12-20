using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Projectiles;
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
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 500f && Main.rand.Next(200) == 0 && !this.stabAttack)
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
					if (this.stabTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						int num55 = Main.rand.Next(8);
						if (num55 == 0)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Sikk!", true, true);
						}
						if (num55 == 1)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Sibu!", true, true);
						}
						if (num55 == 2)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cut�nin doz.", true, true);
						}
						if (num55 == 3)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Uf cul� ut ufe...", true, true);
						}
						if (num55 == 4)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Ka dosmok cul�...", true, true);
						}
						if (num55 == 5)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cult�nin un yei ruk�...", true, true);
						}
						if (num55 == 6)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Consu�nin yei min�...", true, true);
						}
						if (num55 == 7)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Jugh niqui tie...", true, true);
						}
					}
					if (this.stabTimer == 15)
					{
						for (int i = 0; i < 20; i++)
						{
							int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex].velocity *= 1.4f;
						}
						base.npc.netUpdate = true;
					}
					if (this.stabTimer == 17 && Main.netMode != 1)
					{
						if (player.direction == 1)
						{
							Vector2 newPos = new Vector2((float)Main.rand.Next(-34, -32), 0f);
							base.npc.Center = Main.player[base.npc.target].Center + newPos;
							base.npc.netUpdate = true;
						}
						else
						{
							Vector2 newPos2 = new Vector2((float)Main.rand.Next(32, 34), 0f);
							base.npc.Center = Main.player[base.npc.target].Center + newPos2;
							base.npc.netUpdate = true;
						}
					}
					if (this.stabTimer == 19)
					{
						for (int j = 0; j < 20; j++)
						{
							int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 1.2f);
							Main.dust[dustIndex2].velocity *= 1.4f;
						}
						base.npc.netUpdate = true;
					}
					if (this.stabTimer == 24)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 5f;
						Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage = 55;
						int type = ModContent.ProjectileType<DamagePro1>();
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
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
				NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.position.Y + Main.rand.Next(-42, 0), ModContent.NPCType<SmallShadesoulNPC>(), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.rand.Next(4) == 0)
				{
					NPC.NewNPC((int)base.npc.Center.X + Main.rand.Next(-12, 12), (int)base.npc.position.Y + Main.rand.Next(-42, 0), ModContent.NPCType<ShadesoulNPC>(), 0, 0f, 0f, 0f, 0f, 255);
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 40; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 1.6f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/v08/SoullessAssassinHop");
			Texture2D throwAni = base.mod.GetTexture("NPCs/v08/SoullessAssassinStab");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.stabAttack && !this.hop)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.stabAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.stabAttack)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = throwAni.Height / 6;
				int y7 = num215 * this.stabFrame;
				Main.spriteBatch.Draw(throwAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, throwAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)throwAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool stabAttack;

		private int stabFrame;

		private int stabCounter;

		private int stabTimer;

		private bool hop;
	}
}
