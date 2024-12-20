using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Redemption.Effects
{
	public class GradientTrail : ITrailColor
	{
		public GradientTrail(Color start, Color end)
		{
			this._startColour = start;
			this._endColour = end;
		}

		public Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points)
		{
			float progress = distanceFromStart / trailLength;
			return Color.Lerp(this._startColour, this._endColour, progress) * (1f - progress);
		}

		private Color _startColour;

		private Color _endColour;
	}
}
