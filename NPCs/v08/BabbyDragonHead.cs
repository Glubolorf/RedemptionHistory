using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class BabbyDragonHead : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/v08/BabbyDragonHead";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Serpent");
			Main.npcFrameCount[base.npc.type] = 1;
		}

		public override void SetDefaults()
		{
			base.npc.height = 30;
			base.npc.width = 36;
			base.npc.aiStyle = -1;
			base.npc.netAlways = true;
			base.npc.damage = 95;
			base.npc.defense = 25;
			base.npc.lifeMax = 40000;
			base.npc.value = (float)Item.buyPrice(0, 5, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.behindTiles = true;
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
				int dust = Dust.NewDust(base.npc.position - new Vector2(8f, 8f), base.npc.width + 16, base.npc.height + 16, 242, 0f, 0f, 0, Color.Black, 0.2f);
				Main.dust[dust].velocity *= 0f;
				Main.dust[dust].noGravity = true;
			}
			if (Main.netMode != 1 && base.npc.ai[0] == 0f)
			{
				base.npc.realLife = base.npc.whoAmI;
				int latestNPC = base.npc.whoAmI;
				for (int j = 0; j < 4; j++)
				{
					latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<BabbyDragonBody>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
					Main.npc[latestNPC].realLife = base.npc.whoAmI;
					Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
					latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<BabbyDragonLeg>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
					Main.npc[latestNPC].realLife = base.npc.whoAmI;
					Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
				}
				latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<BabbyDragonTail1>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
				Main.npc[latestNPC].realLife = base.npc.whoAmI;
				Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
				latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<BabbyDragonTail2>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
				Main.npc[latestNPC].realLife = base.npc.whoAmI;
				Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
				base.npc.ai[0] = 1f;
				base.npc.netUpdate = true;
			}
			int minTilePosX = (int)((double)base.npc.position.X / 16.0) - 1;
			int maxTilePosX = (int)((double)(base.npc.position.X + (float)base.npc.width) / 16.0) + 2;
			int minTilePosY = (int)((double)base.npc.position.Y / 16.0) - 1;
			int num3 = (int)((double)(base.npc.position.Y + (float)base.npc.height) / 16.0) + 2;
			if (minTilePosX < 0)
			{
			}
			if (maxTilePosX > Main.maxTilesX)
			{
				maxTilePosX = Main.maxTilesX;
			}
			if (minTilePosY < 0)
			{
			}
			if (num3 > Main.maxTilesY)
			{
				int maxTilesY = Main.maxTilesY;
			}
			bool collision = true;
			float speed = 17f;
			float acceleration = 0.2f;
			Vector2 npcCenter = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float targetXPos = Main.player[base.npc.target].position.X + (float)(Main.player[base.npc.target].width / 2);
			double num4 = (double)(Main.player[base.npc.target].position.Y + (float)(Main.player[base.npc.target].height / 2));
			float targetRoundedPosX = (float)((int)((double)targetXPos / 16.0) * 16);
			float num5 = (float)((int)(num4 / 16.0) * 16);
			npcCenter.X = (float)((int)((double)npcCenter.X / 16.0) * 16);
			npcCenter.Y = (float)((int)((double)npcCenter.Y / 16.0) * 16);
			float dirX = targetRoundedPosX - npcCenter.X;
			float dirY = num5 - npcCenter.Y;
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
			if (Main.player[base.npc.target].dead || Math.Abs(base.npc.position.X - Main.player[base.npc.target].position.X) > 6000f || Math.Abs(base.npc.position.Y - Main.player[base.npc.target].position.Y) > 6000f)
			{
				base.npc.velocity.Y = base.npc.velocity.Y + 1f;
				if ((double)base.npc.position.Y > Main.rockLayer * 16.0)
				{
					base.npc.velocity.Y = base.npc.velocity.Y + 1f;
				}
				if ((double)base.npc.position.Y > Main.rockLayer * 16.0)
				{
					for (int num2 = 0; num2 < 200; num2++)
					{
						if (Main.npc[num2].aiStyle == base.npc.aiStyle)
						{
							Main.npc[num2].active = false;
						}
					}
				}
			}
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

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Sky.Chance * ((Main.hardMode && !NPC.AnyNPCs(ModContent.NPCType<BabbyDragonHead>()) && NPC.downedMoonlord && RedeWorld.downedPatientZero) ? 0.1f : 0f);
		}

		public float[] internalAI = new float[4];
	}
}
