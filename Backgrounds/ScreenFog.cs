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
			bool zoneXeno = Main.player[Main.myPlayer].GetModPlayer<RedePlayer>(Redemption.inst).ZoneXeno;
			if (!this.backgroundFog && BasePlayer.HasAccessory(player, Redemption.inst.ItemType("GasMask"), true, false))
			{
				this.fogOffsetX++;
			}
			if (this.fogOffsetX >= texture.Width)
			{
				this.fogOffsetX = 0;
			}
			if (zoneXeno)
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
			Color newColor;
			newColor..ctor(49, 100, 60);
			this.GetAlpha(defaultColor, 0.2f * this.fadeOpacity * this.dayTimeOpacity);
			Color alpha = this.GetAlpha(newColor, 0.4f * this.fadeOpacity * this.dayTimeOpacity);
			int num = -texture.Width;
			int num2 = -texture.Height;
			int num3 = Main.screenWidth + texture.Width;
			int num4 = Main.screenHeight + texture.Height;
			for (int i = num; i < num3; i += texture.Width)
			{
				for (int j = num2; j < num4; j += texture.Height)
				{
					Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? (-this.fogOffsetX) : this.fogOffsetX), j, texture.Width, texture.Height), null, alpha, 0f, Vector2.Zero, 0, 0f);
				}
			}
			if (setSB)
			{
				Main.spriteBatch.End();
			}
		}

		public Color GetAlpha(Color newColor, float alph)
		{
			int num = 255 - (int)(255f * alph);
			float num2 = (float)(255 - num) / 255f;
			int num3 = (int)((float)newColor.R * num2);
			int num4 = (int)((float)newColor.G * num2);
			int num5 = (int)((float)newColor.B * num2);
			int num6 = (int)newColor.A - num;
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num6 > 255)
			{
				num6 = 255;
			}
			return new Color(num3, num4, num5, num6);
		}

		public int fogOffsetX;

		public float fadeOpacity;

		public float dayTimeOpacity;

		public bool backgroundFog;
	}
}
