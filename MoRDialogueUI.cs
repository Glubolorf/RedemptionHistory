using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption
{
	public class MoRDialogueUI : UIState
	{
		public void DisplayDialogue(string text, int displayTime = 30, int fadeTime = 12, float fontScale = 1f, string whosespeaking = null, float shakestrength = 0f, Color? textColor = null, Color? shadowColor = null, Vector2? textPosition = null, Vector2? speakerPosition = null, int font = 0, int id = 0)
		{
			if (!RedeConfigClient.Instance.NoLoreElements && !Main.dedServ)
			{
				this.Text = text;
				this.Title = whosespeaking;
				this.FadeTimer = 0;
				this.DisplayTimer = 0;
				this.MaxDisplayTime = displayTime;
				this.MaxFadeTime = fadeTime;
				this.FontScale = fontScale;
				this.PointPos = speakerPosition;
				this.TextColor = textColor;
				this.ShadowColor = shadowColor;
				this.Shake = shakestrength;
				this.TextPos = textPosition;
				this.ID = id;
				MoRDialogueUI.Visible = true;
			}
		}

		public void HandleTimer()
		{
			if (MoRDialogueUI.Visible)
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
					MoRDialogueUI.Visible = false;
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!MoRDialogueUI.Visible)
			{
				return;
			}
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
			Vector2 CenterPosition;
			if (this.TextPos != null)
			{
				CenterPosition = this.TextPos.Value;
			}
			else
			{
				CenterPosition = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight / 3));
			}
			int centerX = (int)CenterPosition.X;
			int centerY = (int)CenterPosition.Y;
			int textLength = (int)(font.MeasureString(this.Text).X * this.FontScale);
			int textHeight = (int)(font.MeasureString(this.Text).Y * this.FontScale);
			float opacity = (float)this.FadeTimer / (float)this.MaxFadeTime;
			Color drawColor = new Color(255, 255, 255);
			Color shadowColor = new Color(25, 25, 25);
			Texture2D arrowTexture = ModContent.GetTexture("Redemption/ExtraTextures/MoRDialogueArrow");
			Texture2D darkTexture = ModContent.GetTexture("Redemption/ExtraTextures/BlackSquare");
			int titleDrawX = centerX - textLength / 2;
			int titleDrawY = centerY - (int)((float)textHeight * 0.6f);
			int totalLength = textLength;
			int totalHeight = textHeight;
			int titleLength = 0;
			int titleHeight = 0;
			Color textColor;
			if (this.TextColor == null)
			{
				textColor = drawColor;
			}
			else
			{
				textColor = this.TextColor.Value;
			}
			Color textShadowColor;
			if (this.ShadowColor == null)
			{
				textShadowColor = shadowColor;
			}
			else
			{
				textShadowColor = this.ShadowColor.Value;
			}
			if (this.Title != null)
			{
				titleLength = (int)(font.MeasureString(this.Title).X * (this.FontScale * 0.7f));
				titleHeight = (int)(font.MeasureString(this.Title).Y * (this.FontScale * 0.7f));
				totalLength = textLength + titleLength;
				totalHeight = textHeight + titleHeight * 2;
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			Vector2 topleft = new Vector2(CenterPosition.X - (float)(totalLength / 2), CenterPosition.Y - (float)(totalHeight / 2));
			for (int i = 0; i < 20; i++)
			{
				Vector2 fakeGaussianBlurEffect = Utils.RotatedBy(new Vector2(20f, 0f), (double)(0.31415927f * (float)i), default(Vector2));
				Vector2 actualdrawposition = topleft + fakeGaussianBlurEffect;
				Rectangle rect = new Rectangle((int)actualdrawposition.X, (int)actualdrawposition.Y, totalLength, totalHeight);
				spriteBatch.Draw(darkTexture, rect, new Rectangle?(darkTexture.Bounds), new Color(0, 0, 0) * (opacity * 0.02f));
			}
			if (this.PointPos != null)
			{
				Vector2 arrowOffset = Utils.RotatedBy(new Vector2(1.4142135f, 0f), (double)Utils.ToRotation((this.PointPos - Main.screenPosition).Value - CenterPosition), default(Vector2));
				if (Math.Abs(arrowOffset.X) > 1f)
				{
					bool a = arrowOffset.X >= 0f;
					arrowOffset.X = (float)Utils.ToDirectionInt(a);
				}
				if (Math.Abs(arrowOffset.Y) > 1f)
				{
					bool a2 = arrowOffset.Y >= 0f;
					arrowOffset.Y = (float)Utils.ToDirectionInt(a2);
				}
				arrowOffset.X *= (float)(totalLength / 2 + 30);
				arrowOffset.Y *= (float)(totalHeight / 2 + 30);
				float rot = Utils.ToRotation((this.PointPos - Main.screenPosition).Value - (CenterPosition + arrowOffset));
				spriteBatch.Draw(arrowTexture, CenterPosition + arrowOffset, new Rectangle?(arrowTexture.Bounds), Color.White * opacity * 0.5f, rot, new Vector2((float)(arrowTexture.Width / 2), (float)(arrowTexture.Height / 2)), 1f, SpriteEffects.None, 0f);
			}
			Vector2 textpos = new Vector2((float)centerX - (float)textLength / 2f, (float)centerY - (float)textHeight / 2f) + Utils.RotatedByRandom(new Vector2(Utils.NextFloat(Main.rand, 0f, this.Shake)), 6.2831854820251465);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.Text, textpos + new Vector2(2f, 2f), textShadowColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale, SpriteEffects.None, 0f);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.Text, textpos, textColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale, SpriteEffects.None, 0f);
			if (this.Title != null)
			{
				Vector2 titlepos = new Vector2((float)titleDrawX - (float)titleLength / 2f, (float)titleDrawY - (float)titleHeight);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.Title, titlepos + new Vector2(1f, 1f), textShadowColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale * 0.7f, SpriteEffects.None, 0f);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, font, this.Title, titlepos, textColor * opacity, 0f, new Vector2(0f, 0f), this.FontScale * 0.7f, SpriteEffects.None, 0f);
			}
			this.HandleTimer();
		}

		private string Text;

		private string Title;

		private int FadeTimer;

		private int MaxFadeTime;

		private int DisplayTimer;

		private int MaxDisplayTime;

		private float FontScale = 1f;

		private float Shake;

		public int ID;

		private readonly int TextFont;

		public Vector2? TextPos;

		public Vector2? PointPos;

		public Color? TextColor;

		public Color? ShadowColor;

		public static bool Visible;
	}
}
