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
			Point point = origin;
			for (int i = 0; i < depth; i++)
			{
				int num = (int)MathHelper.Lerp((float)startwidth, (float)endwidth, (float)i / (float)depth);
				if (widthVariance != null)
				{
					num = Math.Max(endwidth, (int)((float)startwidth * BaseUtility.MultiLerp((float)i / (float)depth, widthVariance)));
				}
				int num2 = point.X + (startwidth - num);
				int num3 = point.Y + (dir ? i : (-i));
				if (variance != 0)
				{
					num2 += ((Main.rand.Next(2) == 0) ? (-Main.rand.Next(variance)) : Main.rand.Next(variance));
				}
				if (randomHeading != 0)
				{
					num2 += randomHeading * (i / 2);
				}
				int num4 = num2 + num - (startwidth - num);
				for (int j = num2; j < num4; j++)
				{
					int num5 = j;
					if (!base.UnitApply(action, point, num5, num3, new object[0]) && this._quitOnFail)
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
