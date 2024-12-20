using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.Items.Usable;
using Redemption.Projectiles.Hostile;
using Redemption.WorldGeneration.Soulless;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Soulless
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
			return SpawnCondition.Cavern.Chance * (RedeWorld.downedPatientZero ? 0.15f : 0f);
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
			if (!Subworld.IsActive<SoullessSub>() && Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<EnterSoulless>(), 1, false, 0, false, false);
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

		public override void AI()
		{
			if (!this.change)
			{
				int num55 = Main.rand.Next(3);
				if (num55 == 0)
				{
					base.npc.SetDefaults(ModContent.NPCType<SoullessAssassin>(), -1f);
					this.change = true;
				}
				if (num55 == 1)
				{
					base.npc.SetDefaults(ModContent.NPCType<SoullessDueller>(), -1f);
					this.change = true;
				}
				if (num55 >= 2)
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
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 500f && Main.rand.Next(50) == 0 && !this.throwAttack)
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
					if (this.throwTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						int num56 = Main.rand.Next(8);
						if (num56 == 0)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Sikk!", true, true);
						}
						if (num56 == 1)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Phish!", true, true);
						}
						if (num56 == 2)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cut�nin doz.", true, true);
						}
						if (num56 == 3)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Uf cul� ut ufe...", true, true);
						}
						if (num56 == 4)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Ka dosmok cul�...", true, true);
						}
						if (num56 == 5)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Cult�nin un yei ruk�...", true, true);
						}
						if (num56 == 6)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Consu�nin yei min�...", true, true);
						}
						if (num56 == 7)
						{
							CombatText.NewText(base.npc.getRect(), Color.Black, "Jugh niqui tie...", true, true);
						}
					}
					if (this.throwTimer == 19)
					{
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 16f;
						Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
						int damage = 55;
						int type = ModContent.ProjectileType<ShadeJavelinPro1>();
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-1, 1), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
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
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/Soulless/SoullessWandererHop");
			Texture2D throwAni = base.mod.GetTexture("NPCs/Soulless/SoullessWandererAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.throwAttack && !this.hop)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.throwAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.throwAttack)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = throwAni.Height / 6;
				int y7 = num215 * this.throwFrame;
				Main.spriteBatch.Draw(throwAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, throwAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)throwAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool hop;

		private bool throwAttack;

		private int throwFrame;

		private int throwCounter;

		private int throwTimer;

		private bool change;
	}
}
