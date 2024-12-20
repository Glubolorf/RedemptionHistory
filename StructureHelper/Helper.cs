using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.StructureHelper
{
	public static class Helper
	{
		public static void RandomizeList<T>(ref List<T> input)
		{
			int i = Enumerable.Count<T>(input);
			while (i > 1)
			{
				i--;
				int j = WorldGen.genRand.Next(i + 1);
				T value = input[j];
				input[j] = input[i];
				input[i] = value;
			}
		}

		public static Texture2D GetItemTexture(Item item)
		{
			if (item.type < 3930)
			{
				return Main.itemTexture[item.type];
			}
			return ModContent.GetTexture(item.modItem.Texture);
		}
	}
}
