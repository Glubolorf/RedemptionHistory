using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class NumberSetter : UIElement
	{
		public NumberSetter(int value, string text, int xOff, string suffix = "")
		{
			this.Value = value;
			this.Text = text;
			this.Suffix = suffix;
			this.Width.Set(32f, 0f);
			this.Height.Set(32f, 0f);
			this.Left.Set((float)(-(float)xOff), 1f);
			this.IncrementButton.Width.Set(12f, 0f);
			this.IncrementButton.Height.Set(8f, 0f);
			this.IncrementButton.Top.Set(7f, 0f);
			this.IncrementButton.OnClick += delegate(UIMouseEvent n, UIElement w)
			{
				this.Value++;
			};
			base.Append(this.IncrementButton);
			this.DecrementButton.Width.Set(12f, 0f);
			this.DecrementButton.Height.Set(8f, 0f);
			this.DecrementButton.Top.Set(15f, 0f);
			this.DecrementButton.OnClick += delegate(UIMouseEvent n, UIElement w)
			{
				this.Value--;
			};
			base.Append(this.DecrementButton);
		}

		public override void ScrollWheel(UIScrollWheelEvent evt)
		{
			this.Value += ((evt.ScrollWheelValue > 0) ? 1 : -1);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 pos = Utils.TopLeft(base.GetDimensions().ToRectangle());
			Rectangle target = new Rectangle((int)pos.X + 14, (int)pos.Y + 8, 20, 16);
			spriteBatch.Draw(Main.magicPixel, target, Color.Black * 0.5f);
			Utils.DrawBorderString(spriteBatch, this.Value.ToString() + this.Suffix, Utils.Center(target), Color.White, 0.7f, 0.5f, 0.4f, -1);
			if (this.Value < 1)
			{
				this.Value = 1;
			}
			if (base.IsMouseHovering)
			{
				Main.hoverItemName = this.Text;
			}
			base.Draw(spriteBatch);
		}

		public int Value;

		private string Text;

		private string Suffix;

		private UIImageButton IncrementButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Up"));

		private UIImageButton DecrementButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Down"));
	}
}
