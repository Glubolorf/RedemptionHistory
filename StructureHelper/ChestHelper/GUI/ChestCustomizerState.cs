using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.StructureHelper.ChestHelper.GUI
{
	internal class ChestCustomizerState : UIState
	{
		public override void OnInitialize()
		{
			ManualGeneratorMenu.SetDims(this.ruleElements, -200, 0.5f, 0, 0.1f, 400, 0f, 0, 0.8f);
			ManualGeneratorMenu.SetDims(this.scrollBar, 232, 0.5f, 0, 0.1f, 32, 0f, 0, 0.8f);
			this.ruleElements.SetScrollbar(this.scrollBar);
			base.Append(this.ruleElements);
			base.Append(this.scrollBar);
			ManualGeneratorMenu.SetDims(this.NewGuaranteed, -200, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			this.NewGuaranteed.OnClick += delegate(UIMouseEvent n, UIElement m)
			{
				this.ruleElements.Add(new GuaranteedRuleElement());
			};
			base.Append(this.NewGuaranteed);
			ManualGeneratorMenu.SetDims(this.NewChance, -160, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			this.NewChance.OnClick += delegate(UIMouseEvent n, UIElement m)
			{
				this.ruleElements.Add(new ChanceRuleElement());
			};
			base.Append(this.NewChance);
			ManualGeneratorMenu.SetDims(this.NewPool, -120, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			this.NewPool.OnClick += delegate(UIMouseEvent n, UIElement m)
			{
				this.ruleElements.Add(new PoolRuleElement());
			};
			base.Append(this.NewPool);
			ManualGeneratorMenu.SetDims(this.NewPoolChance, -80, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			this.NewPoolChance.OnClick += delegate(UIMouseEvent n, UIElement m)
			{
				this.ruleElements.Add(new PoolChanceRuleElement());
			};
			base.Append(this.NewPoolChance);
			ManualGeneratorMenu.SetDims(ChestCustomizerState.closeButton, 168, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			ChestCustomizerState.closeButton.OnClick += delegate(UIMouseEvent n, UIElement m)
			{
				ChestCustomizerState.Visible = false;
			};
			base.Append(ChestCustomizerState.closeButton);
		}

		public bool SetData(ChestEntity entity)
		{
			entity.rules.Clear();
			if (this.ruleElements.Count == 0)
			{
				entity.Kill((int)entity.Position.X, (int)entity.Position.Y);
				return false;
			}
			for (int i = 0; i < this.ruleElements.Count; i++)
			{
				entity.rules.Add((this.ruleElements._items[i] as ChestRuleElement).rule.Clone());
			}
			return true;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			this.Recalculate();
			Color color = new Color(49, 84, 141);
			ManualGeneratorMenu.DrawBox(spriteBatch, this.NewGuaranteed.GetDimensions().ToRectangle(), this.NewGuaranteed.IsMouseHovering ? color : (color * 0.8f));
			ManualGeneratorMenu.DrawBox(spriteBatch, this.NewChance.GetDimensions().ToRectangle(), this.NewChance.IsMouseHovering ? color : (color * 0.8f));
			ManualGeneratorMenu.DrawBox(spriteBatch, this.NewPool.GetDimensions().ToRectangle(), this.NewPool.IsMouseHovering ? color : (color * 0.8f));
			ManualGeneratorMenu.DrawBox(spriteBatch, this.NewPoolChance.GetDimensions().ToRectangle(), this.NewPoolChance.IsMouseHovering ? color : (color * 0.8f));
			ManualGeneratorMenu.DrawBox(spriteBatch, ChestCustomizerState.closeButton.GetDimensions().ToRectangle(), ChestCustomizerState.closeButton.IsMouseHovering ? color : (color * 0.8f));
			Rectangle rect = this.ruleElements.GetDimensions().ToRectangle();
			rect.Inflate(30, 10);
			ManualGeneratorMenu.DrawBox(spriteBatch, rect, new Color(20, 40, 60) * 0.8f);
			if (rect.Contains(Utils.ToPoint(Main.MouseScreen)))
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.NewGuaranteed.IsMouseHovering)
			{
				Main.hoverItemName = "Add New Guaranteed Rule";
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.NewChance.IsMouseHovering)
			{
				Main.hoverItemName = "Add New Chance Rule";
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.NewPool.IsMouseHovering)
			{
				Main.hoverItemName = "Add New Pool Rule";
				Main.LocalPlayer.mouseInterface = true;
			}
			if (this.NewPoolChance.IsMouseHovering)
			{
				Main.hoverItemName = "Add New Pool + Chance Rule";
				Main.LocalPlayer.mouseInterface = true;
			}
			if (ChestCustomizerState.closeButton.IsMouseHovering)
			{
				Main.hoverItemName = "Close";
				Main.LocalPlayer.mouseInterface = true;
			}
			base.Draw(spriteBatch);
		}

		public static bool Visible;

		internal UIList ruleElements = new UIList();

		internal UIScrollbar scrollBar = new UIScrollbar();

		private UIImageButton NewGuaranteed = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/PlusR"));

		private UIImageButton NewChance = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/PlusG"));

		private UIImageButton NewPool = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/PlusP"));

		private UIImageButton NewPoolChance = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/PlusB"));

		public static UIImageButton closeButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Cross"));
	}
}
