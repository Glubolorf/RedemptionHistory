using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	[AutoloadBossHead]
	public class StarWyvernHead : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Nebuleus/StarWyvernHead";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Star Serpent");
			NPCID.Sets.TechnicallyABoss[base.npc.type] = true;
			Main.npcFrameCount[base.npc.type] = 1;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.55f * bossLifeScale);
			base.npc.defense = (int)((float)base.npc.defense * 1.2f);
		}

		public override void SetDefaults()
		{
			base.npc.noTileCollide = true;
			base.npc.height = 54;
			base.npc.width = 54;
			base.npc.aiStyle = -1;
			base.npc.netAlways = true;
			base.npc.knockBackResist = 0f;
			base.npc.damage = 100;
			base.npc.defense = 25;
			base.npc.lifeMax = 450000;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.boss = true;
			base.npc.aiStyle = -1;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.behindTiles = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossStarGod1");
			base.npc.HitSound = SoundID.NPCHit56;
			base.npc.DeathSound = SoundID.NPCDeath60;
			for (int i = 0; i < base.npc.buffImmune.Length; i++)
			{
				base.npc.buffImmune[i] = true;
			}
			base.npc.buffImmune[103] = false;
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if (Main.netMode == 2 || Main.dedServ)
			{
				writer.Write(this.internalAI[0]);
				writer.Write(this.internalAI[1]);
				writer.Write(this.internalAI[2]);
				writer.Write(this.internalAI[3]);
			}
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if (Main.netMode == 1)
			{
				this.internalAI[0] = reader.ReadFloat();
				this.internalAI[1] = reader.ReadFloat();
				this.internalAI[2] = reader.ReadFloat();
				this.internalAI[3] = reader.ReadFloat();
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 2; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 58, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num].velocity *= 1.8f;
				}
			}
		}

		public override bool PreAI()
		{
			Player player = Main.player[base.npc.target];
			base.npc.Distance(player.Center);
			base.npc.spriteDirection = ((base.npc.velocity.X > 0f) ? -1 : 1);
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] >= 1200f)
			{
				base.npc.ai[1] = 0f;
			}
			base.npc.TargetClosest(true);
			if (!Main.player[base.npc.target].active || Main.player[base.npc.target].dead)
			{
				base.npc.TargetClosest(true);
				if (!Main.player[base.npc.target].active || Main.player[base.npc.target].dead)
				{
					base.npc.ai[3] += 1f;
					base.npc.velocity.Y = base.npc.velocity.Y + 0.11f;
					if (base.npc.ai[3] >= 300f)
					{
						base.npc.active = false;
					}
				}
				else
				{
					base.npc.ai[3] = 0f;
				}
			}
			for (int i = 0; i < 2; i++)
			{
				int num = Dust.NewDust(base.npc.position - new Vector2(8f, 8f), base.npc.width + 16, base.npc.height + 16, 242, 0f, 0f, 0, Color.Black, 0.2f);
				Main.dust[num].velocity *= 0f;
				Main.dust[num].noGravity = true;
			}
			if (Main.netMode != 1 && base.npc.ai[0] == 0f)
			{
				base.npc.realLife = base.npc.whoAmI;
				int num2 = base.npc.whoAmI;
				num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernNeck"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
				Main.npc[num2].realLife = base.npc.whoAmI;
				Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
				num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernCollar"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
				Main.npc[num2].realLife = base.npc.whoAmI;
				Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
				for (int j = 0; j < 8; j++)
				{
					num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernBody"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
					Main.npc[num2].realLife = base.npc.whoAmI;
					Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
					num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernBody"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
					Main.npc[num2].realLife = base.npc.whoAmI;
					Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
					num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernLeg"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
					Main.npc[num2].realLife = base.npc.whoAmI;
					Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
				}
				num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernTail1"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
				Main.npc[num2].realLife = base.npc.whoAmI;
				Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
				num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernTail2"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
				Main.npc[num2].realLife = base.npc.whoAmI;
				Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
				num2 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("StarWyvernTail3"), base.npc.whoAmI, 0f, (float)num2, 0f, 0f, 255);
				Main.npc[num2].realLife = base.npc.whoAmI;
				Main.npc[num2].ai[3] = (float)base.npc.whoAmI;
				base.npc.ai[0] = 1f;
				base.npc.netUpdate = true;
			}
			int num3 = (int)((double)base.npc.position.X / 16.0) - 1;
			int num4 = (int)((double)(base.npc.position.X + (float)base.npc.width) / 16.0) + 2;
			int num5 = (int)((double)base.npc.position.Y / 16.0) - 1;
			int num6 = (int)((double)(base.npc.position.Y + (float)base.npc.height) / 16.0) + 2;
			if (num3 < 0)
			{
			}
			if (num4 > Main.maxTilesX)
			{
				num4 = Main.maxTilesX;
			}
			if (num5 < 0)
			{
			}
			if (num6 > Main.maxTilesY)
			{
				num6 = Main.maxTilesY;
			}
			bool flag = true;
			float num7 = 19f;
			float num8 = 0.2f;
			Vector2 vector;
			vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float num9 = Main.player[base.npc.target].position.X + (float)(Main.player[base.npc.target].width / 2);
			float num10 = Main.player[base.npc.target].position.Y + (float)(Main.player[base.npc.target].height / 2);
			float num11 = (float)((int)((double)num9 / 16.0) * 16);
			float num12 = (float)((int)((double)num10 / 16.0) * 16);
			vector.X = (float)((int)((double)vector.X / 16.0) * 16);
			vector.Y = (float)((int)((double)vector.Y / 16.0) * 16);
			float num13 = num11 - vector.X;
			float num14 = num12 - vector.Y;
			float num15 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
			if (!flag)
			{
				base.npc.TargetClosest(true);
				base.npc.velocity.Y = base.npc.velocity.Y + 0.11f;
				if (base.npc.velocity.Y > num7)
				{
					base.npc.velocity.Y = num7;
				}
				if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num7 * 0.4)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X - num8 * 1.1f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X + num8 * 1.1f;
					}
				}
				else if (base.npc.velocity.Y == num7)
				{
					if (base.npc.velocity.X < num13)
					{
						base.npc.velocity.X = base.npc.velocity.X + num8;
					}
					else if (base.npc.velocity.X > num13)
					{
						base.npc.velocity.X = base.npc.velocity.X - num8;
					}
				}
				else if ((double)base.npc.velocity.Y > 4.0)
				{
					if ((double)base.npc.velocity.X < 0.0)
					{
						base.npc.velocity.X = base.npc.velocity.X + num8 * 0.9f;
					}
					else
					{
						base.npc.velocity.X = base.npc.velocity.X - num8 * 0.9f;
					}
				}
			}
			else
			{
				if (base.npc.soundDelay == 0)
				{
					float num16 = num15 / 40f;
					if ((double)num16 < 10.0)
					{
						num16 = 10f;
					}
					if ((double)num16 > 20.0)
					{
						num16 = 20f;
					}
					base.npc.soundDelay = (int)num16;
				}
				float num17 = Math.Abs(num13);
				float num18 = Math.Abs(num14);
				float num19 = num7 / num15;
				num13 *= num19;
				num14 *= num19;
				if (((double)base.npc.velocity.X > 0.0 && (double)num13 > 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)num13 < 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)num14 > 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)num14 < 0.0))
				{
					if (base.npc.velocity.X < num13)
					{
						base.npc.velocity.X = base.npc.velocity.X + num8;
					}
					else if (base.npc.velocity.X > num13)
					{
						base.npc.velocity.X = base.npc.velocity.X - num8;
					}
					if (base.npc.velocity.Y < num14)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + num8;
					}
					else if (base.npc.velocity.Y > num14)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - num8;
					}
					if ((double)Math.Abs(num14) < (double)num7 * 0.2 && (((double)base.npc.velocity.X > 0.0 && (double)num13 < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)num13 > 0.0)))
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + num8 * 2f;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - num8 * 2f;
						}
					}
					if ((double)Math.Abs(num13) < (double)num7 * 0.2 && (((double)base.npc.velocity.Y > 0.0 && (double)num14 < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)num14 > 0.0)))
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + num8 * 2f;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - num8 * 2f;
						}
					}
				}
				else if (num17 > num18)
				{
					if (base.npc.velocity.X < num13)
					{
						base.npc.velocity.X = base.npc.velocity.X + num8 * 1.1f;
					}
					else if (base.npc.velocity.X > num13)
					{
						base.npc.velocity.X = base.npc.velocity.X - num8 * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num7 * 0.5)
					{
						if ((double)base.npc.velocity.Y > 0.0)
						{
							base.npc.velocity.Y = base.npc.velocity.Y + num8;
						}
						else
						{
							base.npc.velocity.Y = base.npc.velocity.Y - num8;
						}
					}
				}
				else
				{
					if (base.npc.velocity.Y < num14)
					{
						base.npc.velocity.Y = base.npc.velocity.Y + num8 * 1.1f;
					}
					else if (base.npc.velocity.Y > num14)
					{
						base.npc.velocity.Y = base.npc.velocity.Y - num8 * 1.1f;
					}
					if ((double)(Math.Abs(base.npc.velocity.X) + Math.Abs(base.npc.velocity.Y)) < (double)num7 * 0.5)
					{
						if ((double)base.npc.velocity.X > 0.0)
						{
							base.npc.velocity.X = base.npc.velocity.X + num8;
						}
						else
						{
							base.npc.velocity.X = base.npc.velocity.X - num8;
						}
					}
				}
			}
			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
			if (Main.player[base.npc.target].dead || Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) > 6000f || Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 6000f)
			{
				base.npc.velocity.Y = base.npc.velocity.Y + 1f;
				if ((double)base.npc.position.Y > Main.rockLayer * 16.0)
				{
					base.npc.velocity.Y = base.npc.velocity.Y + 1f;
				}
				if ((double)base.npc.position.Y > Main.rockLayer * 16.0)
				{
					for (int k = 0; k < 200; k++)
					{
						if (Main.npc[k].aiStyle == base.npc.aiStyle)
						{
							Main.npc[k].active = false;
						}
					}
				}
			}
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

		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			spriteEffects = ((base.npc.spriteDirection == 1) ? 1 : 0);
		}

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = base.npc.rotation;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernHeadGlow");
			Texture2D texture2 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernNeckGlow");
			Texture2D texture3 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernCollarGlow");
			Texture2D texture4 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernBodyGlow");
			Texture2D texture5 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernLegGlow");
			Texture2D texture6 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernTail1Glow");
			Texture2D texture7 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernTail2Glow");
			Texture2D texture8 = base.mod.GetTexture("NPCs/Bosses/Nebuleus/StarWyvernTail3Glow");
			Vector2 vector = base.npc.Center - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY);
			int shaderIdFromItemId = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Texture2D texture9 = (base.npc.type == base.mod.NPCType<StarWyvernHead>()) ? texture : ((base.npc.type == base.mod.NPCType<StarWyvernNeck>()) ? texture2 : ((base.npc.type == base.mod.NPCType<StarWyvernCollar>()) ? texture3 : ((base.npc.type == base.mod.NPCType<StarWyvernBody>()) ? texture4 : ((base.npc.type == base.mod.NPCType<StarWyvernLeg>()) ? texture5 : ((base.npc.type == base.mod.NPCType<StarWyvernTail1>()) ? texture6 : ((base.npc.type == base.mod.NPCType<StarWyvernTail2>()) ? texture7 : texture8))))));
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			spriteBatch.Draw(texture2D, vector, new Rectangle?(base.npc.frame), base.npc.GetAlpha(drawColor), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
			BaseDrawing.DrawTexture(spriteBatch, texture9, shaderIdFromItemId, base.npc, new Color?(base.npc.GetAlpha(Color.White)), true, Utils.Size(base.npc.frame) / 2f);
			return false;
		}

		private int speed = 8;

		public float[] internalAI = new float[4];
	}
}
