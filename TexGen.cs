using System;
using Terraria;

namespace Redemption
{
	public class TexGen
	{
		public TexGen(int w, int h)
		{
			this.width = w;
			this.height = h;
			this.tileGen = new TileInfo[this.width, this.height];
		}

		public void Generate(int x, int y, bool silent, bool sync)
		{
			for (int x2 = 0; x2 < this.width; x2++)
			{
				for (int y2 = 0; y2 < this.height; y2++)
				{
					int x3 = x + x2;
					int y3 = y + y2;
					TileInfo info = this.tileGen[x2, y2];
					if (info.tileID != -1 || info.wallID != -1 || info.liquidType != -1 || info.wire != -1)
					{
						if (info.tileID != -1 || info.wallID > -1 || info.wire > -1)
						{
							BaseWorldGen.GenerateTile(x3, y3, info.tileID, info.wallID, (info.tileStyle != 0) ? info.tileStyle : ((info.tileID == 4) ? this.torchStyle : ((info.tileID == 19) ? this.platformStyle : 0)), info.tileID > -1, info.liquidAmt == 0, info.slope, false, sync);
						}
						if (info.liquidType != -1)
						{
							BaseWorldGen.GenerateLiquid(x3, y3, info.liquidType, false, info.liquidAmt, sync);
						}
						if (info.objectID != 0)
						{
							WorldGen.PlaceObject(x3, y3, info.objectID, false, 0, 0, -1, -1);
							NetMessage.SendObjectPlacment(-1, x3, y3, info.objectID, 0, 0, -1, -1);
						}
					}
				}
			}
		}

		public int width;

		public int height;

		public TileInfo[,] tileGen;

		public int torchStyle;

		public int platformStyle;
	}
}
