using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.Items.Placeable.Tiles;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.PreHM.Druid.Staves;
using Redemption.Items.Weapons.PreHM.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PreHM
{
	public class AncientStoneGolem : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Gladestone Golem");
			Main.npcFrameCount[base.npc.type] = 12;
		}

		public override void SetDefaults()
		{
			base.npc.width = 54;
			base.npc.height = 80;
			base.npc.damage = 35;
			base.npc.friendly = false;
			base.npc.defense = 20;
			base.npc.lifeMax = 125;
			base.npc.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 5000f;
			base.npc.knockBackResist = 0.2f;
			base.npc.aiStyle = -1;
			base.npc.lavaImmune = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<AncientStoneGolemBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AncientGolemGore8"), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (base.npc.ai[0] == 0f)
			{
				Main.PlaySound(29, base.npc.position, 63);
				base.npc.ai[0] = 1f;
			}
		}

		public override void NPCLoot()
		{
			RedePlayer redePlayer = Main.LocalPlayer.GetModPlayer<RedePlayer>();
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientStone>(), Main.rand.Next(4, 13), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientDirt>(), Main.rand.Next(2, 7), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<AncientWorldStave>(), 1, false, 0, false, false);
			if (Utils.NextBool(Main.rand, 30))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 3109, 1, false, 0, false, false);
			}
			if (Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 150 : 200))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<VictorBattletome>(), 1, false, 0, false, false);
			}
			if (Main.hardMode && Utils.NextBool(Main.rand, redePlayer.bloomingLuck ? 150 : 200))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<BlindJustice>(), 1, false, 0, false, false);
			}
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(this.customAI[0]);
			writer.Write(this.customAI[1]);
			writer.Write(this.customAI[2]);
			writer.Write(this.customAI[3]);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			this.customAI[0] = reader.ReadFloat();
			this.customAI[1] = reader.ReadFloat();
			this.customAI[2] = reader.ReadFloat();
			this.customAI[3] = reader.ReadFloat();
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.collideY || base.npc.velocity.Y == 0f)
			{
				base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
				if (base.npc.frameCounter >= 3.0 || base.npc.frameCounter <= -3.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 88;
					if (base.npc.frame.Y >= 1056)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			else
			{
				base.npc.frame.Y = 264;
			}
			float obj = base.npc.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (3f.Equals(obj))
						{
							this.aniType = 1;
							base.npc.ai[1] += 1f;
							this.frameCounter++;
							if (this.aniFrame < 6)
							{
								base.npc.velocity.X = 0f;
							}
							if (this.frameCounter > 5)
							{
								this.aniFrame++;
								this.frameCounter = 0;
								if (this.aniFrame == 1)
								{
									Main.PlaySound(29, base.npc.position, 64);
									int tilePosY = BaseWorldGen.GetFirstTileFloor((int)base.npc.Center.X / 16, (int)base.npc.Center.Y / 16, true);
									base.npc.Shoot(new Vector2(base.npc.Center.X, (float)(tilePosY * 16 + 55)), ModContent.ProjectileType<AncientStonePillar_Pro>(), base.npc.damage, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
								}
								if (this.aniFrame == 6)
								{
									NPC npc2 = base.npc;
									npc2.velocity.Y = npc2.velocity.Y - (float)Main.rand.Next(10, 20);
									NPC npc3 = base.npc;
									npc3.velocity.X = npc3.velocity.X + (float)((base.npc.spriteDirection == 1) ? Main.rand.Next(2, 7) : Main.rand.Next(-7, -2));
								}
							}
							if (this.aniFrame >= 10)
							{
								this.aniType = 0;
								this.aniFrame = 0;
								base.npc.ai[2] = 0f;
								base.npc.ai[0] = 1f;
							}
						}
					}
					else
					{
						base.npc.velocity.X = 0f;
						this.aniType = 1;
						base.npc.ai[1] += 1f;
						this.frameCounter++;
						if (this.frameCounter > 5)
						{
							this.aniFrame++;
							this.frameCounter = 0;
							if (this.aniFrame == 1)
							{
								Main.PlaySound(29, base.npc.position, 64);
								int tilePosY2 = BaseWorldGen.GetFirstTileFloor((int)player.Center.X / 16, (int)player.Center.Y / 16, true);
								base.npc.Shoot(new Vector2(player.Center.X, (float)(tilePosY2 * 16 + 55)), ModContent.ProjectileType<AncientStonePillar_Pro>(), base.npc.damage, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
							}
						}
						if (this.aniFrame >= 10)
						{
							this.aniType = 0;
							this.aniFrame = 0;
							base.npc.ai[0] = 1f;
						}
					}
				}
				else
				{
					if (base.npc.velocity.X > 0f)
					{
						base.npc.spriteDirection = 1;
					}
					else
					{
						base.npc.spriteDirection = -1;
					}
					base.npc.ai[1] += 1f;
					if (Utils.NextBool(Main.rand, 200) && base.npc.velocity.Y == 0f)
					{
						int dist = BaseWorldGen.GetFirstTileFloor((int)player.Center.X / 16, (int)player.Center.Y / 16, true) * 16 - (int)player.Center.Y;
						if (base.npc.Distance(player.Center) < 300f && dist < 140 && player.active && !player.dead)
						{
							base.npc.ai[0] = 2f;
						}
						else
						{
							base.npc.ai[0] = 3f;
						}
					}
					if (base.npc.ai[1] > 300f)
					{
						base.npc.ai[1] = 0f;
						base.npc.ai[0] = 0f;
					}
				}
			}
			else
			{
				if (base.npc.velocity.X > 0f)
				{
					base.npc.spriteDirection = 1;
				}
				else
				{
					base.npc.spriteDirection = -1;
				}
				bool sight = (base.npc.Center.X > player.Center.X && base.npc.spriteDirection == -1) || (base.npc.Center.X < player.Center.X && base.npc.spriteDirection == 1);
				if (Collision.CanHit(base.npc.position, base.npc.width, base.npc.height, player.position, player.width, player.height) && sight && player.active && !player.dead)
				{
					Main.PlaySound(29, base.npc.position, 63);
					base.npc.ai[0] = 1f;
				}
				if (base.npc.lavaWet && Utils.NextBool(Main.rand, 250) && base.npc.velocity.Y == 0f)
				{
					base.npc.ai[0] = 3f;
				}
			}
			if (base.npc.ai[0] < 2f)
			{
				bool jumpDownPlatforms = false;
				base.npc.JumpDownPlatform(ref jumpDownPlatforms, 20);
				if (jumpDownPlatforms)
				{
					base.npc.noTileCollide = true;
				}
				else
				{
					base.npc.noTileCollide = false;
				}
				BaseAI.AIZombie(base.npc, ref this.customAI, false, true, -1, 0.1f, (float)((base.npc.ai[0] == 1f) ? 3 : 1), 10, 1, 60, true, 10, 60, base.npc.Center.Y > player.Center.Y, null, false);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/PreHM/AncientStoneGolem_Attack");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			int num215 = this.aniType;
			if (num215 != 0)
			{
				if (num215 == 1)
				{
					Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int num214 = attackAni.Height / 10;
					int y6 = num214 * this.aniFrame;
					Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
				}
			}
			else
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public int aniType;

		public int aniFrame;

		public int frameCounter;

		public float[] customAI = new float[4];
	}
}
