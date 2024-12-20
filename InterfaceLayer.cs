using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace Redemption
{
	public class InterfaceLayer
	{
		public bool visible
		{
			get
			{
				return this.listItem != null && this.listItem.Active;
			}
			set
			{
				if (this.listItem != null)
				{
					this.listItem.Active = value;
				}
			}
		}

		public InterfaceLayer(string n, Action<InterfaceLayer, SpriteBatch> action)
		{
			this.name = n;
			this.drawAction = action;
		}

		public void Draw()
		{
			this.drawAction(this, Main.spriteBatch);
		}

		public string name;

		public Action<InterfaceLayer, SpriteBatch> drawAction;

		public GameInterfaceLayer listItem;
	}
}
