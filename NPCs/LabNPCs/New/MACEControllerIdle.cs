using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class MACEControllerIdle : ModNPC
	{
		public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = base.npc.lifeMax;
			base.npc.damage = base.npc.damage;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crane Operator");
			Main.npcFrameCount[base.npc.type] = 10;
		}

		public override void SetDefaults()
		{
			base.npc.width = 82;
			base.npc.height = 56;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.aiStyle = -1;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = false;
			base.npc.noTileCollide = false;
			base.npc.dontTakeDamage = true;
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			ModContent.NPCType<MACEProjectHeadA>();
			if (base.npc.ai[0] == 0f)
			{
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 6.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 58;
					if (base.npc.ai[3] < 3f && base.npc.frame.Y >= 406)
					{
						base.npc.ai[3] += 1f;
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
					if (base.npc.frame.Y > 522)
					{
						if (!player.IsFullTBot())
						{
							base.npc.ai[0] += 1f;
							base.npc.ai[3] = 0f;
						}
						base.npc.ai[3] = 0f;
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
					}
				}
			}
			if (NPC.CountNPCS(ModContent.NPCType<MACEControllerIdle>()) >= 2 && Main.rand.Next(2) == 0)
			{
				base.npc.active = false;
			}
			Vector2 TheDoor = new Vector2(this.Origin.X + 1840f, this.Origin.Y + 2480f);
			if (base.npc.ai[0] == 1f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 7f)
				{
					base.npc.ai[0] += 1f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 2f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 4f)
				{
					if (!NPC.AnyNPCs(ModContent.NPCType<MACEProjectHeadA>()))
					{
						base.npc.ai[0] = 8f;
						base.npc.ai[3] = 0f;
					}
					else
					{
						base.npc.ai[3] = (float)Main.rand.Next(20);
						if (base.npc.ai[3] >= 16f)
						{
							base.npc.ai[0] = 3f;
						}
						else if (base.npc.ai[3] >= 12f && base.npc.ai[3] < 16f)
						{
							base.npc.ai[0] = 4f;
						}
						else if (base.npc.ai[3] == 11f)
						{
							base.npc.ai[0] = 5f;
						}
						else if (base.npc.ai[3] == 10f)
						{
							base.npc.ai[0] = 6f;
						}
						else if (base.npc.ai[3] == 9f)
						{
							base.npc.ai[0] = 7f;
						}
					}
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 3f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 3f)
				{
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 4f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 3f)
				{
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 5f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 3f)
				{
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 6f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 3f)
				{
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 7f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 12f)
				{
					base.npc.ai[0] = 2f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 8f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 3f)
				{
					if (base.npc.ai[3] >= 6f)
					{
						NPC npc2 = base.npc;
						npc2.position.X = npc2.position.X + 24f;
						base.npc.ai[0] = 9f;
					}
					base.npc.ai[3] += 1f;
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
			if (base.npc.ai[0] == 9f)
			{
				if (Vector2.Distance(base.npc.Center, TheDoor) < 32f)
				{
					base.npc.velocity *= 0f;
					for (int g = 0; g < 2; g++)
					{
						int goreIndex = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					}
					base.npc.noTileCollide = true;
					base.npc.noGravity = true;
					base.npc.velocity = new Vector2(0f, -800f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
				else
				{
					this.MoveToVector2(TheDoor);
				}
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] > 6f)
				{
					base.npc.ai[1] += 1f;
					base.npc.ai[2] = 0f;
				}
				if (base.npc.ai[1] >= 8f)
				{
					base.npc.ai[2] = 0f;
					base.npc.ai[1] = 0f;
				}
			}
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 1f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D startAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorStartle");
			Texture2D idleAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorClickButtons");
			Texture2D smashButtonsAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorSmashButtons");
			Texture2D smashLeftAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorSmashButtonsLeft");
			Texture2D smashRightAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorSmashButtonsRight");
			Texture2D kickAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorStepOnButtons");
			Texture2D headbuttAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorHeadbutt");
			Texture2D defeatAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorMACEDefeat");
			Texture2D backAwayAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneOperatorBackaway");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterA = new Vector2(base.npc.Center.X - 10f, base.npc.Center.Y - 4f);
			Vector2 drawCenterA2 = new Vector2(base.npc.Center.X - 8f, base.npc.Center.Y - 4f);
			Vector2 drawCenterA3 = new Vector2(base.npc.Center.X - 9f, base.npc.Center.Y - 4f);
			Vector2 drawCenterA4 = new Vector2(base.npc.Center.X - 7f, base.npc.Center.Y + 1f);
			Vector2 drawCenterB = new Vector2(base.npc.Center.X - 7f, base.npc.Center.Y - 10f);
			Vector2 drawCenterC = new Vector2(base.npc.Center.X - 6f, base.npc.Center.Y - 6f);
			if (base.npc.ai[0] == 1f)
			{
				int numG = startAni.Height / 7;
				int yG = numG * (int)base.npc.ai[1];
				spriteBatch.Draw(startAni, drawCenterC - Main.screenPosition, new Rectangle?(new Rectangle(0, yG, startAni.Width, numG)), drawColor, base.npc.rotation, new Vector2((float)startAni.Width / 2f, (float)numG / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 2f)
			{
				int numG2 = idleAni.Height / 4;
				int yG2 = numG2 * (int)base.npc.ai[1];
				spriteBatch.Draw(idleAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, yG2, idleAni.Width, numG2)), drawColor, base.npc.rotation, new Vector2((float)idleAni.Width / 2f, (float)numG2 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 3f)
			{
				int numG3 = smashButtonsAni.Height / 3;
				int yG3 = numG3 * (int)base.npc.ai[1];
				spriteBatch.Draw(smashButtonsAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, yG3, smashButtonsAni.Width, numG3)), drawColor, base.npc.rotation, new Vector2((float)smashButtonsAni.Width / 2f, (float)numG3 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 4f)
			{
				int numG4 = smashLeftAni.Height / 3;
				int yG4 = numG4 * (int)base.npc.ai[1];
				spriteBatch.Draw(smashLeftAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, yG4, smashLeftAni.Width, numG4)), drawColor, base.npc.rotation, new Vector2((float)smashLeftAni.Width / 2f, (float)numG4 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 5f)
			{
				int numG5 = smashRightAni.Height / 3;
				int yG5 = numG5 * (int)base.npc.ai[1];
				spriteBatch.Draw(smashRightAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, yG5, smashRightAni.Width, numG5)), drawColor, base.npc.rotation, new Vector2((float)smashRightAni.Width / 2f, (float)numG5 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 6f)
			{
				int numG6 = kickAni.Height / 3;
				int yG6 = numG6 * (int)base.npc.ai[1];
				spriteBatch.Draw(kickAni, drawCenterB - Main.screenPosition, new Rectangle?(new Rectangle(0, yG6, kickAni.Width, numG6)), drawColor, base.npc.rotation, new Vector2((float)kickAni.Width / 2f, (float)numG6 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 7f)
			{
				int numG7 = headbuttAni.Height / 12;
				int yG7 = numG7 * (int)base.npc.ai[1];
				spriteBatch.Draw(headbuttAni, drawCenterA2 - Main.screenPosition, new Rectangle?(new Rectangle(0, yG7, headbuttAni.Width, numG7)), drawColor, base.npc.rotation, new Vector2((float)headbuttAni.Width / 2f, (float)numG7 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 8f)
			{
				int numG8 = defeatAni.Height / 3;
				int yG8 = numG8 * (int)base.npc.ai[1];
				spriteBatch.Draw(defeatAni, drawCenterA3 - Main.screenPosition, new Rectangle?(new Rectangle(0, yG8, defeatAni.Width, numG8)), drawColor, base.npc.rotation, new Vector2((float)defeatAni.Width / 2f, (float)numG8 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 9f)
			{
				int numG9 = backAwayAni.Height / 8;
				int yG9 = numG9 * (int)base.npc.ai[1];
				spriteBatch.Draw(backAwayAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, yG9, backAwayAni.Width, numG9)), drawColor, base.npc.rotation, new Vector2((float)backAwayAni.Width / 2f, (float)numG9 / 2f), base.npc.scale, SpriteEffects.None, 0f);
			}
			if (base.npc.ai[0] == 0f)
			{
				spriteBatch.Draw(texture, drawCenterA4 - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public Vector2 Origin = new Vector2((float)((int)((float)Main.maxTilesX * 0.55f)), (float)((int)((float)Main.maxTilesY * 0.65f))) * 16f;
	}
}
