using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class ChestRuleElement : UIElement
	{
		public ChestRuleElement(ChestRule rule)
		{
			this.rule = rule;
			this.Width.Set(400f, 0f);
			this.Height.Set(36f, 0f);
			this.lootElements.Left.Set(0f, 0f);
			this.lootElements.Top.Set(40f, 0f);
			this.lootElements.Width.Set(400f, 0f);
			this.lootElements.Height.Set(0f, 0f);
			base.Append(this.lootElements);
			this.removeButton.Left.Set(-36f, 1f);
			this.removeButton.Width.Set(32f, 0f);
			this.removeButton.Height.Set(32f, 0f);
			this.removeButton.OnClick += new UIElement.MouseEvent(this.Remove);
			base.Append(this.removeButton);
			this.upButton.Left.Set(4f, 0f);
			this.upButton.Top.Set(-4f, 0f);
			this.upButton.Width.Set(24f, 0f);
			this.upButton.Height.Set(18f, 0f);
			this.upButton.SetVisibility(1f, 0.8f);
			this.upButton.OnClick += new UIElement.MouseEvent(this.MoveUp);
			base.Append(this.upButton);
			this.downButton.Left.Set(4f, 0f);
			this.downButton.Top.Set(18f, 0f);
			this.downButton.Width.Set(24f, 0f);
			this.downButton.Height.Set(18f, 0f);
			this.downButton.SetVisibility(1f, 0.8f);
			this.downButton.OnClick += new UIElement.MouseEvent(this.MoveDown);
			base.Append(this.downButton);
			this.hideButton.Left.Set(-56f, 1f);
			this.hideButton.Top.Set(10f, 0f);
			this.hideButton.Width.Set(18f, 0f);
			this.hideButton.Height.Set(12f, 0f);
			this.hideButton.SetVisibility(1f, 0.5f);
			this.hideButton.OnClick += new UIElement.MouseEvent(this.Hide);
			base.Append(this.hideButton);
			foreach (Loot loot in rule.pool)
			{
				this.AddItem(loot);
			}
		}

		private void Hide(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.storedHeight == 0f)
			{
				this.hideButton.SetImage(ModContent.GetTexture("Redemption/StructureHelper/EyeClosed"));
				this.storedHeight = base.GetDimensions().Height;
				this.Height.Set(36f, 0f);
				return;
			}
			this.hideButton.SetImage(ModContent.GetTexture("Redemption/StructureHelper/Eye"));
			this.Height.Set(this.storedHeight, 0f);
			this.storedHeight = 0f;
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

		public override void Click(UIMouseEvent evt)
		{
			if (Main.mouseItem.IsAir || this.storedHeight > 0f)
			{
				return;
			}
			this.AddItem(Main.mouseItem);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Vector2 pos = Utils.TopLeft(base.GetDimensions().ToRectangle());
			Rectangle target = new Rectangle((int)pos.X, (int)pos.Y, (int)base.GetDimensions().Width, 32);
			ManualGeneratorMenu.DrawBox(spriteBatch, target, this.color);
			if (target.Contains(Utils.ToPoint(Main.MouseScreen)))
			{
				Main.hoverItemName = this.rule.Tooltip + "\nLeft click this while holding an item to add it";
			}
			if (this.removeButton.IsMouseHovering)
			{
				Main.hoverItemName = "Remove rule";
			}
			if (this.upButton.IsMouseHovering)
			{
				Main.hoverItemName = "Move Up";
			}
			if (this.downButton.IsMouseHovering)
			{
				Main.hoverItemName = "Move Down";
			}
			if (this.hideButton.IsMouseHovering)
			{
				Main.hoverItemName = ((this.storedHeight > 0f) ? "Show Items" : "Hide Items");
			}
			Utils.DrawBorderString(spriteBatch, this.rule.Name, pos + new Vector2(32f, 8f), Color.White, 0.8f, 0f, 0f, -1);
			if (this.storedHeight == 0f)
			{
				base.Draw(spriteBatch);
				return;
			}
			this.removeButton.Draw(spriteBatch);
			this.upButton.Draw(spriteBatch);
			this.downButton.Draw(spriteBatch);
			this.hideButton.Draw(spriteBatch);
		}

		public void AddItem(Item item)
		{
			LootElement element = new LootElement(this.rule.AddItem(item), this.rule.UsesWeight);
			this.lootElements.Add(element);
			this.lootElements.Height.Set(this.lootElements.Height.Pixels + element.Height.Pixels + 4f, 0f);
			this.Height.Set(this.Height.Pixels + element.Height.Pixels + 4f, 0f);
		}

		public void AddItem(Loot loot)
		{
			LootElement element = new LootElement(loot, this.rule.UsesWeight);
			this.lootElements.Add(element);
			this.lootElements.Height.Set(this.lootElements.Height.Pixels + element.Height.Pixels + 4f, 0f);
			this.Height.Set(this.Height.Pixels + element.Height.Pixels + 4f, 0f);
		}

		public void RemoveItem(Loot loot, LootElement element)
		{
			this.rule.RemoveItem(loot);
			this.lootElements.Remove(element);
			this.lootElements.Height.Set(this.lootElements.Height.Pixels - element.Height.Pixels - 4f, 0f);
			this.Height.Set(this.Height.Pixels - element.Height.Pixels - 4f, 0f);
		}

		private void Remove(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!(this.Parent.Parent.Parent is ChestCustomizerState))
			{
				return;
			}
			(this.Parent.Parent.Parent as ChestCustomizerState).ruleElements.Remove(this);
		}

		internal ChestRule rule;

		internal Color color = Color.White;

		internal float storedHeight;

		internal UIList lootElements = new UIList();

		private UIImageButton removeButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Cross"));

		private UIImageButton upButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/UpLarge"));

		private UIImageButton downButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/DownLarge"));

		private UIImageButton hideButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Eye"));
	}
}
