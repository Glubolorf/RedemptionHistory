using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Backgrounds.Effects
{
	public class ScreenDarkness
	{
		public ScreenDarkness(bool bg)
		{
			this.backgroundFog = bg;
		}

		public void Update(Texture2D texture)
		{
			if (Main.netMode == 2 || Main.dedServ)
			{
				return;
			}
			Player localPlayer = Main.LocalPlayer;
			bool flag = NPC.AnyNPCs(ModContent.NPCType<WardenIdle>());
			if (this.fogOffsetX >= texture.Width)
			{
				this.fogOffsetX = 0;
			}
			if (flag)
			{
				this.fadeOpacity += 0.05f;
				if (this.fadeOpacity > 1f)
				{
					this.fadeOpacity = 1f;
					return;
				}
			}
			else
			{
				this.fadeOpacity -= 0.05f;
				if (this.fadeOpacity < 0f)
				{
					this.fadeOpacity = 0f;
				}
			}
		}

		public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
		{
			if (this.fadeOpacity == 0f)
			{
				return;
			}
			if (setSB)
			{
				Main.spriteBatch.Begin();
			}
			Player localPlayer = Main.LocalPlayer;
			Color DefaultFog = new Color(255, 255, 255);
			Color fogColor = this.GetAlpha(DefaultFog, 1f * this.fadeOpacity);
			int num = -texture.Width;
			int minY = -texture.Height;
			int maxX = Main.screenWidth + texture.Width;
			int maxY = Main.screenHeight + texture.Height;
			for (int i = num; i < maxX; i += texture.Width)
			{
				for (int j = minY; j < maxY; j += texture.Height)
				{
					Main.spriteBatch.Draw(texture, new Rectangle(i, j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
				}
			}
			if (setSB)
			{
				Main.spriteBatch.End();
			}
		}

		public Color GetAlpha(Color newColor, float alph)
		{
			int alpha = 255 - (int)(255f * alph);
			float alphaDiff = (float)(255 - alpha) / 255f;
			int r = (int)((float)newColor.R * alphaDiff);
			int newG = (int)((float)newColor.G * alphaDiff);
			int newB = (int)((float)newColor.B * alphaDiff);
			int newA = (int)newColor.A - alpha;
			if (newA < 0)
			{
				newA = 0;
			}
			if (newA > 255)
			{
				newA = 255;
			}
			return new Color(r, newG, newB, newA);
		}

		public int fogOffsetX;

		public float fadeOpacity;

		public bool backgroundFog;
	}
}
