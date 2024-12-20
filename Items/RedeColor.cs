using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Items
{
	public class RedeColor
	{
		public static Color NebColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color[]
				{
					new Color(255, 0, 174),
					new Color(108, 0, 255),
					new Color(255, 0, 174)
				});
			}
		}

		public static Color GirusTier
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color[]
				{
					new Color(255, 0, 0),
					new Color(196, 70, 30),
					new Color(255, 0, 0)
				});
			}
		}

		public static Color SoullessColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color[]
				{
					Color.DarkGray,
					Color.Black,
					Color.DarkGray
				});
			}
		}

		public static Color COLOR_WHITEFADE1d
		{
			get
			{
				Color c = BaseUtility.MultiLerpColor((float)(Main.player[Main.myPlayer].miscCounter % 100) / 100f, new Color[]
				{
					Color.White,
					Color.White * 0.7f,
					Color.White
				});
				c.A = byte.MaxValue;
				return c;
			}
		}
	}
}
