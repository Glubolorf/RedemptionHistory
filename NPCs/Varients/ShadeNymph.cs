using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Varients
{
	public class ShadeNymph : ModNPC
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
			base.npc.damage = 28;
			base.npc.defense = 5;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 125f;
			base.npc.knockBackResist = 0.3f;
			base.npc.aiStyle = 3;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<ForestNymphBanner>();
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
					if (this.slashFrame >= 5)
					{
						this.slashFrame = 0;
					}
				}
				if (base.npc.Distance(Main.player[base.npc.target].Center) <= 160f && Main.rand.Next(150) == 0 && !this.slashAttack)
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
					if (this.slashTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
					{
						CombatText.NewText(base.npc.getRect(), Colors.RarityRed, "Slash!", true, true);
					}
					if (this.slashTimer == 15)
					{
						if (base.npc.direction == -1)
						{
							Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed = 7f;
							Vector2 vector8 = new Vector2(base.npc.position.X + 54f, base.npc.position.Y + 44f);
							int damage = 11;
							int type = ModContent.ProjectileType<ForestSicklePro3>();
							float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
							int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
							Main.projectile[num54].netUpdate = true;
						}
						else
						{
							Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
							float Speed2 = 7f;
							Vector2 vector9 = new Vector2(base.npc.position.X + 14f, base.npc.position.Y + 44f);
							int damage2 = 11;
							int type2 = ModContent.ProjectileType<ForestSicklePro3>();
							float rotation2 = (float)Math.Atan2((double)(vector9.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector9.X - (player.position.X + (float)player.width * 0.5f)));
							int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
							Main.projectile[num55].netUpdate = true;
						}
					}
					if (this.slashTimer >= 25)
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
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				for (int j = 0; j < 10; j++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 2, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex2].velocity *= 1.4f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/Varients/ShadeNymphHop");
			Texture2D slashAni = base.mod.GetTexture("NPCs/Varients/ShadeNymphSlash");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop && !this.slashAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.slashAttack)
			{
				Vector2 drawCenter2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num215 = slashAni.Height / 5;
				int y7 = num215 * this.slashFrame;
				Main.spriteBatch.Draw(slashAni, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, slashAni.Width, num215)), drawColor, base.npc.rotation, new Vector2((float)slashAni.Width / 2f, (float)num215 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.raining)
			{
				return SpawnCondition.Crimson.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 199) ? 0.008f : 0f);
			}
			return SpawnCondition.Crimson.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 199) ? 0.002f : 0f);
		}

		private bool hop;

		private bool slashAttack;

		private int slashFrame;

		private int slashTimer;

		private int slashCounter;

		private int regenTimer;
	}
}
