using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Redemption.Effects
{
	public interface ITrailColor
	{
		Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points);
	}
}
