using System;

namespace Redemption
{
	public class TileInfo
	{
		public TileInfo(int id, int style, int wid = -1, int lType = -1, int lAmt = 0, int sl = -2, int ob = 0, int w = -1)
		{
			this.tileID = id;
			this.tileStyle = style;
			this.wallID = wid;
			this.liquidType = lType;
			this.liquidAmt = lAmt;
			this.slope = sl;
			this.objectID = ob;
			this.wire = w;
		}

		public int tileID = -1;

		public int tileStyle;

		public int wallID = -1;

		public int objectID;

		public int liquidType = -1;

		public int liquidAmt;

		public int slope = -2;

		public int wire = -1;
	}
}
