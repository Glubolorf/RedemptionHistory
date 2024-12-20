using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Redemption
{
	public class BaseWorldGenTex
	{
		public static TexGen GetTexGenerator(Texture2D tileTex, Dictionary<Color, int> colorToTile, Texture2D wallTex = null, Dictionary<Color, int> colorToWall = null, Texture2D liquidTex = null, Texture2D slopeTex = null)
		{
			if (BaseWorldGenTex.colorToLiquid == null)
			{
				BaseWorldGenTex.colorToLiquid = new Dictionary<Color, int>();
				BaseWorldGenTex.colorToLiquid[new Color(0, 0, 255)] = 0;
				BaseWorldGenTex.colorToLiquid[new Color(255, 0, 0)] = 1;
				BaseWorldGenTex.colorToLiquid[new Color(255, 255, 0)] = 2;
				BaseWorldGenTex.colorToSlope = new Dictionary<Color, int>();
				BaseWorldGenTex.colorToSlope[new Color(255, 0, 0)] = 1;
				BaseWorldGenTex.colorToSlope[new Color(0, 255, 0)] = 2;
				BaseWorldGenTex.colorToSlope[new Color(0, 0, 255)] = 3;
				BaseWorldGenTex.colorToSlope[new Color(255, 255, 0)] = 4;
				BaseWorldGenTex.colorToSlope[new Color(255, 255, 255)] = -1;
				BaseWorldGenTex.colorToSlope[new Color(0, 0, 0)] = -2;
			}
			Color[] tileData = new Color[tileTex.Width * tileTex.Height];
			tileTex.GetData<Color>(0, new Rectangle?(tileTex.Bounds), tileData, 0, tileTex.Width * tileTex.Height);
			Color[] wallData = (wallTex != null) ? new Color[wallTex.Width * wallTex.Height] : null;
			if (wallData != null)
			{
				wallTex.GetData<Color>(0, new Rectangle?(wallTex.Bounds), wallData, 0, wallTex.Width * wallTex.Height);
			}
			Color[] liquidData = (liquidTex != null) ? new Color[liquidTex.Width * liquidTex.Height] : null;
			if (liquidData != null)
			{
				liquidTex.GetData<Color>(0, new Rectangle?(liquidTex.Bounds), liquidData, 0, liquidTex.Width * liquidTex.Height);
			}
			Color[] slopeData = (slopeTex != null) ? new Color[slopeTex.Width * slopeTex.Height] : null;
			if (slopeData != null)
			{
				slopeTex.GetData<Color>(0, new Rectangle?(slopeTex.Bounds), slopeData, 0, slopeTex.Width * slopeTex.Height);
			}
			int x = 0;
			int y = 0;
			TexGen gen = new TexGen(tileTex.Width, tileTex.Height);
			for (int i = 0; i < tileData.Length; i++)
			{
				Color tileColor = tileData[i];
				Color wallColor = (wallTex == null) ? Color.Black : wallData[i];
				Color liquidColor = (liquidTex == null) ? Color.Black : liquidData[i];
				Color slopeColor = (slopeTex == null) ? Color.Black : slopeData[i];
				int tileID = colorToTile.ContainsKey(tileColor) ? colorToTile[tileColor] : -1;
				int wallID = (colorToWall != null && colorToWall.ContainsKey(wallColor)) ? colorToWall[wallColor] : -1;
				int liquidID = (BaseWorldGenTex.colorToLiquid != null && BaseWorldGenTex.colorToLiquid.ContainsKey(liquidColor)) ? BaseWorldGenTex.colorToLiquid[liquidColor] : -1;
				int slopeID = (BaseWorldGenTex.colorToSlope != null && BaseWorldGenTex.colorToSlope.ContainsKey(slopeColor)) ? BaseWorldGenTex.colorToSlope[slopeColor] : -1;
				gen.tileGen[x, y] = new TileInfo(tileID, 0, wallID, liquidID, (liquidID == -1) ? 0 : 255, slopeID, -1);
				x++;
				if (x >= tileTex.Width)
				{
					x = 0;
					y++;
				}
				if (y >= tileTex.Height)
				{
					break;
				}
			}
			return gen;
		}

		public static Dictionary<Color, int> colorToLiquid;

		public static Dictionary<Color, int> colorToSlope;
	}
}
