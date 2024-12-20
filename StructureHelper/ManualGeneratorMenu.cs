using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace Redemption.StructureHelper
{
	internal class ManualGeneratorMenu : UIState
	{
		public static bool Visible
		{
			get
			{
				return TestWand.UIVisible;
			}
		}

		public static void LoadStructures()
		{
			ManualGeneratorMenu.structureElements.Clear();
			ManualGeneratorMenu.selected = null;
			if (!Directory.Exists(ModLoader.ModPath.Replace("Mods", "SavedStructures")))
			{
				return;
			}
			string folderPath = ModLoader.ModPath.Replace("Mods", "SavedStructures");
			foreach (string path in Directory.GetFiles(folderPath))
			{
				string name = path.Replace(folderPath + Path.DirectorySeparatorChar.ToString(), "");
				ManualGeneratorMenu.structureElements.Add(new StructureEntry(name, path));
			}
		}

		public override void OnInitialize()
		{
			ManualGeneratorMenu.LoadStructures();
			ManualGeneratorMenu.SetDims(ManualGeneratorMenu.structureElements, -200, 0.5f, 0, 0.1f, 400, 0f, 0, 0.8f);
			ManualGeneratorMenu.SetDims(ManualGeneratorMenu.scrollBar, 232, 0.5f, 0, 0.1f, 32, 0f, 0, 0.8f);
			ManualGeneratorMenu.structureElements.SetScrollbar(ManualGeneratorMenu.scrollBar);
			base.Append(ManualGeneratorMenu.structureElements);
			base.Append(ManualGeneratorMenu.scrollBar);
			ManualGeneratorMenu.SetDims(ManualGeneratorMenu.refreshButton, -200, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			ManualGeneratorMenu.refreshButton.OnClick += new UIElement.MouseEvent(this.RefreshButton_OnClick);
			base.Append(ManualGeneratorMenu.refreshButton);
			ManualGeneratorMenu.SetDims(ManualGeneratorMenu.ignoreButton, -150, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			ManualGeneratorMenu.ignoreButton.OnClick += new UIElement.MouseEvent(this.IgnoreButton_OnClick);
			base.Append(ManualGeneratorMenu.ignoreButton);
			ManualGeneratorMenu.SetDims(ManualGeneratorMenu.closeButton, 168, 0.5f, -50, 0.1f, 32, 0f, 32, 0f);
			ManualGeneratorMenu.closeButton.OnClick += new UIElement.MouseEvent(this.CloseButton_OnClick);
			base.Append(ManualGeneratorMenu.closeButton);
		}

		private void CloseButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			TestWand.UIVisible = false;
		}

		private void IgnoreButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			ManualGeneratorMenu.ignoreNulls = !ManualGeneratorMenu.ignoreNulls;
			Main.isMouseLeftConsumedByUI = true;
		}

		private void RefreshButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			ManualGeneratorMenu.LoadStructures();
			Main.isMouseLeftConsumedByUI = true;
		}

		public override void Update(GameTime gameTime)
		{
			this.Recalculate();
			if (Main.playerInventory)
			{
				TestWand.UIVisible = false;
			}
			if (ManualGeneratorMenu.ignoreButton.IsMouseHovering)
			{
				Main.hoverItemName = string.Format("Place with null tiles: {0}", ManualGeneratorMenu.ignoreNulls);
				Main.LocalPlayer.mouseInterface = true;
			}
			if (ManualGeneratorMenu.refreshButton.IsMouseHovering)
			{
				Main.hoverItemName = "Reload structure folder";
				Main.LocalPlayer.mouseInterface = true;
			}
			if (ManualGeneratorMenu.closeButton.IsMouseHovering)
			{
				Main.hoverItemName = "Close";
				Main.LocalPlayer.mouseInterface = true;
			}
			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Color color = new Color(49, 84, 141);
			ManualGeneratorMenu.DrawBox(spriteBatch, ManualGeneratorMenu.ignoreButton.GetDimensions().ToRectangle(), ManualGeneratorMenu.ignoreButton.IsMouseHovering ? color : (color * 0.8f));
			ManualGeneratorMenu.DrawBox(spriteBatch, ManualGeneratorMenu.refreshButton.GetDimensions().ToRectangle(), ManualGeneratorMenu.refreshButton.IsMouseHovering ? color : (color * 0.8f));
			ManualGeneratorMenu.DrawBox(spriteBatch, ManualGeneratorMenu.closeButton.GetDimensions().ToRectangle(), ManualGeneratorMenu.closeButton.IsMouseHovering ? color : (color * 0.8f));
			Rectangle rect = ManualGeneratorMenu.structureElements.GetDimensions().ToRectangle();
			rect.Inflate(30, 10);
			ManualGeneratorMenu.DrawBox(spriteBatch, rect, new Color(20, 40, 60) * 0.8f);
			base.Draw(spriteBatch);
			if (!ManualGeneratorMenu.ignoreNulls)
			{
				Texture2D tex = ModContent.GetTexture("Redemption/StructureHelper/Cross");
				spriteBatch.Draw(tex, ManualGeneratorMenu.ignoreButton.GetDimensions().ToRectangle(), ManualGeneratorMenu.ignoreButton.IsMouseHovering ? Color.White : (Color.White * 0.5f));
			}
		}

		public static void SetDims(UIElement ele, int x, int y, int w, int h)
		{
			ele.Left.Set((float)x, 0f);
			ele.Top.Set((float)y, 0f);
			ele.Width.Set((float)w, 0f);
			ele.Height.Set((float)h, 0f);
		}

		public static void SetDims(UIElement ele, int x, float xp, int y, float yp, int w, float wp, int h, float hp)
		{
			ele.Left.Set((float)x, xp);
			ele.Top.Set((float)y, yp);
			ele.Width.Set((float)w, wp);
			ele.Height.Set((float)h, hp);
		}

		public static void DrawBox(SpriteBatch sb, Rectangle target, Color color = default(Color))
		{
			Texture2D tex = ModContent.GetTexture("Redemption/StructureHelper/Box");
			if (color == default(Color))
			{
				color = new Color(49, 84, 141) * 0.8f;
			}
			Rectangle sourceCorner = new Rectangle(0, 0, 6, 6);
			Rectangle sourceEdge = new Rectangle(6, 0, 4, 6);
			Rectangle sourceCenter = new Rectangle(6, 6, 4, 4);
			Rectangle inner = target;
			inner.Inflate(-4, -4);
			sb.Draw(tex, inner, new Rectangle?(sourceCenter), color);
			sb.Draw(tex, new Rectangle(target.X + 2, target.Y, target.Width - 4, 6), new Rectangle?(sourceEdge), color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X, target.Y - 2 + target.Height, target.Height - 4, 6), new Rectangle?(sourceEdge), color, -1.5707964f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X - 2 + target.Width, target.Y + target.Height, target.Width - 4, 6), new Rectangle?(sourceEdge), color, 3.1415927f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X + target.Width, target.Y + 2, target.Height - 4, 6), new Rectangle?(sourceEdge), color, 1.5707964f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X, target.Y, 6, 6), new Rectangle?(sourceCorner), color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X + target.Width, target.Y, 6, 6), new Rectangle?(sourceCorner), color, 1.5707964f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X + target.Width, target.Y + target.Height, 6, 6), new Rectangle?(sourceCorner), color, 3.1415927f, Vector2.Zero, SpriteEffects.None, 0f);
			sb.Draw(tex, new Rectangle(target.X, target.Y + target.Height, 6, 6), new Rectangle?(sourceCorner), color, 4.712389f, Vector2.Zero, SpriteEffects.None, 0f);
		}

		public static StructureEntry selected;

		public static bool ignoreNulls = false;

		public static bool multiMode = false;

		public static int multiIndex;

		public static UIList structureElements = new UIList();

		public static UIScrollbar scrollBar = new UIScrollbar();

		public static UIImageButton refreshButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Refresh"));

		public static UIImageButton ignoreButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Null"));

		public static UIImageButton closeButton = new UIImageButton(ModContent.GetTexture("Redemption/StructureHelper/Cross"));
	}
}
