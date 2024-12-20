using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Redemption.Backgrounds
{
	public class ScreenFog
	{
		public ScreenFog(bool bg)
		{
			this.backgroundFog = bg;
		}

		public void Update(Texture2D texture)
		{
			if (Main.netMode == 2 || Main.dedServ)
			{
				return;
			}
			Player player = Main.player[Main.myPlayer];
			bool flag = Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneXeno || Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().ZoneEvilXeno;
			if (!this.backgroundFog && BasePlayer.HasAccessory(player, Redemption.inst.ItemType("GasMask"), true, false))
			{
				this.fogOffsetX++;
			}
			if (this.fogOffsetX >= texture.Width)
			{
				this.fogOffsetX = 0;
			}
			if (flag || Main.player[Main.myPlayer].GetModPlayer<RedePlayer>().irradiatedEffect >= 4)
			{
				this.fadeOpacity += 0.05f;
				if (this.fadeOpacity > 1f)
				{
					this.fadeOpacity = 1f;
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
			if (this.backgroundFog)
			{
				this.dayTimeOpacity = (Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, new float[]
				{
					0.5f,
					1f,
					1f,
					1f,
					1f,
					1f,
					0.5f
				}) : 0.5f);
				this.dayTimeOpacity *= 0.7f;
				return;
			}
			this.dayTimeOpacity = (Main.dayTime ? BaseUtility.MultiLerp((float)Main.time / 52000f, new float[]
			{
				0.3f,
				1f,
				1f,
				1f,
				1f,
				1f,
				0.3f
			}) : 0.3f);
			this.dayTimeOpacity *= (Main.dayTime ? 2f : 1f);
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
			Redemption inst = Redemption.inst;
			Color DefaultFog = new Color(49, 100, 60);
			this.GetAlpha(defaultColor, 0.2f * this.fadeOpacity * this.dayTimeOpacity);
			Color fogColor = this.GetAlpha(DefaultFog, 0.4f * this.fadeOpacity * this.dayTimeOpacity);
			int num = -texture.Width;
			int minY = -texture.Height;
			int maxX = Main.screenWidth + texture.Width;
			int maxY = Main.screenHeight + texture.Height;
			for (int i = num; i < maxX; i += texture.Width)
			{
				for (int j = minY; j < maxY; j += texture.Height)
				{
					Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? (-this.fogOffsetX) : this.fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
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

		public float dayTimeOpacity;

		public bool backgroundFog;
	}
}
