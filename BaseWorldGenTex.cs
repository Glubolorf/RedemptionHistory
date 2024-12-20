using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Redemption
{
	public class BaseWorldGenTex
	{
		public static TexGen GetTexGenerator(Texture2D tileTex, Dictionary<Color, int> colorToTile, Texture2D wallTex = null, Dictionary<Color, int> colorToWall = null, Texture2D liquidTex = null, Texture2D wireTex = null)
		{
			if (BaseWorldGenTex.colorToLiquid == null)
			{
				BaseWorldGenTex.colorToLiquid = new Dictionary<Color, int>();
				BaseWorldGenTex.colorToLiquid[new Color(0, 0, 255)] = 0;
				BaseWorldGenTex.colorToLiquid[new Color(255, 0, 0)] = 1;
				BaseWorldGenTex.colorToLiquid[new Color(255, 255, 0)] = 2;
			}
			Color[] array = new Color[tileTex.Width * tileTex.Height];
			tileTex.GetData<Color>(0, new Rectangle?(tileTex.Bounds), array, 0, tileTex.Width * tileTex.Height);
			Color[] array2 = (wallTex != null) ? new Color[wallTex.Width * wallTex.Height] : null;
			if (array2 != null)
			{
				wallTex.GetData<Color>(0, new Rectangle?(wallTex.Bounds), array2, 0, wallTex.Width * wallTex.Height);
			}
			Color[] array3 = (liquidTex != null) ? new Color[liquidTex.Width * liquidTex.Height] : null;
			if (array3 != null)
			{
				liquidTex.GetData<Color>(0, new Rectangle?(liquidTex.Bounds), array3, 0, liquidTex.Width * liquidTex.Height);
			}
			int num = 0;
			int num2 = 0;
			TexGen texGen = new TexGen(tileTex.Width, tileTex.Height);
			for (int i = 0; i < array.Length; i++)
			{
				Color key = array[i];
				Color key2 = (wallTex == null) ? Color.Black : array2[i];
				Color key3 = (liquidTex == null) ? Color.Black : array3[i];
				int id = colorToTile.ContainsKey(key) ? colorToTile[key] : -1;
				int wid = (colorToWall != null && colorToWall.ContainsKey(key2)) ? colorToWall[key2] : -1;
				int num3 = (BaseWorldGenTex.colorToLiquid != null && BaseWorldGenTex.colorToLiquid.ContainsKey(key3)) ? BaseWorldGenTex.colorToLiquid[key3] : -1;
				texGen.tileGen[num, num2] = new TileInfo(id, 0, wid, num3, (num3 == -1) ? 0 : 255, -2, -1);
				num++;
				if (num >= tileTex.Width)
				{
					num = 0;
					num2++;
				}
				if (num2 >= tileTex.Height)
				{
					break;
				}
			}
			return texGen;
		}

		public static Dictionary<Color, int> colorToLiquid;
	}
}
