using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption
{
	public class RedeColor
	{
		public static Color NebColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
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
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
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
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.DarkGray,
					Color.Black,
					Color.DarkGray
				});
			}
		}

		public static Color AncientColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					new Color(141, 134, 135),
					new Color(241, 165, 62),
					new Color(141, 134, 135)
				});
			}
		}

		public static Color SlayerColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.LightCyan,
					Color.Cyan,
					Color.LightCyan
				});
			}
		}

		public static Color VlitchGlowColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.Red,
					Color.Crimson,
					Color.Red
				});
			}
		}

		public static Color FadeColour1
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.White * 0f,
					Color.White * 0.4f,
					Color.White * 0f
				});
			}
		}

		public static Color HeatColour
		{
			get
			{
				return BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.Orange * 0f,
					Color.White * 0.4f,
					Color.Orange * 0f
				});
			}
		}

		public static Color COLOR_GLOWPULSE
		{
			get
			{
				return Color.White * ((float)Main.mouseTextColor / 255f);
			}
		}

		public static Color COLOR_WHITEFADE1
		{
			get
			{
				Color c = BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.White,
					Color.White * 0.3f,
					Color.White
				});
				c.A = byte.MaxValue;
				return c;
			}
		}

		public static Color COLOR_WHITEFADE2
		{
			get
			{
				Color c = BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.White,
					Color.Red * 0.6f,
					Color.White
				});
				c.A = byte.MaxValue;
				return c;
			}
		}

		public static Color COLOR_WHITEFADE3
		{
			get
			{
				Color c = BaseUtility.MultiLerpColor((float)(Main.LocalPlayer.miscCounter % 100) / 100f, new Color[]
				{
					Color.White,
					Color.White * 0.8f,
					Color.White
				});
				c.A = byte.MaxValue;
				return c;
			}
		}
	}
}
