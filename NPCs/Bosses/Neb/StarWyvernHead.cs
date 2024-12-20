using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	[AutoloadBossHead]
	public class StarWyvernHead : ModNPC
	{
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
			base.npc.lifeMax = 300000;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.boss = true;
			base.npc.aiStyle = -1;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.behindTiles = true;
			base.npc.HitSound = SoundID.NPCHit56;
			base.npc.DeathSound = SoundID.NPCDeath60;
			base.npc.alpha = 255;
			for (int i = 0; i < base.npc.buffImmune.Length; i++)
			{
				base.npc.buffImmune[i] = true;
			}
			base.npc.buffImmune[103] = false;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 58;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 2; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 58, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].velocity *= 1.8f;
				}
			}
		}

		public override bool PreAI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.alpha > 0)
			{
				base.npc.dontTakeDamage = true;
				base.npc.alpha -= 12;
				for (int spawnDust = 0; spawnDust < 2; spawnDust++)
				{
					int num935 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 242, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			else
			{
				base.npc.dontTakeDamage = false;
				base.npc.alpha = 0;
			}
			if (Main.netMode != 1 && base.npc.localAI[2] == 0f)
			{
				base.npc.realLife = base.npc.whoAmI;
				int latestNPC = base.npc.whoAmI;
				int[] Frame = new int[]
				{
					3,
					1,
					0,
					0,
					2,
					0,
					0,
					2,
					0,
					0,
					2,
					0,
					0,
					2,
					0,
					0,
					2,
					0,
					0,
					2,
					4,
					5,
					6
				};
				for (int i = 0; i < Frame.Length; i++)
				{
					latestNPC = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<StarWyvernBody>(), base.npc.whoAmI, 0f, (float)latestNPC, 0f, 0f, 255);
					Main.npc[latestNPC].realLife = base.npc.whoAmI;
					Main.npc[latestNPC].ai[3] = (float)base.npc.whoAmI;
					Main.npc[latestNPC].netUpdate = true;
					Main.npc[latestNPC].ai[2] = (float)Frame[i];
				}
				base.npc.localAI[2] = 1f;
				base.npc.netUpdate2 = true;
			}
			bool collision = true;
			Vector2 targetPos;
			float num938;
			switch ((int)base.npc.ai[0])
			{
			case 0:
				break;
			case 1:
			{
				if (!base.npc.HasPlayerTarget)
				{
					base.npc.TargetClosest(true);
				}
				targetPos = player.Center;
				this.MovementWorm(targetPos, 36f, 0.06f);
				if (base.npc.ai[1] < 10f)
				{
					this.MovementWorm(targetPos, 20f, 0.8f);
				}
				if (base.npc.ai[1] % 5f == 0f)
				{
					base.npc.Shoot(base.npc.Center, ModContent.ProjectileType<LingeringStar>(), 120, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", 0f, 0f);
				}
				float[] ai = base.npc.ai;
				int num937 = 1;
				num938 = ai[num937] + 1f;
				ai[num937] = num938;
				if (num938 > 80f)
				{
					base.npc.ai[0] += 1f;
					base.npc.ai[1] = 0f;
					base.npc.netUpdate = true;
					goto IL_541;
				}
				goto IL_541;
			}
			case 2:
			{
				if (!base.npc.HasPlayerTarget)
				{
					base.npc.TargetClosest(true);
				}
				targetPos = player.Center;
				this.MovementWorm(targetPos, 13f, 0.5f);
				float[] ai2 = base.npc.ai;
				int num939 = 1;
				num938 = ai2[num939] + 1f;
				ai2[num939] = num938;
				if (num938 > 60f)
				{
					if (base.npc.ai[2] < 2f)
					{
						Main.PlaySound(29, base.npc.Center, 92);
						base.npc.ai[0] = 1f;
						base.npc.ai[1] = 0f;
						base.npc.ai[2] += 1f;
					}
					else
					{
						base.npc.ai[0] = 0f;
						base.npc.ai[1] = 0f;
						base.npc.ai[2] = 0f;
					}
					base.npc.netUpdate = true;
					goto IL_541;
				}
				goto IL_541;
			}
			default:
				base.npc.ai[0] = 0f;
				break;
			}
			if (!base.npc.HasPlayerTarget)
			{
				base.npc.TargetClosest(true);
			}
			targetPos = player.Center;
			this.MovementWorm(targetPos, 19f, 0.2f);
			float[] ai3 = base.npc.ai;
			int num940 = 1;
			num938 = ai3[num940] + 1f;
			ai3[num940] = num938;
			if (num938 > 240f)
			{
				Main.PlaySound(29, base.npc.Center, 92);
				base.npc.ai[0] += 1f;
				base.npc.ai[1] = 0f;
				base.npc.ai[2] = 0f;
				base.npc.netUpdate = true;
			}
			IL_541:
			base.npc.rotation = (float)Math.Atan2((double)base.npc.velocity.Y, (double)base.npc.velocity.X) + 1.57f;
			if (base.npc.velocity.X < 0f)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
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
					for (int num936 = 0; num936 < 200; num936++)
					{
						if (Main.npc[num936].aiStyle == base.npc.aiStyle)
						{
							Main.npc[num936].active = false;
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
			if ((((double)base.npc.velocity.X > 0.0 && (double)base.npc.oldVelocity.X < 0.0) || ((double)base.npc.velocity.X < 0.0 && (double)base.npc.oldVelocity.X > 0.0) || ((double)base.npc.velocity.Y > 0.0 && (double)base.npc.oldVelocity.Y < 0.0) || ((double)base.npc.velocity.Y < 0.0 && (double)base.npc.oldVelocity.Y > 0.0)) && !base.npc.justHit)
			{
				base.npc.netUpdate = true;
			}
			return false;
		}

		public void MovementWorm(Vector2 target, float speed, float acceleration)
		{
			Vector2 npcCenter = base.npc.Center;
			float x = target.X;
			float targetRoundedPosY = target.Y;
			float dirX = x - npcCenter.X;
			float dirY = targetRoundedPosY - npcCenter.Y;
			base.npc.TargetClosest(true);
			float length = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));
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
						return;
					}
					base.npc.velocity.X = base.npc.velocity.X - acceleration * 2f;
					return;
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
						return;
					}
					base.npc.velocity.Y = base.npc.velocity.Y - acceleration;
					return;
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
						return;
					}
					base.npc.velocity.X = base.npc.velocity.X - acceleration;
				}
			}
		}

		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
		{
			spriteEffects = ((base.npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		}

		public override void BossHeadRotation(ref float rotation)
		{
			rotation = base.npc.rotation;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			this.StarWyvernTex = Main.npcTexture[base.npc.type];
			if (base.npc.type == ModContent.NPCType<StarWyvernHead>())
			{
				this.StarWyvernTex = base.mod.GetTexture("NPCs/Bosses/Neb/StarWyvernHead");
			}
			Texture2D glowTex = base.mod.GetTexture("NPCs/Bosses/Neb/StarWyvernHeadGlow");
			Texture2D glowTex2 = base.mod.GetTexture("NPCs/Bosses/Neb/StarWyvernBody_Glow");
			Texture2D wingTex = base.mod.GetTexture("NPCs/Bosses/Neb/StarWyvern_Wing");
			Texture2D wingGlow = base.mod.GetTexture("NPCs/Bosses/Neb/StarWyvern_Wing_Glow");
			Vector2 WingPos = base.npc.Center - Main.screenPosition + new Vector2(0f, base.npc.gfxOffY);
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			int shader = GameShaders.Armor.GetShaderIdFromItemId(2870);
			Texture2D myGlowTex = (base.npc.type == ModContent.NPCType<StarWyvernHead>()) ? glowTex : glowTex2;
			BaseDrawing.DrawTexture(spriteBatch, this.StarWyvernTex, 0, base.npc.position, base.npc.width, base.npc.height, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, (base.npc.type == ModContent.NPCType<StarWyvernHead>()) ? 1 : 7, base.npc.frame, new Color?(base.npc.GetAlpha(drawColor)), true, default(Vector2));
			if (base.npc.type == ModContent.NPCType<StarWyvernBody>() && base.npc.frame.Y == 168)
			{
				spriteBatch.Draw(wingTex, WingPos, new Rectangle?(new Rectangle(0, 0, wingTex.Width, wingTex.Height)), base.npc.GetAlpha(drawColor), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, base.npc.position, base.npc.width, base.npc.height, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, (base.npc.type == ModContent.NPCType<StarWyvernHead>()) ? 1 : 7, base.npc.frame, new Color?(base.npc.GetAlpha(Color.White)), true, default(Vector2));
			if (base.npc.type == ModContent.NPCType<StarWyvernBody>() && base.npc.frame.Y == 168)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
				GameShaders.Armor.ApplySecondary(shader, Main.player[Main.myPlayer], null);
				spriteBatch.Draw(wingGlow, WingPos, new Rectangle?(new Rectangle(0, 0, wingTex.Width, wingTex.Height)), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			}
			return false;
		}

		public Texture2D StarWyvernTex;
	}
}
