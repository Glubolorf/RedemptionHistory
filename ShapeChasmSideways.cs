using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class ShapeChasmSideways : GenShape
	{
		public ShapeChasmSideways(int startheight, int endheight, int length, int variance, int randomHeading, float[] heightVariance = null, bool dir = true)
		{
			this._startheight = startheight;
			this._endheight = endheight;
			this._length = length;
			this._variance = variance;
			this._randomHeading = randomHeading;
			this._heightVariance = heightVariance;
			this._dir = dir;
		}

		public void ResetChasmParams(int startheight, int endheight, int length, int variance, int randomHeading, float[] heightVariance = null, bool dir = true)
		{
			this._startheight = startheight;
			this._endheight = endheight;
			this._length = length;
			this._variance = variance;
			this._randomHeading = randomHeading;
			this._heightVariance = heightVariance;
			this._dir = dir;
		}

		private bool DoChasm(Point origin, GenAction action, int startheight, int endheight, int length, int variance, int randomHeading, float[] heightVariance, bool dir)
		{
			for (int i = 0; i < length; i++)
			{
				int height = (int)MathHelper.Lerp((float)startheight, (float)endheight, (float)i / (float)length);
				if (heightVariance != null)
				{
					height = Math.Max(endheight, (int)((float)startheight * BaseUtility.MultiLerp((float)i / (float)length, heightVariance)));
				}
				int x = origin.X + (dir ? i : (-i));
				int y = origin.Y + (startheight - height);
				if (variance != 0)
				{
					y += ((Main.rand.Next(2) == 0) ? (-Main.rand.Next(variance)) : Main.rand.Next(variance));
				}
				if (randomHeading != 0)
				{
					y += randomHeading * (i / 2);
				}
				int yend = y + height - (startheight - height);
				for (int m2 = y; m2 < yend; m2++)
				{
					int y2 = m2;
					if (!base.UnitApply(action, origin, x, y2, new object[0]) && this._quitOnFail)
					{
						return false;
					}
				}
			}
			return true;
		}

		public override bool Perform(Point origin, GenAction action)
		{
			return this.DoChasm(origin, action, this._startheight, this._endheight, this._length, this._variance, this._randomHeading, this._heightVariance, this._dir);
		}

		public int _startheight = 20;

		public int _endheight = 5;

		public int _length = 60;

		public int _variance;

		public int _randomHeading;

		public float[] _heightVariance;

		public bool _dir = true;
	}
}
