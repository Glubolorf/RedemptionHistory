using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.CrossMod
{
	public class TitleCard : UIState
	{
		public void DisplayTitle(string text, int displayTime = 1, int fadeTime = 120, float fontScale = 1f, int font = 0, Color? altColor = null, string subtitle = null, bool decorated = true)
		{
			if (RedeConfigClient.Instance.BossIntroText && !Main.dedServ)
			{
				this.Text = text;
				this.SubtitleText = subtitle;
				this.FadeTimer = 0;
				this.DisplayTimer = 0;
				this.MaxDisplayTime = displayTime;
				this.MaxFadeTime = fadeTime;
				this.FontScale = fontScale;
				this.Decorated = decorated;
				this.TextColor = altColor;
				TitleCard.Showing = true;
			}
		}

		public void HandleTimer()
		{
			if (TitleCard.Showing)
			{
				if (this.DisplayTimer < this.MaxDisplayTime)
				{
					if (this.FadeTimer < this.MaxFadeTime)
					{
						this.FadeTimer++;
						return;
					}
					this.DisplayTimer++;
					return;
				}
				else
				{
					if (this.FadeTimer > 0)
					{
						this.FadeTimer--;
						return;
					}
					TitleCard.Showing = false;
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!TitleCard.Showing)
			{
				return;
			}
			Texture2D lineTexture = ModContent.GetTexture("Redemption/ExtraTextures/TitleLine");
			Texture2D leftEndTexture = ModContent.GetTexture("Redemption/ExtraTextures/TitleLineEndLeft");
			Texture2D rightEndTexture = ModContent.GetTexture("Redemption/ExtraTextures/TitleLineEndRight");
			int textFont = this.TextFont;
			DynamicSpriteFont font;
			if (textFont != 1)
			{
				if (textFont != 2)
				{
					font = Main.fontDeathText;
				}
				else
				{
					font = Main.fontMouseText;
				}
			}
			else
			{
				font = Main.fontItemStack;
			}
			int centerX = (int)((float)Main.screenWidth * Main.UIScale) / 2;
			int centerY = (int)((float)Main.screenHeight * Main.UIScale) / 4;
			int textLength = (int)(font.MeasureString(this.Text).X * this.FontScale);
			int textHeight = (int)(font.MeasureString(this.Text).Y * this.FontScale);
			float opacity = (float)this.FadeTimer / (float)this.MaxFadeTime;
			Color drawColor = new Color(255, 255, 255);
			Color shadowColor = new Color(25, 25, 25);
			int totalLength = textLength;
			int subtitleDrawX = centerX;
			int subtitleDrawY = centerY - (int)((float)textHeight * 0.6f);
			int subtitleLength = 0;
			int subtitleHeight = 0;
			Color textColor;
			if (this.TextColor == null)
			{
				textColor = drawColor;
			}
			else
			{
				textColor = this.TextColor.Value;
			}
			if (this.SubtitleText != null)
			{
				subtitleLength = (int)(font.MeasureString(this.SubtitleText).X * (this.FontScale * 0.5f));
				subtitleHeight = (int)(font.MeasureString(this.SubtitleText).Y * (this.FontScale * 0.5f));
				totalLength = ((subtitleLength > textLength) ? subtitleLength : textLength);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			for (int i = centerX - totalLength / 2 + totalLength / 2 % 38; i <= centerX + totalLength / 2 - totalLength / 2 % 38; i += 38)
			{
				Vector2 lineDrawPos = new Vector2((float)i, (float)centerY);
				spriteBatch.Draw(lineTexture, lineDrawPos, new Rectangle?(lineTexture.Bounds), drawColor * opacity, 0f, RedeHelper.GetOrigin(lineTexture, 1), this.FontScale, SpriteEffects.None, 0f);
			}
			Vector2 leftEndPos = new Vector2((float)(centerX - totalLength / 2 + totalLength / 2 % 38 - 23), (float)centerY);
			spriteBatch.Draw(leftEndTexture, leftEndPos, new Rectangle?(leftEndTexture.Bounds), drawColor * opacity, 0f, RedeHelper.GetOrigin(leftEndTexture, 1), this.FontScale, SpriteEffects.None, 0f);
			Vector2 rightEndPos = new Vector2((float)(centerX + totalLength / 2 - totalLength / 2 % 38 + 23), (float)centerY);
			spriteBatch.Draw(rightEndTexture, rightEndPos, new Rectangle?(rightEndTexture.Bounds), drawColor * opacity, 0f, RedeHelper.GetOrigin(rightEndTexture, 1), this.FontScale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			Vector2 textpos = new Vector2((float)centerX - (float)textLength / 2f, (float)centerY - (float)textHeight / 2f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.Text, textpos + new Vector2(2f, 2f), shadowColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale, SpriteEffects.None, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.Text, textpos, textColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale, SpriteEffects.None, 0f);
			if (this.SubtitleText != null)
			{
				Vector2 subtitlepos = new Vector2((float)subtitleDrawX - (float)subtitleLength / 2f, (float)subtitleDrawY - (float)subtitleHeight / 2f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.SubtitleText, subtitlepos + new Vector2(1f, 1f), shadowColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale * 0.5f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.SubtitleText, subtitlepos, textColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale * 0.5f, SpriteEffects.None, 0f);
			}
			this.HandleTimer();
		}

		private string Text;

		private string SubtitleText;

		private int FadeTimer;

		private int MaxFadeTime;

		private int DisplayTimer;

		private int MaxDisplayTime;

		private float FontScale = 1f;

		private bool Decorated;

		private readonly int TextFont;

		private Color? TextColor;

		public static bool Showing;
	}
}
