using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Redemption.Effects
{
	public interface ITrailCap
	{
		int ExtraTris { get; }

		void AddCap(VertexPositionColorTexture[] array, ref int currentIndex, Color colour, Vector2 position, Vector2 startNormal, float width);
	}
}
