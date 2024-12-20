using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.UI
{
	internal class AMemoryUIState : UIState
	{
		public override void OnInitialize()
		{
			this.BG = new AMemoryUIState.UIBackground(this.BgSprite);
			this.BG.Width.Set((float)this.BgSprite.Width, 0f);
			this.BG.Height.Set((float)this.BgSprite.Height, 0f);
			this.BG.Top.Set((float)(Main.screenHeight / 2 - this.BgSprite.Height / 2), 0f);
			this.BG.Left.Set((float)(Main.screenWidth / 2 - this.BgSprite.Width / 2), 0f);
			UIImageButton closeButton = new UIImageButton(ModContent.GetTexture("Redemption/UI/ButtonClosePlaceholder"));
			closeButton.Width.Set(22f, 0f);
			closeButton.Height.Set(22f, 0f);
			closeButton.Left.Set((float)(this.BgSprite.Width - 30), 0f);
			closeButton.Top.Set(8f, 0f);
			closeButton.OnClick += new UIElement.MouseEvent(this.CloseMenu);
			this.BG.Append(closeButton);
			base.Append(this.BG);
		}

		private void CloseMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			AMemoryUIState.Visible = false;
		}

		public override void MouseOver(UIMouseEvent evt)
		{
			Main.isMouseLeftConsumedByUI = true;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (!AMemoryUIState.Visible)
			{
				return;
			}
			base.Draw(spriteBatch);
		}

		public Texture2D BgSprite = ModContent.GetTexture("Redemption/UI/AMemory");

		public static bool Visible;

		internal AMemoryUIState.UIBackground BG;

		internal class UIBackground : UIElement
		{
			public UIBackground(Texture2D tex)
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
