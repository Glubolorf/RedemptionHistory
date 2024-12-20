using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class PlayerSoulPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/ExtraTextures/WhiteOrb";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul Fragment");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 600;
			base.projectile.alpha = 255;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			Player player = Main.player[(int)base.projectile.ai[1]];
			float obj = base.projectile.ai[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (2f.Equals(obj))
					{
						for (int i = 0; i < 6; i++)
						{
							double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
							Vector2 dustVector;
							dustVector.X = (float)(Math.Sin(angle) * 50.0);
							dustVector.Y = (float)(Math.Cos(angle) * 50.0);
							Dust dust2 = Main.dust[Dust.NewDust(base.projectile.Center + dustVector, 2, 2, 261, 0f, 0f, 100, default(Color), 1f)];
							dust2.noGravity = true;
							dust2.velocity = base.projectile.DirectionTo(dust2.position) * 6f;
						}
						if (Main.netMode != 1)
						{
							int j = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, ModContent.NPCType<CagedSoul>(), 0, (float)player.whoAmI, 0f, 0f, 0f, 255);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(23, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
							}
						}
						base.projectile.Kill();
					}
				}
				else
				{
					if (this.speed < 30f)
					{
						this.speed *= 1.01f;
					}
					base.projectile.Move(this.vector, this.speed, 1f, false);
					if (base.projectile.Distance(this.vector) < 20f)
					{
						base.projectile.localAI[0] += 1f;
						if (base.projectile.localAI[0] > 60f)
						{
							base.projectile.velocity *= 0f;
							base.projectile.ai[0] = 2f;
						}
					}
					else
					{
						base.projectile.localAI[0] = 0f;
					}
				}
			}
			else
			{
				base.projectile.alpha -= 5;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.ai[0] = 1f;
					this.vector = this.PosPick();
				}
			}
			if (base.projectile.ai[0] != 1f)
			{
				return;
			}
			for (int p = 0; p < 1000; p++)
			{
				Projectile clearCheck = Main.projectile[p];
				if (clearCheck.active && clearCheck.whoAmI != base.projectile.whoAmI && clearCheck.type == ModContent.ProjectileType<PlayerSoulPro>() && base.projectile.Hitbox.Intersects(clearCheck.Hitbox))
				{
					base.projectile.position += RedeHelper.PolarVector(5f, Utils.NextFloat(Main.rand, 0f, 6.2831855f));
					this.vector = this.PosPick();
				}
			}
			for (int p2 = 0; p2 < 200; p2++)
			{
				NPC clearCheck2 = Main.npc[p2];
				if (clearCheck2.active && clearCheck2.type == ModContent.NPCType<CagedSoul>() && base.projectile.Hitbox.Intersects(clearCheck2.Hitbox))
				{
					base.projectile.position += RedeHelper.PolarVector(5f, Utils.NextFloat(Main.rand, 0f, 6.2831855f));
					this.vector = this.PosPick();
				}
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

		public override void Kill(int timeLeft)
		{
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i];
				Color color = base.projectile.GetAlpha(Color.White) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(texture, drawPos + base.projectile.Size / 2f - Main.screenPosition, new Rectangle?(rect), color * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public Vector2 vector;

		public float speed = 10f;
	}
}
