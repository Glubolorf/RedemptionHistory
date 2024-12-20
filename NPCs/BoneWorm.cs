using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class BoneWorm : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/BoneWormHead";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Worm");
		}

		public override void SetDefaults()
		{
			base.npc.noTileCollide = true;
			base.npc.height = 34;
			base.npc.width = 34;
			base.npc.aiStyle = -1;
			base.npc.netAlways = true;
			base.npc.damage = 30;
			base.npc.defense = 25;
			base.npc.lifeMax = 100;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.behindTiles = true;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath5;
		}

		public override bool PreAI()
		{
			if (Main.netMode != 1 && base.npc.ai[0] == 0f)
			{
				base.npc.realLife = base.npc.whoAmI;
				int num = base.npc.whoAmI;
				int num2 = 0;
				int num3 = 4;
				for (int i = 0; i < num3; i++)
				{
					num = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("BoneWormBody"), base.npc.whoAmI, 0f, (float)num, 0f, 0f, 255);
					Main.npc[num].realLife = base.npc.whoAmI;
					Main.npc[num].ai[3] = (float)base.npc.whoAmI;
					num2++;
				}
				num = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("BoneWormTail"), base.npc.whoAmI, 0f, (float)num, 0f, 0f, 255);
				Main.npc[num].realLife = base.npc.whoAmI;
				Main.npc[num].ai[3] = (float)base.npc.whoAmI;
				base.npc.ai[0] = 1f;
				base.npc.netUpdate = true;
			}
			int num4 = (int)((double)base.npc.position.X / 16.0) - 1;
			int num5 = (int)((double)(base.npc.position.X + (float)base.npc.width) / 16.0) + 2;
			int num6 = (int)((double)base.npc.position.Y / 16.0) - 1;
			int num7 = (int)((double)(base.npc.position.Y + (float)base.npc.height) / 16.0) + 2;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			bool flag = false;
			for (int j = num4; j < num5; j++)
			{
				for (int k = num6; k < num7; k++)
				{
					if (Main.tile[j, k] != null && ((Main.tile[j, k].nactive() && (Main.tileSolid[(int)Main.tile[j, k].type] || (Main.tileSolidTop[(int)Main.tile[j, k].type] && Main.tile[j, k].frameY == 0))) || Main.tile[j, k].liquid > 64))
					{
						Vector2 vector;
						vector.X = (float)(j * 16);
						vector.Y = (float)(k * 16);
						if (base.npc.position.X + (float)base.npc.width > vector.X && (double)base.npc.position.X < (double)vector.X + 16.0 && (double)(base.npc.position.Y + (float)base.npc.height) > (double)vector.Y && (double)base.npc.position.Y < (double)vector.Y + 16.0)
						{
							flag = true;
							if (Main.rand.Next(100) == 0 && Main.tile[j, k].nactive())
							{
								WorldGen.KillTile(j, k, true, true, false);
							}
						}
					}
				}
			}
			if (!flag)
			{
				Rectangle rectangle;
				rectangle..ctor((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height);
				int num8 = 1000;
				bool flag2 = true;
				for (int l = 0; l < 255; l++)
				{
					if (Main.player[l].active)
					{
						Rectangle rectangle2;
						rectangle2..ctor((int)Main.player[l].position.X - num8, (int)Main.player[l].position.Y - num8, num8 * 2, num8 * 2);
						if (rectangle.Intersects(rectangle2))
						{
							flag2 = false;
							break;
						}
					}
				}
				if (flag2)
				{
					flag = true;
				}
			}
			float num9 = 7f;
			float num10 = 0.18f;
			Vector2 vector2;
			vector2..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float num11 = Main.player[base.npc.target].position.X + (float)(Main.player[base.npc.target].width / 2);
			float num12 = Main.player[base.npc.target].position.Y + (float)(Main.player[base.npc.target].height / 2);
			float num13 = (float)((int)((double)num11 / 16.0) * 16);
			float num14 = (float)((int)((double)num12 / 16.0) * 16);
			vector2.X = (float)((int)((double)vector2.X / 16.0) * 16);
			vector2.Y = (float)((int)((double)vector2.Y / 16.0) * 16);
			float num15 = num13 - vector2.X;
			float num16 = num14 - vector2.Y;
			float num17 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
			if (!flag)
			{
				base.npc.TargetClosest(true);
				base.npc.velocity.Y = base.npc.velocity.Y + 0.11f;
				if (base.npc.velocity.Y > num9)
				{
					base.npc.velocity.Y = num9;
				}
				if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num9 * 0.4)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X - num10 * 1.1f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X + num10 * 1.1f;
					}
				}
				else if (base.npc.velocity.Y == num9)
				{
					if (base.npc.velocity.X < num15)
					{
						base.npc.velocity.X = base.npc.velocity.X + num10;
					}
					else if (base.npc.velocity.X > num15)
					{
						base.npc.velocity.X = base.npc.velocity.X - num10;
					}
				}
				else if ((double)base.npc.velocity.Y > 4.0)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X + num10 * 0.9f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X - num10 * 0.9f;
					}
				}
			}
			else
			{
				if (base.npc.soundDelay == 0)
				{
					float num18 = num17 / 40f;
					if ((double)num18 < 10.0)
					{
						num18 = 10f;
					}
					if ((double)num18 > 20.0)
					{
						num18 = 20f;
					}
					base.npc.soundDelay = (int)num18;
					Main.PlaySound(15, (int)base.npc.position.X, (int)base.npc.position.Y, 1, 1f, 0f);
				}
				float num19 = Math.Abs(num15);
				float num20 = Math.Abs(num16);
				float num21 = num9 / num17;
				num15 *= num21;
				num16 *= num21;
				if (((double)base.npc.velocity.X > 0.0 && (double)num15 > 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)num15 < 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)num16 > 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)num16 < 0.0))
				{
					if (base.npc.velocity.X < num15)
					{
						base.npc.velocity.X = base.npc.velocity.X + num10;
					}
					else if (base.npc.velocity.X > num15)
					{
						base.npc.velocity.X = base.npc.velocity.X - num10;
					}
					if (base.npc.velocity.Y < num16)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + num10;
					}
					else if (base.npc.velocity.Y > num16)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - num10;
					}
					if ((double)Math.Abs(num16) < (double)num9 * 0.2 && (((double)base.npc.velocity.X > 0.0 && (double)num15 < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)num15 > 0.0)))
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + num10 * 2f;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - num10 * 2f;
						}
					}
					if ((double)Math.Abs(num15) < (double)num9 * 0.2 && (((double)base.npc.velocity.Y > 0.0 && (double)num16 < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)num16 > 0.0)))
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + num10 * 2f;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - num10 * 2f;
						}
					}
				}
				else if (num19 > num20)
				{
					if (base.npc.velocity.X < num15)
					{
						base.npc.velocity.X = base.npc.velocity.X + num10 * 1.1f;
					}
					else if (base.npc.velocity.X > num15)
					{
						base.npc.velocity.X = base.npc.velocity.X - num10 * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num9 * 0.5)
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + num10;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - num10;
						}
					}
				}
				else
				{
					if (base.npc.velocity.Y < num16)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + num10 * 1.1f;
					}
					else if (base.npc.velocity.Y > num16)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - num10 * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num9 * 0.5)
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + num10;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - num10;
						}
					}
				}
			}
			if (Main.player[base.npc.target].dead)
			{
				base.npc.velocity.Y = base.npc.velocity.Y + 1f;
				if ((double)base.npc.position.Y > Main.rockLayer * 16.0)
				{
					base.npc.velocity.Y = base.npc.velocity.Y + 1f;
				}
				if ((double)base.npc.position.Y > Main.rockLayer * 16.0)
				{
					for (int m = 0; m < 200; m++)
					{
						if (Main.npc[m].aiStyle == base.npc.aiStyle)
						{
							Main.npc[m].active = false;
						}
					}
				}
			}
			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
			if (flag)
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
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			int spriteDirection = base.npc.spriteDirection;
			spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/BoneWormHeadGore"), 1f);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).skeletonFriendly;
		}
	}
}
