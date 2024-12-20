using System;
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
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 100f && Main.rand.Next(50) == 0 && !this.specialAttack)
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
					if (this.attackTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						int num55 = Main.rand.Next(7);
						if (num55 == 0)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Swush!", true, true);
						}
						if (num55 == 1)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cut�nin doz.", true, true);
						}
						if (num55 == 2)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Uf cul� ut ufe...", true, true);
						}
						if (num55 == 3)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Ka dosmok cul�...", true, true);
						}
						if (num55 == 4)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cult�nin un yei ruk�...", true, true);
						}
						if (num55 == 5)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Consu�nin yei min�...", true, true);
						}
						if (num55 == 6)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Jugh niqui tie...", true, true);
						}
					}
					if (this.attackTimer == 35)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 8f;
						Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage = 55;
						int type = base.mod.ProjectileType("DamagePro2");
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						if (Main.netMode != 1)
						{
							Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), type, damage, 0f, 0, 0f, 0f);
						}
					}
					if (this.attackTimer == 55)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed2 = 10f;
						Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage2 = 55;
						int type2 = base.mod.ProjectileType("DamagePro2");
						float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-1, 1), type2, damage2, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
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
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
			int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("VoidFlame"), 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex3].velocity *= 1.6f;
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/v08/SoullessDuellerHop");
			Texture2D attackAni = base.mod.GetTexture("NPCs/v08/SoullessDuellerSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack && !this.hop)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.specialAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = num214 * this.hopFrame;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = attackAni.Height / 15;
				int y7 = num215 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, attackAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
