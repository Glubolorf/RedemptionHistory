using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Redemption.Effects
{
	public class NoCap : ITrailCap
	{
		public int ExtraTris
		{
			get
			{
				return 0;
			}
		}

		public void AddCap(VertexPositionColorTexture[] array, ref int currentIndex, Color colour, Vector2 position, Vector2 startNormal, float width)
		{
		}
	}
}
