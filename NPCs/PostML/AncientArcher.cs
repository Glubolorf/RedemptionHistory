using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Materials.PostML;
using Redemption.NPCs.Friendly;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PostML
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
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientPowerCore>(), Main.rand.Next(1, 3), false, 0, false, false);
				return;
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientCoreF>(), Main.rand.Next(1, 3), false, 0, false, false);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			float distance = base.npc.Distance(Main.player[base.npc.target].Center);
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
				if (distance <= 600f && Main.rand.Next(120) == 0 && !this.shoot && !this.shootAir)
				{
					this.shoot = true;
				}
				if (this.shoot)
				{
					this.shootTimer++;
					base.npc.velocity.X = 0f;
					if (this.shootTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Energy Bolts!", true, true);
					}
					if (this.shootTimer == 30 || this.shootTimer == 34 || this.shootTimer == 38)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed = 18f;
						Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage = 55;
						int type = ModContent.ProjectileType<AncientShot>();
						float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
						int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
						Main.projectile[num54].netUpdate = true;
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
					if (this.shootTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Energy Bolt!", true, true);
					}
					if (this.shootTimer == 2)
					{
						Main.PlaySound(SoundID.Item125, (int)base.npc.position.X, (int)base.npc.position.Y);
						float Speed2 = 21f;
						Vector2 vector9 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
						int damage2 = 55;
						int type2 = ModContent.ProjectileType<AncientShot>();
						float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
						int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
						Main.projectile[num55].netUpdate = true;
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
				if (distance <= 700f)
				{
					BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.3f, 7f, 17, 17, 60, true, 10, 60, false, null, false);
					return;
				}
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.08f, 1f, 8, 8, 60, true, 10, 60, false, null, false);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/PostML/AncientArcherJump");
			Texture2D shootAni = base.mod.GetTexture("NPCs/PostML/AncientArcherAttack");
			Texture2D shootAirAni = base.mod.GetTexture("NPCs/PostML/AncientArcherJumpAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.shoot && !this.shootAir)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop && !this.shoot && !this.shootAir)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.shoot)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = shootAni.Height / 2;
				int y7 = num215 * this.shootFrame;
				Main.spriteBatch.Draw(shootAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, shootAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)shootAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.shootAir)
			{
				Vector2 drawCenter3 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num216 = shootAirAni.Height / 2;
				int y8 = num216 * this.shootAirFrame;
				Main.spriteBatch.Draw(shootAirAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, shootAirAni.Width, num216)), drawColor, base.npc.rotation, new Vector2((float)shootAirAni.Width / 2f, (float)num216 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
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
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<LostSoul3>(), 0, 0f, 0f, 0f, 0f, 255);
				}
				for (int i = 0; i < 20; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
				for (int j = 0; j < 10; j++)
				{
					int dustIndex3 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex3].velocity *= 2.6f;
				}
			}
			int dustIndex4 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 1f);
			Main.dust[dustIndex4].velocity *= 4.6f;
		}

		private bool hop;

		private bool shoot;

		private int shootFrame;

		private bool shootAir;

		private int shootAirFrame;

		private int shootAirCounter;

		private int shootCounter;

		private int shootTimer;
	}
}
