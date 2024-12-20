using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI.Chat;

namespace Redemption
{
	public class AmmoSlotRender
	{
		public AmmoSlotRender()
		{
		}

		public AmmoSlotRender(int itemtype, int ammoitemtype, bool typeisammo = false) : this(itemtype, typeisammo ? null : new int[]
		{
			ammoitemtype
		}, typeisammo ? new int[]
		{
			ammoitemtype
		} : null)
		{
		}

		public AmmoSlotRender(int itemtype, int[] ammoitemtypes, int[] ammotypes = null)
		{
			this.itemType = itemtype;
			this.ammoItemTypes = ammoitemtypes;
			this.ammoTypes = ammotypes;
		}

		public virtual void Draw(SpriteBatch sb, Color color, Item item, Vector2 pos, float sc)
		{
			if (Main.playerInventory || item.type <= 0 || item.stack <= 0 || item.type != this.itemType)
			{
				return;
			}
			int num = 0;
			if (this.ammoItemTypes != null)
			{
				num += BasePlayer.GetItemstackSum(Main.player[Main.myPlayer], this.ammoItemTypes, false, true, true);
			}
			if (this.ammoTypes != null)
			{
				num += BasePlayer.GetItemstackSum(Main.player[Main.myPlayer], this.ammoTypes, true, true, true);
			}
			string text = string.Concat(num);
			if (num > 99999)
			{
				text = "A Lot!";
			}
			ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontItemStack, text, pos + new Vector2(10f * sc, 32f * sc), color, 0f, default(Vector2), new Vector2(sc *= 0.8f), -1f, 0.8f);
		}

		public int itemType = -1;

		public int[] ammoItemTypes = new int[0];

		public int[] ammoTypes = new int[0];
	}
}
