using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption
{
	public class ColorUtils
	{
		public static Color COLOR_GLOWPULSE
		{
			get
			{
				return Color.White * ((float)Main.mouseTextColor / 255f);
			}
		}
	}
}
