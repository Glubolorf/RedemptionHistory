using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Redemption.Effects
{
	public class StandardColorTrail : ITrailColor
	{
		public StandardColorTrail(Color colour)
		{
			this._colour = colour;
		}

		public Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points)
		{
			float progress = distanceFromStart / trailLength;
			return this._colour * (1f - progress);
		}

		private Color _colour;
	}
}
