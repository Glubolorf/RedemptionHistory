using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInvasion
{
	public class TrojanChicken : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Trojan Chicken");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			if (RedeWorld.downedPatientZero)
			{
				base.npc.lifeMax = 60000;
				base.npc.damage = 100;
				base.npc.defense = 17;
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/ChickenInvasion1");
			}
			else
			{
				base.npc.lifeMax = 1200;
				base.npc.damage = 20;
				base.npc.defense = 7;
				this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossKingChicken");
			}
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 1, 25, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 166;
			base.npc.height = 154;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (this.specialAttack || this.specialAttack2)
			{
				this.attackCounter++;
				if (this.attackCounter > 20)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 7)
				{
					this.attackFrame = 0;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 162;
				if (base.npc.frame.Y > 486)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			this.openTimer++;
			if (this.openTimer >= 500 && !this.specialAttack && !this.specialAttack2)
			{
				int num = Main.rand.Next(2);
				if (num == 0)
				{
					this.specialAttack = true;
					this.openTimer = -140;
				}
				if (num == 1)
				{
					this.specialAttack2 = true;
					this.openTimer = -140;
				}
			}
			if (!this.specialAttack && !this.specialAttack2)
			{
				base.npc.aiStyle = -1;
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.06f, 1f, 1, 1, 60, true, 10, 60, false, null, false);
			}
			if (this.specialAttack)
			{
				this.attackTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer >= 60 && this.attackTimer <= 90)
				{
					if (Main.rand.Next(30) == 0)
					{
						if (base.npc.direction == -1)
						{
							int num2 = Projectile.NewProjectile(new Vector2(base.npc.Center.X - 33f, base.npc.position.Y + 76f), new Vector2((float)(-3 + Main.rand.Next(-3, 0)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickmanFly1"), 10, 3f, 255, 0f, 0f);
							Main.projectile[num2].netUpdate = true;
						}
						else
						{
							int num3 = Projectile.NewProjectile(new Vector2(base.npc.Center.X + 33f, base.npc.position.Y + 76f), new Vector2((float)(3 + Main.rand.Next(0, 3)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickmanFly1"), 10, 3f, 255, 0f, 0f);
							Main.projectile[num3].netUpdate = true;
						}
					}
					if (Main.rand.Next(30) == 0)
					{
						if (base.npc.direction == -1)
						{
							int num4 = Projectile.NewProjectile(new Vector2(base.npc.Center.X - 33f, base.npc.position.Y + 76f), new Vector2((float)(-3 + Main.rand.Next(-3, 0)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickmanFly2"), 10, 3f, 255, 0f, 0f);
							Main.projectile[num4].netUpdate = true;
						}
						else
						{
							int num5 = Projectile.NewProjectile(new Vector2(base.npc.Center.X + 33f, base.npc.position.Y + 76f), new Vector2((float)(3 + Main.rand.Next(0, 3)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickmanFly2"), 10, 3f, 255, 0f, 0f);
							Main.projectile[num5].netUpdate = true;
						}
					}
					if (Main.rand.Next(30) == 0)
					{
						if (base.npc.direction == -1)
						{
							int num6 = Projectile.NewProjectile(new Vector2(base.npc.Center.X - 33f, base.npc.position.Y + 76f), new Vector2((float)(-3 + Main.rand.Next(-3, 0)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickmanFly3"), 10, 3f, 255, 0f, 0f);
							Main.projectile[num6].netUpdate = true;
						}
						else
						{
							int num7 = Projectile.NewProjectile(new Vector2(base.npc.Center.X + 33f, base.npc.position.Y + 76f), new Vector2((float)(3 + Main.rand.Next(0, 3)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickmanFly3"), 10, 3f, 255, 0f, 0f);
							Main.projectile[num7].netUpdate = true;
						}
					}
				}
				if (this.attackTimer >= 140)
				{
					this.specialAttack = false;
					this.specialAttack2 = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
					return;
				}
			}
			else if (this.specialAttack2)
			{
				this.attackTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer >= 60 && this.attackTimer <= 90 && Main.rand.Next(4) == 0)
				{
					if (RedeWorld.downedPatientZero)
					{
						if (base.npc.direction == -1)
						{
							int num8 = Projectile.NewProjectile(new Vector2(base.npc.Center.X - 33f, base.npc.position.Y + 76f), new Vector2((float)(-3 + Main.rand.Next(-9, 0)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickenEggProH"), 50, 3f, 255, 0f, 0f);
							Main.projectile[num8].netUpdate = true;
						}
						else
						{
							int num9 = Projectile.NewProjectile(new Vector2(base.npc.Center.X + 33f, base.npc.position.Y + 76f), new Vector2((float)(3 + Main.rand.Next(0, 9)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickenEggProH"), 50, 3f, 255, 0f, 0f);
							Main.projectile[num9].netUpdate = true;
						}
					}
					else if (base.npc.direction == -1)
					{
						int num10 = Projectile.NewProjectile(new Vector2(base.npc.Center.X - 33f, base.npc.position.Y + 76f), new Vector2((float)(-3 + Main.rand.Next(-9, 0)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickenEggProH"), 10, 3f, 255, 0f, 0f);
						Main.projectile[num10].netUpdate = true;
					}
					else
					{
						int num11 = Projectile.NewProjectile(new Vector2(base.npc.Center.X + 33f, base.npc.position.Y + 76f), new Vector2((float)(3 + Main.rand.Next(0, 9)), (float)(-2 + Main.rand.Next(0, 3))), base.mod.ProjectileType("ChickenEggProH"), 10, 3f, 255, 0f, 0f);
						Main.projectile[num11].netUpdate = true;
					}
				}
				if (this.attackTimer >= 140)
				{
					this.specialAttack = false;
					this.specialAttack2 = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/ChickenInvasion/TrojanChickenOpenUp");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack && !this.specialAttack2)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialAttack || this.specialAttack2)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 7;
				int num2 = num * this.attackFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore8"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore9"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore10"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore10"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore10"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore10"), 1f);
				if (ChickWorld.chickArmy)
				{
					ChickWorld.ChickPoints2 += 4;
				}
				for (int i = 0; i < 2; i++)
				{
					int num = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num].scale = 1.5f;
					Main.gore[num].velocity.X = Main.gore[num].velocity.X + 1.5f;
					Main.gore[num].velocity.Y = Main.gore[num].velocity.Y + 1.5f;
					num = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num].scale = 1.5f;
					Main.gore[num].velocity.X = Main.gore[num].velocity.X - 1.5f;
					Main.gore[num].velocity.Y = Main.gore[num].velocity.Y + 1.5f;
					num = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num].scale = 1.5f;
					Main.gore[num].velocity.X = Main.gore[num].velocity.X + 1.5f;
					Main.gore[num].velocity.Y = Main.gore[num].velocity.Y - 1.5f;
					num = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num].scale = 1.5f;
					Main.gore[num].velocity.X = Main.gore[num].velocity.X - 1.5f;
					Main.gore[num].velocity.Y = Main.gore[num].velocity.Y - 1.5f;
				}
			}
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackTimer;

		private int attackCounter;

		private int openTimer;

		private bool specialAttack2;
	}
}
