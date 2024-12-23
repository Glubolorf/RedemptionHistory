﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;

namespace Redemption
{
	public class CustomCurrency : CustomCurrencySingleCoin
	{
		public CustomCurrency(int coinItemID, long currencyCap) : base(coinItemID, currencyCap)
		{
		}

		public override void GetPriceText(string[] lines, ref int currentLine, int price)
		{
			Color color = this.CustomCurrencyTextColor * ((float)Main.mouseTextColor / 255f);
			int num = currentLine;
			currentLine = num + 1;
			lines[num] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
			{
				color.R,
				color.G,
				color.B,
				Lang.tip[50],
				price,
				"Antique Doruls"
			});
		}

		public Color CustomCurrencyTextColor = Color.DimGray;
	}
}
