using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class InfectedDiggerHead : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Digger");
			Main.npcFrameCount[base.npc.type] = 1;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 250;
			base.npc.damage = 50;
			base.npc.defense = 35;
			base.npc.knockBackResist = 0f;
			base.npc.width = 30;
			base.npc.height = 34;
			base.npc.friendly = false;
			base.npc.lavaImmune = false;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.behindTiles = true;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = (float)Item.buyPrice(0, 1, 0, 0);
			base.npc.npcSlots = 1f;
			base.npc.netAlways = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<InfectedDiggerBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/InfectedDiggerHeadGore"), 1f);
			}
		}

		public override bool PreAI()
		{
			if (Main.player[base.npc.target].dead)
			{
				base.npc.timeLeft = 0;
			}
			if (Main.netMode != 1 && base.npc.ai[0] == 0f)
			{
				base.npc.realLife = base.npc.whoAmI;
				int latestNPC = base.npc.whoAmI;
				int randomWormLength = Main.rand.Next(6, 8);
				for (int i = 0; i < randomWormLength; i++)
				{
					latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<InfectedDiggerBody>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
					Main.npc[latestNPC].realLife = base.npc.whoAmI;
					Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
				}
				latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<InfectedDiggerTail>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
				Main.npc[latestNPC].realLife = base.npc.whoAmI;
				Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
				base.npc.ai[0] = 1f;
				base.npc.netUpdate = true;
			}
			int minTilePosX = (int)((double)base.npc.position.X / 16.0) - 1;
			int maxTilePosX = (int)((double)(base.npc.position.X + (float)base.npc.width) / 16.0) + 2;
			int minTilePosY = (int)((double)base.npc.position.Y / 16.0) - 1;
			int maxTilePosY = (int)((double)(base.npc.position.Y + (float)base.npc.height) / 16.0) + 2;
			if (minTilePosX < 0)
			{
				minTilePosX = 0;
			}
			if (maxTilePosX > Main.maxTilesX)
			{
				maxTilePosX = Main.maxTilesX;
			}
			if (minTilePosY < 0)
			{
				minTilePosY = 0;
			}
			if (maxTilePosY > Main.maxTilesY)
			{
				maxTilePosY = Main.maxTilesY;
			}
			bool collision = false;
			for (int j = minTilePosX; j < maxTilePosX; j++)
			{
				for (int k = minTilePosY; k < maxTilePosY; k++)
				{
					if (Main.tile[j, k] != null && ((Main.tile[j, k].nactive() && (Main.tileSolid[(int)Main.tile[j, k].type] || (Main.tileSolidTop[(int)Main.tile[j, k].type] && Main.tile[j, k].frameY == 0))) || Main.tile[j, k].liquid > 64))
					{
						Vector2 vector2;
						vector2.X = (float)(j * 16);
						vector2.Y = (float)(k * 16);
						if (base.npc.position.X + (float)base.npc.width > vector2.X && (double)base.npc.position.X < (double)vector2.X + 16.0 && (double)(base.npc.position.Y + (float)base.npc.height) > (double)vector2.Y && (double)base.npc.position.Y < (double)vector2.Y + 16.0)
						{
							collision = true;
							if (Main.rand.Next(100) == 0 && Main.tile[j, k].nactive())
							{
								WorldGen.KillTile(j, k, true, true, false);
							}
						}
					}
				}
			}
			if (!collision)
			{
				Rectangle rectangle = new Rectangle((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height);
				int maxDistance = 1000;
				bool playerCollision = true;
				for (int index = 0; index < 255; index++)
				{
					if (Main.player[index].active)
					{
						Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - maxDistance, (int)Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
						if (rectangle.Intersects(rectangle2))
						{
							playerCollision = false;
							break;
						}
					}
				}
				if (playerCollision)
				{
					collision = true;
				}
			}
			float speed = 7f;
			float acceleration = 0.08f;
			Vector2 npcCenter = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float targetXPos = Main.player[base.npc.target].position.X + (float)(Main.player[base.npc.target].width / 2);
			double num2 = (double)(Main.player[base.npc.target].position.Y + (float)(Main.player[base.npc.target].height / 2));
			float targetRoundedPosX = (float)((int)((double)targetXPos / 16.0) * 16);
			float num3 = (float)((int)(num2 / 16.0) * 16);
			npcCenter.X = (float)((int)((double)npcCenter.X / 16.0) * 16);
			npcCenter.Y = (float)((int)((double)npcCenter.Y / 16.0) * 16);
			float dirX = targetRoundedPosX - npcCenter.X;
			float dirY = num3 - npcCenter.Y;
			float length = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));
			if (!collision)
			{
				base.npc.TargetClosest(true);
				base.npc.velocity.Y = base.npc.velocity.Y + 0.11f;
				if (base.npc.velocity.Y > speed)
				{
					base.npc.velocity.Y = speed;
				}
				if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)speed * 0.4)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration * 1.1f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration * 1.1f;
					}
				}
				else if (base.npc.velocity.Y == speed)
				{
					if (base.npc.velocity.X < dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration;
					}
					else if (base.npc.velocity.X > dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration;
					}
				}
				else if ((double)base.npc.velocity.Y > 4.0)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration * 0.9f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration * 0.9f;
					}
				}
			}
			else
			{
				if (base.npc.soundDelay == 0)
				{
					float num = length / 40f;
					if ((double)num < 10.0)
					{
						num = 10f;
					}
					if ((double)num > 20.0)
					{
						num = 20f;
					}
					base.npc.soundDelay = (int)num;
					Main.PlaySound(15, (int)base.npc.position.X, (int)base.npc.position.Y, 1, 1f, 0f);
				}
				float absDirX = Math.Abs(dirX);
				float absDirY = Math.Abs(dirY);
				float newSpeed = speed / length;
				dirX *= newSpeed;
				dirY *= newSpeed;
				if (((double)base.npc.velocity.X > 0.0 && (double)dirX > 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)dirX < 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)dirY > 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)dirY < 0.0))
				{
					if (base.npc.velocity.X < dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration;
					}
					else if (base.npc.velocity.X > dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration;
					}
					if (base.npc.velocity.Y < dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + acceleration;
					}
					else if (base.npc.velocity.Y > dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - acceleration;
					}
					if ((double)Math.Abs(dirY) < (double)speed * 0.2 && (((double)base.npc.velocity.X > 0.0 && (double)dirX < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)dirX > 0.0)))
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + acceleration * 2f;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - acceleration * 2f;
						}
					}
					if ((double)Math.Abs(dirX) < (double)speed * 0.2 && (((double)base.npc.velocity.Y > 0.0 && (double)dirY < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)dirY > 0.0)))
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + acceleration * 2f;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - acceleration * 2f;
						}
					}
				}
				else if (absDirX > absDirY)
				{
					if (base.npc.velocity.X < dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X + acceleration * 1.1f;
					}
					else if (base.npc.velocity.X > dirX)
					{
						base.npc.velocity.X = base.npc.velocity.X - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + acceleration;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - acceleration;
						}
					}
				}
				else
				{
					if (base.npc.velocity.Y < dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + acceleration * 1.1f;
					}
					else if (base.npc.velocity.Y > dirY)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - acceleration * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)speed * 0.5)
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + acceleration;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - acceleration;
						}
					}
				}
			}
			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
			if (collision)
			{
				if (base.npc.localAI[0] != 1f)
				{
					base.npc.netUpdate = true;
				}
				base.npc.localAI[0] = 1f;
			}
			else
			{
				if ((double)base.npc.localAI[0] != 0.0)
				{
					base.npc.netUpdate = true;
				}
				base.npc.localAI[0] = 0f;
			}
			if ((((double)base.npc.velocity.X > 0.0 && (double)base.npc.oldVelocity.X < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)base.npc.oldVelocity.X > 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)base.npc.oldVelocity.Y < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)base.npc.oldVelocity.Y > 0.0)) && !base.npc.justHit)
			{
				base.npc.netUpdate = true;
			}
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			Main.spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, null, drawColor, base.npc.rotation, origin, base.npc.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((RedeWorld.downedSoI && Main.hardMode) ? 0.01f : 0f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 750), true);
			}
			if (Main.rand.Next(14) == 0 || (Main.expertMode && Main.rand.Next(9) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(50, 100), true);
			}
		}
	}
}
