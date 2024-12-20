using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	[AutoloadBossHead]
	public class WardenIdle_Fake : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Warden/WardenIdle";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Warden");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 300000;
			base.npc.damage = 150;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 72;
			base.npc.height = 102;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.alpha = 255;
			base.npc.dontTakeDamage = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit49;
			base.npc.DeathSound = SoundID.NPCDeath51;
			base.npc.chaseable = false;
			for (int i = 0; i < base.npc.buffImmune.Length; i++)
			{
				base.npc.buffImmune[i] = true;
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override bool CheckActive()
		{
			NPC real = Main.npc[(int)base.npc.ai[0]];
			Player player = Main.player[base.npc.target];
			return !player.active || player.dead || (real.ai[0] != 7f && real.ai[0] != 31f) || real.ai[1] >= 3f || Vector2.Distance(base.npc.Center, player.Center) > 3000f;
		}

		private void DespawnHandler()
		{
			Player player = Main.player[base.npc.target];
			if (!player.active || player.dead)
			{
				base.npc.alpha += 10;
				if (base.npc.alpha >= 255)
				{
					base.npc.velocity = new Vector2(0f, -20f);
				}
				if (base.npc.timeLeft > 10)
				{
					base.npc.timeLeft = 10;
				}
				return;
			}
		}

		public override void AI()
		{
			if (this.floatTimer == 0)
			{
				NPC npc = base.npc;
				npc.velocity.Y = npc.velocity.Y + 0.007f;
				if (base.npc.velocity.Y > 0.3f)
				{
					this.floatTimer = 1;
					base.npc.netUpdate = true;
				}
			}
			else if (this.floatTimer == 1)
			{
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.007f;
				if (base.npc.velocity.Y < -0.3f)
				{
					this.floatTimer = 0;
					base.npc.netUpdate = true;
				}
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc3 = base.npc;
				npc3.frame.Y = npc3.frame.Y + 108;
				if (base.npc.frame.Y >= 432)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.DespawnHandler();
			int boss = (int)base.npc.ai[0];
			if (boss < 0 || boss >= 200 || Main.npc[boss].type != ModContent.NPCType<WardenIdle>())
			{
				base.npc.active = false;
			}
			NPC real = Main.npc[(int)base.npc.ai[0]];
			base.npc.lifeMax = real.lifeMax;
			base.npc.life = real.life;
			base.npc.defense = real.defense;
			if ((real.ai[0] != 7f && real.ai[0] != 31f) || real.ai[1] >= 3f)
			{
				base.npc.dontTakeDamage = true;
				base.npc.alpha += 10;
				if (base.npc.alpha >= 255)
				{
					base.npc.active = false;
				}
				return;
			}
			if (base.npc.alpha <= 0)
			{
				base.npc.dontTakeDamage = false;
			}
			else
			{
				base.npc.dontTakeDamage = true;
			}
			float obj = base.npc.ai[2];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					base.npc.LookAtPlayer();
					base.npc.alpha -= 2;
					if (base.npc.alpha <= 0)
					{
						base.npc.ai[2] = 2f;
						base.npc.netUpdate = true;
					}
				}
			}
			else
			{
				if (real.ai[0] == 31f)
				{
					while (base.npc.ai[3] == real.ai[3] || base.npc.ai[3] == 0f)
					{
						base.npc.ai[3] = (float)Main.rand.Next(1, 15);
					}
				}
				else
				{
					base.npc.ai[3] = (float)Main.rand.Next(7);
				}
				base.npc.Shoot(new Vector2(base.npc.Center.X, base.npc.Center.Y), ModContent.ProjectileType<WardenBall_Fake>(), 0, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)base.npc.whoAmI, 0f);
				base.npc.ai[2] = 1f;
				base.npc.netUpdate = true;
			}
			for (int p = 0; p < 200; p++)
			{
				this.clearCheck = Main.npc[p];
				if (this.clearCheck.active && this.clearCheck.whoAmI != base.npc.whoAmI && (this.clearCheck.type == ModContent.NPCType<WardenIdle>() || this.clearCheck.type == ModContent.NPCType<WardenIdle_Fake>()) && base.npc.Hitbox.Intersects(this.clearCheck.Hitbox))
				{
					base.npc.Center = this.PosPick();
					base.npc.ai[1] = 1f;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			NPC real = Main.npc[(int)base.npc.ai[0]];
			if (real.ai[1] < 3f && real.ai[0] == 7f)
			{
				real.ai[1] = 3f;
			}
			if (real.ai[1] < 3f && real.ai[0] == 31f)
			{
				real.ai[1] = 3f;
			}
		}

		public Vector2 PosPick()
		{
			Vector2[] pickArray = new Vector2[]
			{
				new Vector2(1064f, 1184f),
				new Vector2(1033f, 1202f),
				new Vector2(1069f, 1215f),
				new Vector2(1166f, 1203f),
				new Vector2(1135f, 1184f),
				new Vector2(1133f, 1217f),
				new Vector2(1131f, 1253f),
				new Vector2(1168f, 1268f),
				new Vector2(1132f, 1290f),
				new Vector2(1037f, 1270f),
				new Vector2(1069f, 1290f),
				new Vector2(1067f, 1252f)
			};
			return pickArray[Main.rand.Next(Enumerable.Count<Vector2>(pickArray))] * 16f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			NPC npc = Main.npc[(int)base.npc.ai[0]];
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D faces = base.mod.GetTexture("NPCs/Bosses/Warden/WardenFalseFaces");
			Texture2D faces2 = base.mod.GetTexture("NPCs/Bosses/Warden/WardenFalseFaces2");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			new Vector2(base.npc.Center.X, base.npc.Center.Y);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			if ((npc.modNPC as WardenIdle).finalPhase)
			{
				if (this.auraDirection)
				{
					this.auraPercent += 0.1f;
					this.auraDirection = (this.auraPercent < 1f);
				}
				else
				{
					this.auraPercent -= 0.1f;
					this.auraDirection = (this.auraPercent <= 0f);
				}
				BaseDrawing.DrawAura(spriteBatch, Main.npcTexture[base.npc.type], 0, base.npc.position, base.npc.width, base.npc.height, this.auraPercent, 4f, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, 4, base.npc.frame, 0f, 0f, new Color?(RedeColor.COLOR_GLOWPULSE));
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			if ((npc.modNPC as WardenIdle).finalPhase)
			{
				Vector2 maskCenter = new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 5f) : (base.npc.Center.X - 5f), base.npc.Center.Y - 35f);
				int num214 = faces2.Height / 14;
				int y6 = num214 * ((int)base.npc.ai[3] - 1);
				Main.spriteBatch.Draw(faces2, maskCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, faces2.Width, num214)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)faces2.Width / 2f, (float)num214 / 2f), base.npc.scale, effects, 0f);
			}
			else
			{
				Vector2 maskCenter2 = new Vector2((base.npc.spriteDirection == 1) ? (base.npc.Center.X + 5f) : (base.npc.Center.X - 5f), base.npc.Center.Y - 35f);
				int num215 = faces.Height / 7;
				int y7 = num215 * (int)base.npc.ai[3];
				Main.spriteBatch.Draw(faces, maskCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, faces.Width, num215)), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, new Vector2((float)faces.Width / 2f, (float)num215 / 2f), base.npc.scale, effects, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public Vector2 MoveVector2;

		public Vector2 vector;

		public int floatTimer;

		public int frameCounters;

		private NPC clearCheck;

		public float auraPercent;

		public bool auraDirection = true;
	}
}
