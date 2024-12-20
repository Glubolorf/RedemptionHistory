using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class AncientArcher : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Archer");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 21000;
			base.npc.damage = 60;
			base.npc.defense = 70;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 2, 40, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 44;
			base.npc.height = 50;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath43;
			base.npc.lavaImmune = true;
		}

		public override void NPCLoot()
		{
			if (RedeWorld.downedEaglecrestGolemPZ)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientPowerCore"), Main.rand.Next(1, 3), false, 0, false, false);
				return;
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientCoreF"), Main.rand.Next(1, 3), false, 0, false, false);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
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
				this.shootAir = false;
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 54;
					if (base.npc.frame.Y > 216)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.shoot)
				{
					this.shootCounter++;
					if (this.shootCounter > 5)
					{
						this.shootFrame++;
						this.shootCounter = 0;
					}
					if (this.shootFrame >= 2)
					{
						this.shootFrame = 0;
					}
				}
				if (this.shootAir)
				{
					this.shootAirCounter++;
					if (this.shootAirCounter > 5)
					{
						this.shootAirFrame++;
						this.shootAirCounter = 0;
					}
					if (this.shootAirFrame >= 2)
					{
						this.shootAirFrame = 0;
					}
				}
				if (num <= 600f && Main.rand.Next(120) == 0 && !this.shoot && !this.shootAir)
				{
					this.shoot = true;
				}
				if (this.shoot)
				{
					this.shootTimer++;
					base.npc.velocity.X = 0f;
					if (this.shootTimer == 1 && !Config.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Energy Bolts!", true, true);
					}
					if (this.shootTimer == 30 || this.shootTimer == 34 || this.shootTimer == 38)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num2 = 18f;
						Vector2 vector;
						vector..ctor(base.npc.Center.X, base.npc.Center.Y);
						int num3 = 55;
						int num4 = base.mod.ProjectileType("AncientShot");
						float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
						int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
						Main.projectile[num6].netUpdate = true;
					}
					if (this.shootTimer >= 60)
					{
						this.shoot = false;
						this.shootTimer = 0;
						this.shootCounter = 0;
						this.shootFrame = 0;
					}
				}
			}
			else
			{
				this.hop = true;
				if (Main.rand.Next(30) == 0 && !this.shootAir)
				{
					this.shootAir = true;
				}
				if (this.shootAir)
				{
					this.shootTimer++;
					if (this.shootTimer == 1 && !Config.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Energy Bolt!", true, true);
					}
					if (this.shootTimer == 2)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float num7 = 21f;
						Vector2 vector2;
						vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
						int num8 = 55;
						int num9 = base.mod.ProjectileType("AncientShot");
						float num10 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
						int num11 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num10) * (double)num7 * -1.0), (float)(Math.Sin((double)num10) * (double)num7 * -1.0), num9, num8, 0f, 0, 0f, 0f);
						Main.projectile[num11].netUpdate = true;
					}
					if (this.shootTimer >= 5)
					{
						this.shootTimer = 0;
						this.shootAir = false;
					}
				}
			}
			if (!this.shoot)
			{
				if (num <= 700f)
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.3f, 7f, 17, 17, 60, true, 10, 60, false, null, false);
					return;
				}
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.08f, 1f, 8, 8, 60, true, 10, 60, false, null, false);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/AncientArcherJump");
			Texture2D texture2 = base.mod.GetTexture("NPCs/v08/AncientArcherAttack");
			Texture2D texture3 = base.mod.GetTexture("NPCs/v08/AncientArcherJumpAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.shoot && !this.shootAir)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop && !this.shoot && !this.shootAir)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.shoot)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 2;
				int num4 = num3 * this.shootFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.shootAir)
			{
				Vector2 vector3;
				vector3..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num5 = texture3.Height / 2;
				int num6 = num5 * this.shootAirFrame;
				Main.spriteBatch.Draw(texture3, vector3 - Main.screenPosition, new Rectangle?(new Rectangle(0, num6, texture3.Width, num5)), drawColor, base.npc.rotation, new Vector2((float)texture3.Width / 2f, (float)num5 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * (RedeWorld.downedPatientZero ? 0.04f : 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				if (Main.netMode != 1 && base.npc.life <= 0)
				{
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("LostSoul3"), 0, 0f, 0f, 0f, 0f, 255);
				}
				for (int i = 0; i < 20; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].velocity *= 2.6f;
				}
				for (int j = 0; j < 10; j++)
				{
					int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num2].velocity *= 2.6f;
				}
			}
			int num3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[num3].velocity *= 4.6f;
		}

		private bool hop;

		private int hopFrame;

		private bool shoot;

		private int shootFrame;

		private bool shootAir;

		private int shootAirFrame;

		private int shootAirCounter;

		private int shootCounter;

		private int shootTimer;

		private int shootAirTimer;
	}
}
