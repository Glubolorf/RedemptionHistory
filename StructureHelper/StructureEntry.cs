using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace Redemption.StructureHelper
{
	internal class StructureEntry : UIElement
	{
		private bool active
		{
			get
			{
				return ManualGeneratorMenu.selected == this;
			}
		}

		public StructureEntry(string name, string path)
		{
			this.Name = name;
			this.Path = path;
			this.Width.Set(400f, 0f);
			this.Height.Set(32f, 0f);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (base.IsMouseHovering)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
			Vector2 pos = Utils.TopLeft(base.GetDimensions().ToRectangle());
			Rectangle mainBox = new Rectangle((int)pos.X, (int)pos.Y, 400, 32);
			Color color = Color.Gray;
			if (base.IsMouseHovering)
			{
				color = Color.White;
			}
			if (this.active)
			{
				color = Color.Yellow;
			}
			ManualGeneratorMenu.DrawBox(spriteBatch, mainBox, (base.IsMouseHovering || this.active) ? new Color(49, 84, 141) : (new Color(49, 84, 141) * 0.6f));
			Utils.DrawBorderString(spriteBatch, this.Name, Utils.Center(mainBox) + Vector2.UnitY * 4f, color, 0.8f, 0.5f, 0.5f, -1);
			base.Draw(spriteBatch);
			if (!this.active)
			{
				this.Height.Set(32f, 0f);
				base.RemoveAllChildren();
			}
		}

		public override void Click(UIMouseEvent evt)
		{
			ManualGeneratorMenu.selected = this;
			ManualGeneratorMenu.multiIndex = 0;
			if (!Generator.StructureDataCache.ContainsKey(this.Path))
			{
				Generator.LoadFile(this.Path, Redemption.Inst, true);
			}
			if (Generator.StructureDataCache[this.Path].ContainsKey("Structures"))
			{
				ManualGeneratorMenu.multiMode = true;
				int count = Generator.StructureDataCache[this.Path].Get<List<TagCompound>>("Structures").Count;
				this.Height.Set((float)(36 + 36 * count), 0f);
				UIList list = new UIList();
				for (int i = 0; i < count; i++)
				{
					list.Add(new MultiSelectionEntry(i));
				}
				list.Width.Set(300f, 0f);
				list.Height.Set((float)(36 * count), 0f);
				list.Left.Set(50f, 0f);
				list.Top.Set(36f, 0f);
				base.Append(list);
				return;
			}
			ManualGeneratorMenu.multiMode = false;
		}

		public string Name = "";

		public string Path;
	}
}
