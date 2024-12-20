using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class StarGodSky2 : CustomSky
	{
		public override void Update(GameTime gameTime)
		{
			if (this.Active)
			{
				this.Intensity = Math.Min(1f, 0.01f + this.Intensity);
				return;
			}
			this.Intensity = Math.Max(0f, this.Intensity - 0.01f);
		}

		public override Color OnTileColor(Color inColor)
		{
			return new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, this.Intensity * 0.5f));
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			Texture2D SkyTex = this.mod.GetTexture("NPCs/Bosses/Nebuleus/StarGodSky2");
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				this.Rotation -= 0.0016f;
				if (!Main.dayTime)
				{
					Vector2 SkyPos = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 2));
					spriteBatch.Draw(SkyTex, SkyPos, null, new Color(120, 120, 120), this.Rotation, new Vector2((float)(SkyTex.Width >> 1), (float)(SkyTex.Height >> 1)), 2f, SpriteEffects.None, 1f);
					double num66 = Main.time / 32400.0;
					int screenWidth = Main.screenWidth;
					int width = Main.moonTexture[Main.moonType].Width;
					int width2 = Main.moonTexture[Main.moonType].Width;
					Color white2 = Color.White;
					double num67 = Main.time / 32400.0;
					float num65 = 1f - Main.cloudAlpha * 1.5f;
					if (num65 < 0f)
					{
						num65 = 0f;
					}
					white2.R = (byte)((float)white2.R * num65);
					white2.G = (byte)((float)white2.G * num65);
					white2.B = (byte)((float)white2.B * num65);
					white2.A = (byte)((float)white2.A * num65);
				}
			}
		}

		public override float GetCloudAlpha()
		{
			return 1f - this.Intensity;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			this.Intensity = 0.006f;
			this.Active = true;
		}

		public override void Deactivate(params object[] args)
		{
			this.Active = false;
		}

		public override void Reset()
		{
			this.Active = false;
		}

		public override bool IsActive()
		{
			return this.Active || this.Intensity > 0.001f;
		}

		public bool Active;

		public float Intensity;

		private UnifiedRandom _random = new UnifiedRandom();

		private readonly Redemption mod = Redemption.inst;

		public float Rotation;
	}
}
