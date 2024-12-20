using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class OmegaAndroid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Android");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 30;
			base.npc.height = 42;
			base.npc.damage = 80;
			base.npc.defense = 50;
			base.npc.lifeMax = 2000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 1, 0, 0);
			base.npc.knockBackResist = 0.01f;
			base.npc.aiStyle = 3;
			this.aiType = 140;
			this.animationType = 140;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("OmegaAndroidBanner");
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((!RedeWorld.girusCloaked && Main.hardMode && RedeWorld.downedVlitch3) ? 0.03f : 0f);
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (this.fistStart)
			{
				this.fistCounter++;
				if (this.fistCounter > 5)
				{
					this.fistFrame++;
					this.fistCounter = 0;
				}
				if (this.fistFrame >= 8)
				{
					this.fistFrame = 0;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 500f && Main.rand.Next(150) == 0 && !this.fistAttack)
			{
				this.fistAttack = true;
			}
			if (!this.fistAttack)
			{
				base.npc.aiStyle = 3;
				this.fistStart = false;
			}
			if (this.fistAttack)
			{
				this.fistTimer++;
				this.fistStart = true;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.fistTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Red, "Rocket Fist!", true, true);
				}
				if (this.fistTimer == 25)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 29f, base.npc.position.Y + 21f), new Vector2(-6f, 0f), base.mod.ProjectileType("OmegaARocketFist"), 50, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
					}
					else
					{
						Main.PlaySound(SoundID.Item74, (int)base.npc.position.X, (int)base.npc.position.Y);
						int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 11f, base.npc.position.Y + 21f), new Vector2(6f, 0f), base.mod.ProjectileType("OmegaARocketFist"), 50, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
					}
				}
				if (this.fistTimer >= 40)
				{
					this.fistAttack = false;
					this.fistStart = false;
					this.fistTimer = 0;
					this.fistCounter = 0;
					this.fistFrame = 0;
				}
			}
			if (base.npc.ai[2] != 0f && base.npc.ai[3] != 0f)
			{
				Main.PlaySound(SoundID.Item8, base.npc.position);
				for (int num67 = 0; num67 < 25; num67++)
				{
					int num68 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num68].velocity *= 3f;
					Main.dust[num68].noGravity = true;
				}
				base.npc.position.X = base.npc.ai[2] * 16f - (float)(base.npc.width / 2) + 8f;
				base.npc.position.Y = base.npc.ai[3] * 16f - (float)base.npc.height;
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				base.npc.ai[2] = 0f;
				base.npc.ai[3] = 0f;
				Main.PlaySound(SoundID.Item8, base.npc.position);
				for (int num69 = 0; num69 < 25; num69++)
				{
					int num70 = Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num70].velocity *= 3f;
					Main.dust[num70].noGravity = true;
				}
			}
			if (Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) + Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 2000f)
			{
				base.npc.ai[0] = 650f;
			}
			if (base.npc.ai[0] >= 650f && Main.netMode != 1)
			{
				base.npc.ai[0] = 1f;
				int playerTilePositionX = (int)Main.player[base.npc.target].position.X / 16;
				int playerTilePositionY = (int)Main.player[base.npc.target].position.Y / 16;
				int npcTilePositionX = (int)base.npc.position.X / 16;
				int npcTilePositionY = (int)base.npc.position.Y / 16;
				int playerTargetShift = 40;
				int num71 = 0;
				for (int s = 0; s < 100; s++)
				{
					num71++;
					int nearPlayerX = Main.rand.Next(playerTilePositionX - playerTargetShift, playerTilePositionX + playerTargetShift);
					for (int num72 = Main.rand.Next(playerTilePositionY - playerTargetShift, playerTilePositionY + playerTargetShift); num72 < playerTilePositionY + playerTargetShift; num72++)
					{
						if ((nearPlayerX < playerTilePositionX - 12 || nearPlayerX > playerTilePositionX + 12) && (num72 < npcTilePositionY - 1 || num72 > npcTilePositionY + 1 || nearPlayerX < npcTilePositionX - 1 || nearPlayerX > npcTilePositionX + 1) && Main.tile[nearPlayerX, num72].nactive())
						{
							bool flag5 = true;
							if (Main.tile[nearPlayerX, num72 - 1].lava())
							{
								flag5 = false;
							}
							if (flag5 && Main.tileSolid[(int)Main.tile[nearPlayerX, num72].type] && !Collision.SolidTiles(nearPlayerX - 1, nearPlayerX + 1, num72 - 4, num72 - 1))
							{
								base.npc.ai[1] = 20f;
								base.npc.ai[2] = (float)nearPlayerX;
								base.npc.ai[3] = (float)num72 - 1f;
								break;
							}
						}
					}
				}
				base.npc.netUpdate = true;
			}
			if (base.npc.ai[1] > 0f)
			{
				base.npc.ai[1] -= 1f;
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OAndroidGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OAndroidGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OAndroidGore1"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.rand.Next(4) == 0 && base.npc.life <= 2000)
			{
				base.npc.ai[0] = 650f;
				base.npc.TargetClosest(true);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/OmegaAndroid_Glow");
			Texture2D fistAni = base.mod.GetTexture("NPCs/OmegaAndroidFistRocket");
			Texture2D fistGlow = base.mod.GetTexture("NPCs/OmegaAndroidFistRocket_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (!this.fistStart)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			if (this.fistStart)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = fistAni.Height / 8;
				int y6 = num214 * this.fistFrame;
				Main.spriteBatch.Draw(fistAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, fistAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)fistAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				Main.spriteBatch.Draw(fistGlow, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, fistAni.Width, num214)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)fistAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool fistStart;

		private int fistFrame;

		private int fistCounter;

		private bool fistAttack;

		private int fistTimer;
	}
}
