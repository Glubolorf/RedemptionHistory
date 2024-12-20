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
			this.itemType = -1;
			this.ammoItemTypes = new int[0];
			this.ammoTypes = new int[0];
			base..ctor();
		}

		public AmmoSlotRender(int itemtype, int ammoitemtype, bool typeisammo = false)
		{
			int[] ammoitemtypes;
			if (!typeisammo)
			{
				(ammoitemtypes = new int[1])[0] = ammoitemtype;
			}
			else
			{
				ammoitemtypes = null;
			}
			object ammotypes;
			if (!typeisammo)
			{
				ammotypes = null;
			}
			else
			{
				(ammotypes = new int[1])[0] = ammoitemtype;
			}
			this..ctor(itemtype, ammoitemtypes, ammotypes);
		}

		public AmmoSlotRender(int itemtype, int[] ammoitemtypes, int[] ammotypes = null)
		{
			this.itemType = -1;
			this.ammoItemTypes = new int[0];
			this.ammoTypes = new int[0];
			base..ctor();
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
			int totalItemCount = 0;
			if (this.ammoItemTypes != null)
			{
				totalItemCount += BasePlayer.GetItemstackSum(Main.player[Main.myPlayer], this.ammoItemTypes, false, true, true);
			}
			if (this.ammoTypes != null)
			{
				totalItemCount += BasePlayer.GetItemstackSum(Main.player[Main.myPlayer], this.ammoTypes, true, true, true);
			}
			string s = string.Concat(totalItemCount);
			if (totalItemCount > 99999)
			{
				s = "A Lot!";
			}
			ChatManager.DrawColorCodedStringWithShadow(sb, Main.fontItemStack, s, pos + new Vector2(10f * sc, 32f * sc), color, 0f, default(Vector2), new Vector2(sc *= 0.8f), -1f, 0.8f);
		}

		public int itemType;

		public int[] ammoItemTypes;

		public int[] ammoTypes;
	}
}
