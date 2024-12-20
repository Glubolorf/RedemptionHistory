﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KSBeamCell : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Beam Cell");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 3600;
			base.projectile.tileCollide = false;
		}

		public float LaserLength
		{
			get
			{
				return base.projectile.localAI[1];
			}
			set
			{
				base.projectile.localAI[1] = value;
			}
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(this.attackCounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			this.attackCounter = reader.ReadSingle();
		}

		public override void AI()
		{
			int boss = (int)base.projectile.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != ModContent.NPCType<KS3_Body>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 CellPos = new Vector2((npc2.spriteDirection == 1) ? (npc2.Center.X + 2f) : (npc2.Center.X - 2f), npc2.Center.Y - 16f);
			base.projectile.Center = CellPos;
			ref this.rot.SlowRotation((npc2.spriteDirection == 1) ? Utils.ToRotation(base.projectile.DirectionTo(Main.player[npc2.target].Center)) : (-Utils.ToRotation(base.projectile.DirectionTo(Main.player[npc2.target].Center))), 0.010471975f);
			base.projectile.velocity = Utils.ToRotationVector2(this.rot);
			if (npc2.spriteDirection == 1)
			{
				base.projectile.velocity *= 1f;
			}
			else
			{
				base.projectile.velocity *= -1f;
			}
			if (this.attackCounter == 0f)
			{
				if (base.projectile.ai[1] == 0f && !Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BallFire").WithVolume(0.9f).WithPitchVariance(0f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
				}
				base.projectile.ai[1] = 5f;
			}
			else if (this.attackCounter >= 30f)
			{
				base.projectile.ai[1] += 5f * (float)this.multiplier;
			}
			this.attackCounter += 1f;
			if (base.projectile.ai[1] == 30f)
			{
				base.projectile.hostile = true;
				this.shoot = 1;
			}
			if (base.projectile.ai[1] >= 230f && this.multiplier == 1)
			{
				this.multiplier = -1;
			}
			if (this.multiplier == -1 && base.projectile.ai[1] <= 0f)
			{
				base.projectile.Kill();
			}
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) - 1.5707964f;
			base.projectile.velocity = Vector2.Normalize(base.projectile.velocity);
			float[] sampleArray = new float[2];
			Collision.LaserScan(base.projectile.Center, base.projectile.velocity, 0f, 3000f, sampleArray);
			float sampledLength = 0f;
			for (int i = 0; i < sampleArray.Length; i++)
			{
				sampledLength += sampleArray[i];
			}
			sampledLength /= (float)sampleArray.Length;
			float amount = 0.75f;
			this.LaserLength = MathHelper.Lerp(this.LaserLength, sampledLength, amount);
			this.LaserLength = 3000f;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float collisionPoint = 0f;
			return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + base.projectile.velocity * this.LaserLength, (float)projHitbox.Width, ref collisionPoint));
		}

		public override bool? CanCutTiles()
		{
			DelegateMethods.tilecut_0 = 2;
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + base.projectile.velocity * this.LaserLength, (float)base.projectile.width * base.projectile.scale * 2f, new Utils.PerLinePoint(this.CutTilesAndBreakWalls));
			return new bool?(true);
		}

		private bool CutTilesAndBreakWalls(int x, int y)
		{
			return DelegateMethods.CutTiles(x, y);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			NPC npc = Main.npc[(int)base.projectile.ai[0]];
			if (base.projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			Texture2D texture2D19 = Main.projectileTexture[base.projectile.type];
			Texture2D texture2D20 = base.mod.GetTexture("NPCs/Bosses/KSIII/KSBeamCell_Beam");
			Texture2D texture2D21 = base.mod.GetTexture("NPCs/Bosses/KSIII/KSBeamCell_End");
			float num228 = this.LaserLength;
			Color color44 = Color.White * 0.8f;
			Texture2D arg_AF99_ = texture2D19;
			Vector2 arg_AF99_2 = base.projectile.Center + new Vector2(0f, base.projectile.gfxOffY) - Main.screenPosition;
			spriteBatch.Draw(arg_AF99_, arg_AF99_2, null, color44, base.projectile.rotation, Utils.Size(texture2D19) / 2f, new Vector2(Math.Min(base.projectile.ai[1], 30f) / 30f, 1f), SpriteEffects.None, 0f);
			num228 -= (float)(texture2D19.Height / 2 + texture2D21.Height) * base.projectile.scale;
			Vector2 value20 = base.projectile.Center + new Vector2(0f, base.projectile.gfxOffY);
			value20 += base.projectile.velocity * base.projectile.scale * (float)texture2D19.Height / 2f;
			if (num228 > 0f)
			{
				float num229 = 0f;
				Rectangle rectangle7 = new Rectangle(0, 16 * (base.projectile.timeLeft / 3 % 5), texture2D20.Width, 16);
				while (num229 + 1f < num228)
				{
					if (num228 - num229 < (float)rectangle7.Height)
					{
						rectangle7.Height = (int)(num228 - num229);
					}
					Main.spriteBatch.Draw(texture2D20, value20 - Main.screenPosition, new Rectangle?(rectangle7), color44, base.projectile.rotation, new Vector2((float)(rectangle7.Width / 2), 0f), new Vector2(Math.Min(base.projectile.ai[1], 30f) / 30f, 1f), SpriteEffects.None, 0f);
					num229 += (float)rectangle7.Height * base.projectile.scale;
					value20 += base.projectile.velocity * (float)rectangle7.Height * base.projectile.scale;
					rectangle7.Y += 16;
					if (rectangle7.Y + rectangle7.Height > texture2D20.Height)
					{
						rectangle7.Y = 0;
					}
				}
			}
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D arg_B1FF_ = texture2D21;
			Vector2 arg_B1FF_2 = value20 - Main.screenPosition;
			spriteBatch2.Draw(arg_B1FF_, arg_B1FF_2, null, color44, base.projectile.rotation, Utils.Top(Utils.Frame(texture2D21, 1, 1, 0, 0)), new Vector2(Math.Min(base.projectile.ai[1], 30f) / 30f, 1f), SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		internal const float charge = 30f;

		public const float LaserLengthMax = 3000f;

		private int multiplier = 1;

		public int shoot;

		public float rot;

		private float attackCounter;
	}
}
