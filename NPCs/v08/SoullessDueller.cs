﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class SoullessDueller : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soulless Duelist");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 56;
			base.npc.damage = 90;
			base.npc.defense = 0;
			base.npc.lifeMax = 14500;
			base.npc.HitSound = SoundID.NPCHit48;
			base.npc.DeathSound = SoundID.NPCDeath50;
			base.npc.value = (float)Item.buyPrice(0, 0, 55, 0);
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = -1;
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
			if (base.npc.velocity.Y == 0f)
			{
				this.hop = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 5.0 || base.npc.frameCounter <= -5.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 66;
					if (base.npc.frame.Y > 198)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.specialAttack)
				{
					this.attackCounter++;
					if (this.attackCounter > 5)
					{
						this.attackFrame++;
						this.attackCounter = 0;
					}
					if (this.attackFrame >= 15)
					{
						this.attackFrame = 0;
					}
				}
				float num = base.npc.Distance(Main.player[base.npc.target].Center);
				if (num <= 100f && Main.rand.Next(50) == 0 && !this.specialAttack)
				{
					this.specialAttack = true;
				}
				if (!this.specialAttack)
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.1f, 1.7f, 5, 5, 60, true, 10, 60, false, null, false);
				}
				if (this.specialAttack)
				{
					this.attackTimer++;
					base.npc.velocity.X = 0f;
					if (this.attackTimer == 1 && !Config.NoCombatText)
					{
						int num2 = Main.rand.Next(7);
						if (num2 == 0)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Swush!", true, true);
						}
						if (num2 == 1)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cut’nin doz.", true, true);
						}
						if (num2 == 2)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Uf cul’ ut ufe...", true, true);
						}
						if (num2 == 3)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Ka dosmok cul’...", true, true);
						}
						if (num2 == 4)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cult’nin un yei ruk’...", true, true);
						}
						if (num2 == 5)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Consu’nin yei min’...", true, true);
						}
						if (num2 == 6)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Jugh niqui tie...", true, true);
						}
					}
					if (this.attackTimer == 35)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num3 = 8f;
						Vector2 vector;
						vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int num4 = 55;
						int num5 = base.mod.ProjectileType("DamagePro2");
						float num6 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num6) * (double)num3 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num6) * (double)num3 * -1.0) + (float)Main.rand.Next(-1, 1), num5, num4, 0f, 0, 0f, 0f);
						}
					}
					if (this.attackTimer == 55)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num7 = 10f;
						Vector2 vector2;
						vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int num8 = 55;
						int num9 = base.mod.ProjectileType("DamagePro2");
						float num10 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
						int num11 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num10) * (double)num7 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)num10) * (double)num7 * -1.0) + (float)Main.rand.Next(-1, 1), num9, num8, 0f, 0, 0f, 0f);
						Main.projectile[num11].netUpdate = true;
					}
					if (this.attackTimer >= 85)
					{
						this.specialAttack = false;
						this.attackTimer = 0;
						this.attackCounter = 0;
						this.attackFrame = 0;
						return;
					}
				}
			}
			else
			{
				this.hop = true;
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
			Texture2D texture = base.mod.GetTexture("NPCs/v08/SoullessDuellerHop");
			Texture2D texture2 = base.mod.GetTexture("NPCs/v08/SoullessDuellerSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack && !this.hop)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop && !this.specialAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 15;
				int num4 = num3 * this.attackFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;

		private int throwCounter;

		private int throwFrame;

		private bool change;

		public bool hop;

		private int hopFrame;

		private bool throwAttack;
	}
}