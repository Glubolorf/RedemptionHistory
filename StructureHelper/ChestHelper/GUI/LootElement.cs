using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class LootElement : UIElement
	{
		public LootElement(Loot loot, bool hasWeight)
		{
			this.loot = loot;
			this.Width.Set(350f, 0f);
			this.Height.Set(36f, 0f);
			this.Left.Set(50f, 0f);
			this.removeButton.Left.Set(-36f, 1f);
			this.removeButton.Width.Set(32f, 0f);
			this.removeButton.Height.Set(32f, 0f);
			this.removeButton.OnClick += new UIElement.MouseEvent(this.Remove);
			base.Append(this.removeButton);
			this.min = new NumberSetter(loot.min, "Minimum", 80, "");
			base.Append(this.min);
			this.max = new NumberSetter(loot.max, "Maximum", 120, "");
			base.Append(this.max);
			if (hasWeight)
			{
				this.weight = new NumberSetter(loot.weight, "Weight", 160, "");
				base.Append(this.weight);
				return;
			}
			this.upButton.Left.Set(8f, 0f);
			this.upButton.Top.Set(6f, 0f);
			this.upButton.Width.Set(12f, 0f);
			this.upButton.Height.Set(8f, 0f);
			this.upButton.SetVisibility(1f, 0.8f);
			this.upButton.OnClick += new UIElement.MouseEvent(this.MoveUp);
			base.Append(this.upButton);
			this.downButton.Left.Set(8f, 0f);
			this.downButton.Top.Set(16f, 0f);
			this.downButton.Width.Set(12f, 0f);
			this.downButton.Height.Set(8f, 0f);
			this.downButton.SetVisibility(1f, 0.8f);
			this.downButton.OnClick += new UIElement.MouseEvent(this.MoveDown);
			base.Append(this.downButton);
		}

		private void MoveDown(UIMouseEvent evt, UIElement listeningElement)
		{
			UIList list = this.Parent.Parent as UIList;
			int i = list._items.IndexOf(this);
			if (i < list.Count - 1)
			{
				UIElement temp = list._items[i];
				list._items[i] = list._items[i + 1];
				list._items[i + 1] = temp;
			}
		}

		private void MoveUp(UIMouseEvent evt, UIElement listeningElement)
		{
			UIList list = this.Parent.Parent as UIList;
			int i = list._items.IndexOf(this);
			if (i >= 1)
			{
				UIElement temp = list._items[i];
				list._items[i] = list._items[i - 1];
				list._items[i - 1] = temp;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 pos = Utils.TopLeft(base.GetDimensions().ToRectangle());
			Rectangle target = new Rectangle((int)pos.X, (int)pos.Y, (int)base.GetDimensions().Width, 32);
			Color color = Color.White;
			if (this.Parent.Parent.Parent is ChestRuleElement)
			{
				color = (this.Parent.Parent.Parent as ChestRuleElement).color;
			}
			if (this.removeButton.IsMouseHovering)
			{
				Main.hoverItemName = "Remove item";
			}
			ManualGeneratorMenu.DrawBox(spriteBatch, target, color);
			int xOff = 0;
			if (this.weight == null)
			{
				xOff += 16;
			}
			spriteBatch.Draw(Helper.GetItemTexture(this.loot.LootItem), new Rectangle((int)pos.X + 8 + xOff, (int)pos.Y + 8, 16, 16), Color.White);
			string name = (this.loot.LootItem.Name.Length > 25) ? (this.loot.LootItem.Name.Substring(0, 23) + "...") : this.loot.LootItem.Name;
			Utils.DrawBorderString(spriteBatch, name, pos + new Vector2((float)(28 + xOff), 10f), Color.White, 0.7f, 0f, 0f, -1);
			if (this.min.Value > this.max.Value)
			{
				this.min.Value = this.max.Value;
			}
			if (this.max.Value < this.min.Value)
			{
				this.max.Value = this.min.Value;
			}
			this.loot.min = this.min.Value;
			this.loot.max = this.max.Value;
			if (this.weight != null)
			{
				this.loot.weight = this.weight.Value;
			}
			base.Draw(spriteBatch);
		}

		private void Remove(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(this.Parent.Parent.Parent is ChestRuleElement))
			{
				return;
			}
			(this.Parent.Parent.Parent as ChestRuleElement).RemoveItem(this.loot, this);
		}

		private Loot loot;

		private UIImageButton removeButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Cross"));

		private NumberSetter min;

		private NumberSetter max;

		private NumberSetter weight;

		private UIImageButton upButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Up"));

		private UIImageButton downButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Down"));
	}
}
