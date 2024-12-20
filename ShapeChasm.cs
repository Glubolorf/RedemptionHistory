using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Redemption
{
	public class ShapeChasm : GenShape
	{
		public ShapeChasm(int startwidth, int endwidth, int depth, int variance, int randomHeading, float[] widthVariance = null, bool dir = true)
		{
			this._startwidth = startwidth;
			this._endwidth = endwidth;
			this._depth = depth;
			this._variance = variance;
			this._randomHeading = randomHeading;
			this._widthVariance = widthVariance;
			this._dir = dir;
		}

		public void ResetChasmParams(int startwidth, int endwidth, int depth, int variance, int randomHeading, float[] widthVariance = null, bool dir = true)
		{
			this._startwidth = startwidth;
			this._endwidth = endwidth;
			this._depth = depth;
			this._variance = variance;
			this._randomHeading = randomHeading;
			this._widthVariance = widthVariance;
			this._dir = dir;
		}

		private bool DoChasm(Point origin, GenAction action, int startwidth, int endwidth, int depth, int variance, int randomHeading, float[] widthVariance, bool dir)
		{
			for (int i = 0; i < depth; i++)
			{
				int width = (int)MathHelper.Lerp((float)startwidth, (float)endwidth, (float)i / (float)depth);
				if (widthVariance != null)
				{
					width = Math.Max(endwidth, (int)((float)startwidth * BaseUtility.MultiLerp((float)i / (float)depth, widthVariance)));
				}
				int x = origin.X + (startwidth - width);
				int y = origin.Y + (dir ? i : (-i));
				if (variance != 0)
				{
					x += ((Main.rand.Next(2) == 0) ? (-Main.rand.Next(variance)) : Main.rand.Next(variance));
				}
				if (randomHeading != 0)
				{
					x += randomHeading * (i / 2);
				}
				int xend = x + width - (startwidth - width);
				for (int m2 = x; m2 < xend; m2++)
				{
					int x2 = m2;
					if (!base.UnitApply(action, origin, x2, y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
				}
			}
			return true;
		}

		public override bool Perform(Point origin, GenAction action)
		{
			return this.DoChasm(origin, action, this._startwidth, this._endwidth, this._depth, this._variance, this._randomHeading, this._widthVariance, this._dir);
		}

		public int _startwidth = 20;

		public int _endwidth = 5;

		public int _depth = 60;

		public int _variance;

		public int _randomHeading;

		public float[] _widthVariance;

		public bool _dir = true;
	}
}
