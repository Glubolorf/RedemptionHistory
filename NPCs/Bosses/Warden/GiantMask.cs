using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class GiantMask : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Mask");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 138;
			base.projectile.height = 184;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 1800;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			Player target = Main.player[base.projectile.GetNearestAlivePlayer()];
			NPC host = Main.npc[(int)base.projectile.ai[0]];
			if (!host.active || host.type != ModContent.NPCType<WardenIdle>())
			{
				base.projectile.Kill();
			}
			float obj = base.projectile.ai[1];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (2f.Equals(obj))
					{
						base.projectile.hostile = true;
						Projectile projectile = base.projectile;
						int num = projectile.frameCounter + 1;
						projectile.frameCounter = num;
						if (num >= 15)
						{
							base.projectile.frameCounter = 0;
							Projectile projectile2 = base.projectile;
							num = projectile2.frame + 1;
							projectile2.frame = num;
							if (num >= 2)
							{
								base.projectile.frame = 0;
								Main.PlaySound(SoundID.NPCHit48, base.projectile.position);
							}
						}
						if ((base.projectile.Center.X > 19168f && base.projectile.velocity.X > 0f) || (base.projectile.Center.X < 16064f && base.projectile.velocity.X < 0f))
						{
							Projectile projectile3 = base.projectile;
							projectile3.velocity.X = projectile3.velocity.X * -1f;
						}
						if ((base.projectile.Center.Y < 18672f && base.projectile.velocity.Y < 0f) || (base.projectile.Center.Y > 21264f && base.projectile.velocity.Y > 0f))
						{
							Projectile projectile4 = base.projectile;
							projectile4.velocity.Y = projectile4.velocity.Y * -1f;
						}
					}
				}
				else
				{
					this.maskAlpha -= 5;
					base.projectile.localAI[0] += 1f;
					base.projectile.localAI[0] *= 1.02f;
					if (base.projectile.localAI[1] < 100f)
					{
						base.projectile.localAI[1] += 1f;
					}
					for (int i = 0; i < 5; i++)
					{
						this.maskPos[i] = base.projectile.Center + Utils.RotatedBy(Vector2.One, (double)MathHelper.ToRadians((float)(72 * i) + base.projectile.localAI[0]), default(Vector2)) * base.projectile.localAI[1];
					}
					if (this.maskAlpha <= 0)
					{
						base.projectile.alpha -= 4;
						if (base.projectile.scale < 1f)
						{
							base.projectile.scale += 0.05f;
						}
					}
					if (base.projectile.alpha < 0 && base.projectile.scale >= 1f)
					{
						for (int j = 0; j < 5; j++)
						{
							for (int k = 0; k < 16; k++)
							{
								int dustID = Dust.NewDust(new Vector2(this.maskPos[j].X - 1f, this.maskPos[j].Y - 1f), 2, 2, 261, 0f, 0f, 100, Color.White, 1f);
								Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(3f, 0f), (float)k / 16f * 6.28f);
								Main.dust[dustID].noLight = false;
								Main.dust[dustID].noGravity = true;
							}
						}
						this.maskAlpha = 255;
						base.projectile.alpha = 0;
						base.projectile.scale = 1f;
						base.projectile.ai[1] = 2f;
						base.projectile.velocity = base.projectile.DirectionTo(target.Center) * 6f;
					}
				}
			}
			else
			{
				base.projectile.scale = 0.01f;
				base.projectile.ai[1] = 1f;
			}
			if (base.projectile.timeLeft < 60)
			{
				base.projectile.hostile = false;
				base.projectile.alpha += 5;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
			{
				lightColor,
				Color.GhostWhite,
				lightColor
			}));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D masks = base.mod.GetTexture("NPCs/Bosses/Warden/WardenFacesFront");
			int height = texture.Height / 2;
			int y = height * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y, texture.Width, height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)height / 2f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			for (int i = 0; i < base.projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.projectile.oldPos[i];
				Color color = base.projectile.GetAlpha(Color.White) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
				spriteBatch.Draw(texture, drawPos + base.projectile.Size / 2f - Main.screenPosition, new Rectangle?(rect), color * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			for (int j = 0; j < 5; j++)
			{
				int height2 = masks.Height / 5;
				Vector2 origin2 = new Vector2((float)(masks.Width / 2), (float)(height2 / 2));
				this.maskFrame[j] = j;
				spriteBatch.Draw(masks, this.maskPos[j] - Main.screenPosition, new Rectangle?(new Rectangle(0, height2 * this.maskFrame[j], masks.Width, height2)), drawColor * ((float)(255 - this.maskAlpha) / 255f), base.projectile.rotation, origin2, 1f, SpriteEffects.None, 0f);
			}
			return false;
		}

		public Vector2[] maskPos = new Vector2[5];

		public int[] maskFrame = new int[5];

		public int maskAlpha = 255;
	}
}
