using System;

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
			for (int i = 0; i < this.width; i++)
			{
				for (int j = 0; j < this.height; j++)
				{
					int x2 = x + i;
					int y2 = y + j;
					TileInfo tileInfo = this.tileGen[i, j];
					if (tileInfo.tileID != -1 || tileInfo.wallID != -1 || tileInfo.liquidType != -1 || tileInfo.wire != -1)
					{
						if (tileInfo.tileID != -1 || tileInfo.wallID > -1 || tileInfo.wire > -1)
						{
							BaseWorldGen.GenerateTile(x2, y2, tileInfo.tileID, tileInfo.wallID, (tileInfo.tileStyle != 0) ? tileInfo.tileStyle : ((tileInfo.tileID == 4) ? this.torchStyle : ((tileInfo.tileID == 19) ? this.platformStyle : 0)), tileInfo.tileID > -1, tileInfo.liquidAmt == 0, tileInfo.slope, false, sync);
						}
						if (tileInfo.liquidType > -1)
						{
							BaseWorldGen.GenerateLiquid(x2, y2, tileInfo.liquidType, false, tileInfo.liquidAmt, sync);
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
