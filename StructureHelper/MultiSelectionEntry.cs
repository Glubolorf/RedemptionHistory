using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Redemption.StructureHelper
{
	internal class MultiSelectionEntry : UIElement
	{
		private bool active
		{
			get
			{
				return ManualGeneratorMenu.multiIndex == this.value;
			}
		}

		public MultiSelectionEntry(int index)
		{
			this.value = index;
			this.Width.Set(50f, 0f);
			this.Height.Set(32f, 0f);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (base.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			Vector2 pos = Utils.Center(base.GetDimensions().ToRectangle());
			Color color = Color.Gray;
			if (base.IsMouseHovering)
			{
				color = Color.White;
			}
			if (this.active)
			{
				color = Color.Yellow;
			}
			ManualGeneratorMenu.DrawBox(spriteBatch, base.GetDimensions().ToRectangle(), (base.IsMouseHovering || this.active) ? new Color(49, 84, 141) : (new Color(49, 84, 141) * 0.6f));
			Utils.DrawBorderString(spriteBatch, this.value.ToString(), pos + Vector2.UnitY * 4f, color, 0.8f, 0.5f, 0.5f, -1);
			base.Draw(spriteBatch);
		}

		public override void Click(UIMouseEvent evt)
		{
			ManualGeneratorMenu.multiIndex = this.value;
		}

		public int value;
	}
}
