using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class SoullessWanderer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soulless Wanderer");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 54;
			base.npc.height = 52;
			base.npc.damage = 80;
			base.npc.defense = 0;
			base.npc.lifeMax = 11000;
			base.npc.HitSound = SoundID.NPCHit48;
			base.npc.DeathSound = SoundID.NPCDeath50;
			base.npc.value = (float)Item.buyPrice(0, 0, 50, 0);
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = -1;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * (RedeWorld.downedPatientZero ? 0.04f : 0f);
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

		public override void AI()
		{
			if (!this.change)
			{
				int num = Main.rand.Next(3);
				if (num == 0)
				{
					base.npc.SetDefaults(base.mod.NPCType("SoullessAssassin"), -1f);
					this.change = true;
				}
				if (num == 1)
				{
					base.npc.SetDefaults(base.mod.NPCType("SoullessDueller"), -1f);
					this.change = true;
				}
				if (num >= 2)
				{
					this.change = true;
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
					npc.frame.Y = npc.frame.Y + 60;
					if (base.npc.frame.Y > 120)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.throwAttack)
				{
					this.throwCounter++;
					if (this.throwCounter > 5)
					{
						this.throwFrame++;
						this.throwCounter = 0;
					}
					if (this.throwFrame >= 6)
					{
						this.throwFrame = 0;
					}
				}
				float num2 = base.npc.Distance(Main.player[base.npc.target].Center);
				if (num2 <= 500f && Main.rand.Next(50) == 0 && !this.throwAttack)
				{
					this.throwAttack = true;
				}
				if (!this.throwAttack)
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.1f, 1.5f, 5, 5, 60, true, 10, 60, false, null, false);
				}
				if (this.throwAttack)
				{
					this.throwTimer++;
					base.npc.velocity.X = 0f;
					if (this.throwTimer == 1 && !Config.NoCombatText)
					{
						int num3 = Main.rand.Next(8);
						if (num3 == 0)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Sikk!", true, true);
						}
						if (num3 == 1)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Phish!", true, true);
						}
						if (num3 == 2)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cut’nin doz.", true, true);
						}
						if (num3 == 3)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Uf cul’ ut ufe...", true, true);
						}
						if (num3 == 4)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Ka dosmok cul’...", true, true);
						}
						if (num3 == 5)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cult’nin un yei ruk’...", true, true);
						}
						if (num3 == 6)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Consu’nin yei min’...", true, true);
						}
						if (num3 == 7)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Jugh niqui tie...", true, true);
						}
					}
					if (this.throwTimer == 19)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num4 = 16f;
						Vector2 vector;
						vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int num5 = 55;
						int num6 = base.mod.ProjectileType("ShadeJavelinPro1");
						float num7 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
						int num8 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num7) * (double)num4 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num7) * (double)num4 * -1.0) + (float)Main.rand.Next(-1, 1), num6, num5, 0f, 0, 0f, 0f);
						Main.projectile[num8].netUpdate = true;
					}
					if (this.throwTimer >= 30)
					{
						this.throwAttack = false;
						this.throwTimer = 0;
						this.throwCounter = 0;
						this.throwFrame = 0;
						return;
					}
				}
			}
			else
			{
				this.hop = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/SoullessWandererHop");
			Texture2D texture2 = base.mod.GetTexture("NPCs/v08/SoullessWandererAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.throwAttack && !this.hop)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop && !this.throwAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.throwAttack)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 6;
				int num4 = num3 * this.throwFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool hop;

		private int hopFrame;

		private bool throwAttack;

		private int throwFrame;

		private int throwCounter;

		private int throwTimer;

		private bool change;
	}
}
