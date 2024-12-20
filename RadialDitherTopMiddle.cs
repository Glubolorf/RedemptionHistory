using System;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Redemption
{
	public class RadialDitherTopMiddle : GenAction
	{
		public RadialDitherTopMiddle(int width, int height, float innerRadius, float outerRadius)
		{
			this._width = width;
			this._height = height;
			this._innerRadius = innerRadius;
			this._outerRadius = outerRadius;
		}

		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Vector2 value = new Vector2((float)origin.X + (float)(this._width / 2), (float)origin.Y);
			float num = Vector2.Distance(new Vector2((float)x, (float)y), value);
			float num2 = Math.Max(0f, Math.Min(1f, (num - this._innerRadius) / (this._outerRadius - this._innerRadius)));
			if (GenBase._random.NextDouble() > (double)num2)
			{
				return base.UnitApply(origin, x, y, args);
			}
			return base.Fail();
		}

		private int _width;

		private int _height;

		private float _innerRadius;

		private float _outerRadius;
	}
}
