using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenDeath : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Warden/WardenFacesFront";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Warden");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 24;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.penetrate = -1;
			base.projectile.ignoreWater = true;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			base.projectile.frame = (int)base.projectile.ai[0];
			float obj = base.projectile.ai[1];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (!3f.Equals(obj))
						{
							return;
						}
						Projectile projectile = base.projectile;
						projectile.velocity.Y = projectile.velocity.Y - 0.5f;
						base.projectile.localAI[0] += 1f;
						if (base.projectile.localAI[0] > 300f)
						{
							base.projectile.Kill();
						}
					}
					else
					{
						base.projectile.Move(this.origin, 5f, 1f, false);
						this.glowAlpha -= 4;
						this.teleGlow = true;
						if (this.teleGlowTimer > 40f)
						{
							this.teleGlowTimer -= 1f;
						}
						if (this.teleGlowTimer <= 40f)
						{
							base.projectile.velocity.X = 0f;
							base.projectile.velocity.Y = 4f;
							base.projectile.ai[1] = 3f;
							return;
						}
					}
				}
				else
				{
					base.projectile.velocity *= 0.94f;
					base.projectile.localAI[0] += 1f;
					if (base.projectile.localAI[0] > 120f)
					{
						base.projectile.ai[1] = 2f;
						base.projectile.localAI[0] = 0f;
						return;
					}
				}
				return;
			}
			this.origin = base.projectile.Center;
			base.projectile.ai[1] = 1f;
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
			Texture2D white = base.mod.GetTexture("NPCs/Bosses/Warden/WardenFacesFront_White");
			int height = texture.Height / 5;
			int y = height * base.projectile.frame;
			Vector2 position = base.projectile.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, y, texture.Width, height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)height / 2f);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), drawColor * ((float)(255 - base.projectile.alpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(white, position, new Rectangle?(rect), base.projectile.GetAlpha(Color.White) * ((float)(255 - this.glowAlpha) / 255f), base.projectile.rotation, origin, base.projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			Texture2D teleportGlow = base.mod.GetTexture("ExtraTextures/WhiteGlow");
			Rectangle rect2 = new Rectangle(0, 0, teleportGlow.Width, teleportGlow.Height);
			Vector2 origin2 = new Vector2((float)(teleportGlow.Width / 2), (float)(teleportGlow.Height / 2));
			Vector2 position3 = base.projectile.Center - Main.screenPosition;
			Color colour2 = Color.Lerp(Color.White, Color.White, 1f / this.teleGlowTimer * 10f) * (1f / this.teleGlowTimer * 10f);
			if (this.teleGlow)
			{
				for (int i = 0; i < base.projectile.oldPos.Length; i++)
				{
					Vector2 drawPos = base.projectile.oldPos[i];
					base.projectile.GetAlpha(Color.White) * ((float)(base.projectile.oldPos.Length - i) / (float)base.projectile.oldPos.Length);
					spriteBatch.Draw(teleportGlow, drawPos + base.projectile.Size / 2f - Main.screenPosition, new Rectangle?(rect2), colour2, base.projectile.rotation, origin2, base.projectile.scale, SpriteEffects.None, 0f);
				}
				spriteBatch.Draw(teleportGlow, position3, new Rectangle?(rect2), colour2, base.projectile.rotation, origin2, 2f, SpriteEffects.None, 0f);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
		}

		public Vector2 origin;

		public int glowAlpha = 255;

		public float teleGlowTimer = 120f;

		public bool teleGlow;
	}
}
