using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Varients
{
	public class HallowNymph : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Nymph");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 42;
			base.npc.height = 68;
			base.npc.friendly = false;
			base.npc.damage = 45;
			base.npc.defense = 10;
			base.npc.lifeMax = 500;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 220f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = 3;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("ForestNymphBanner");
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
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
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 10.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 74;
					if (base.npc.frame.Y > 148)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
				if (this.slashAttack)
				{
					this.slashCounter++;
					if (this.slashCounter > 5)
					{
						this.slashFrame++;
						this.slashCounter = 0;
					}
					if (this.slashFrame >= 4)
					{
						this.slashFrame = 0;
					}
				}
				float num = base.npc.Distance(Main.player[base.npc.target].Center);
				if (num <= 200f && Main.rand.Next(150) == 0 && !this.slashAttack)
				{
					this.slashAttack = true;
				}
				if (!this.slashAttack)
				{
					base.npc.aiStyle = 3;
				}
				if (this.slashAttack)
				{
					this.slashTimer++;
					base.npc.aiStyle = 0;
					base.npc.velocity.X = 0f;
					if (this.slashTimer == 1 && !Config.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Color.Pink, "Crystal Scythes!", true, true);
					}
					if (this.slashTimer == 10 || this.slashTimer == 15 || this.slashTimer == 20)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item27, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num2 = 7f;
							Vector2 vector;
							vector..ctor(base.npc.Center.X, base.npc.Center.Y);
							int num3 = 23;
							int num4 = base.mod.ProjectileType("CrystalScythe");
							float num5 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
							int num6 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num5) * (double)num2 * -1.0), (float)(Math.Sin((double)num5) * (double)num2 * -1.0), num4, num3, 0f, 0, 0f, 0f);
							Main.projectile[num6].netUpdate = true;
						}
						else
						{
							Main.PlaySound(SoundID.Item27, (int)base.npc.position.X, (int)base.npc.position.Y);
							float num7 = 11f;
							Vector2 vector2;
							vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
							int num8 = 23;
							int num9 = base.mod.ProjectileType("CrystalScythe");
							float num10 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
							int num11 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num10) * (double)num7 * -1.0), (float)(Math.Sin((double)num10) * (double)num7 * -1.0), num9, num8, 0f, 0, 0f, 0f);
							Main.projectile[num11].netUpdate = true;
						}
					}
					if (this.slashTimer >= 20)
					{
						this.slashAttack = false;
						this.slashTimer = 0;
						this.slashCounter = 0;
						this.slashFrame = 0;
					}
				}
			}
			else
			{
				this.hop = true;
			}
			if (Main.raining)
			{
				this.regenTimer++;
				if (this.regenTimer >= 40 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
			if (base.npc.wet && !base.npc.lavaWet)
			{
				this.regenTimer++;
				if (this.regenTimer >= 30 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FNymphGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FNymphGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/FNymphGore2"), 1f);
				for (int i = 0; i < 10; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.4f;
				}
				for (int j = 0; j < 10; j++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 254, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num2].velocity *= 1.4f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Varients/HallowNymphHop");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Varients/HallowNymphMAGIC");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.slashAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.hop)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 1;
				int num2 = num * this.hopFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.slashAttack)
			{
				Vector2 vector2;
				vector2..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num3 = texture2.Height / 4;
				int num4 = num3 * this.slashFrame;
				Main.spriteBatch.Draw(texture2, vector2 - Main.screenPosition, new Rectangle?(new Rectangle(0, num4, texture2.Width, num3)), drawColor, base.npc.rotation, new Vector2((float)texture2.Width / 2f, (float)num3 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.raining)
			{
				return SpawnCondition.OverworldHallow.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 109) ? 0.008f : 0f);
			}
			return SpawnCondition.OverworldHallow.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 109) ? 0.002f : 0f);
		}

		private bool hop;

		private int hopFrame;

		private int hopCounter;

		private bool slashAttack;

		private int slashFrame;

		private int slashTimer;

		private int slashCounter;

		private int regenTimer;
	}
}
