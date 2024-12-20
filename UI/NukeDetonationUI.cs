using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.UI
{
	internal class NukeDetonationUI : UIState
	{
		public override void OnInitialize()
		{
			this.BG = new NukeDetonationUI.NukeBackground(this.BgSprite);
			this.BG.Width.Set((float)this.BgSprite.Width, 0f);
			this.BG.Height.Set((float)this.BgSprite.Height, 0f);
			this.BG.Top.Set((float)(Main.screenHeight / 2 - this.BgSprite.Height / 2), 0f);
			this.BG.Left.Set((float)(Main.screenWidth / 2 - this.BgSprite.Width / 2), 0f);
			UIImageButton nukeButton = new UIImageButton(ModContent.GetTexture("Redemption/Empty"));
			nukeButton.Left.Set(26f, 0f);
			nukeButton.Top.Set(104f, 0f);
			nukeButton.Width.Set(164f, 0f);
			nukeButton.Height.Set(164f, 0f);
			nukeButton.OnClick += new UIElement.MouseEvent(this.NukeButtonClicked);
			this.BG.Append(nukeButton);
			UIImageButton closeButton = new UIImageButton(ModContent.GetTexture("Redemption/UI/ButtonClosePlaceholder"));
			closeButton.Width.Set(22f, 0f);
			closeButton.Height.Set(22f, 0f);
			closeButton.Left.Set((float)(this.BgSprite.Width - 30), 0f);
			closeButton.Top.Set(8f, 0f);
			closeButton.OnClick += new UIElement.MouseEvent(this.CloseMenu);
			this.BG.Append(closeButton);
			base.Append(this.BG);
		}

		private void NukeButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.PlaySound(12, -1, -1, 1, 1f, 0f);
			if (this.ButtonState < 2 && Vector2.Distance(RedeWorld.nukeGroundZero, new Vector2((float)(Main.spawnTileX * 16), (float)(Main.spawnTileY * 16))) > (float)(Main.maxTilesX / 8 * 16) && (double)RedeWorld.nukeGroundZero.Y < Main.worldSurface * 16.0)
			{
				this.ButtonState++;
			}
			else if (this.ButtonState < 2)
			{
				BaseUtility.Chat("The bomb must be activated on the surface and in the far reaches of the world", byte.MaxValue, byte.MaxValue, byte.MaxValue, true);
			}
			Main.isMouseLeftConsumedByUI = true;
		}

		private void CloseMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.ButtonState != 2)
			{
				this.ButtonState = 0;
				NukeDetonationUI.Visible = false;
			}
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			Main.isMouseLeftConsumedByUI = true;
		}

		public override void Update(GameTime gameTime)
		{
			if (!NukeDetonationUI.Visible)
			{
				this.ButtonState = 0;
			}
			if (this.ButtonState == 2)
			{
				this.Timer++;
				if (this.Timer > 60)
				{
					this.ButtonState = 3;
					this.Timer = 0;
					RedeWorld.nukeCountdownActive = true;
					NukeDetonationUI.Visible = false;
					return;
				}
			}
			else
			{
				this.Timer = 0;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!NukeDetonationUI.Visible)
			{
				return;
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			base.Draw(spriteBatch);
			Texture2D buttonTexture = ModContent.GetTexture("Redemption/UI/NukeDetonationUI_Button1");
			Texture2D buttonTexture2 = ModContent.GetTexture("Redemption/UI/NukeDetonationUI_Button2");
			Texture2D buttonTexture3 = ModContent.GetTexture("Redemption/UI/NukeDetonationUI_Button3");
			Vector2 buttonOrigin = new Vector2(101f, 300f);
			int centerX = Main.screenWidth / 2;
			int centerY = Main.screenHeight / 2;
			Vector2 screenCenter = new Vector2((float)centerX, (float)centerY);
			int buttonState = this.ButtonState;
			Texture2D buttonDrawTexture;
			if (buttonState != 1)
			{
				if (buttonState != 2)
				{
					buttonDrawTexture = buttonTexture;
				}
				else
				{
					buttonDrawTexture = buttonTexture3;
				}
			}
			else
			{
				buttonDrawTexture = buttonTexture2;
			}
			spriteBatch.Draw(buttonDrawTexture, screenCenter + new Vector2(-196f, 20f), null, Color.White, 0f, buttonOrigin, 1f, SpriteEffects.None, 0f);
		}

		public Texture2D BgSprite = ModContent.GetTexture("Redemption/UI/NukeDetonationUI_BG");

		public Texture2D ButtonTexture = ModContent.GetTexture("Redemption/UI/NukeDetonationUI_Button1");

		public int ButtonState;

		public int Timer;

		public static bool Visible;

		internal NukeDetonationUI.NukeBackground BG;

		internal class NukeBackground : UIElement
		{
			public NukeBackground(Texture2D tex)
			{
				this.Texture = tex;
			}

			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				Point point = new Point((int)dimensions.X, (int)dimensions.Y);
				int width = (int)Math.Ceiling((double)dimensions.Width);
				int height = (int)Math.Ceiling((double)dimensions.Height);
				spriteBatch.Draw(this.Texture, new Rectangle(point.X, point.Y, width, height), Color.White);
			}

			public Texture2D Texture;
		}
	}
}
