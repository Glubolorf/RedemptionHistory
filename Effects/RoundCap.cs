using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Redemption.Effects
{
	public class RoundCap : ITrailCap
	{
		public int ExtraTris
		{
			get
			{
				return 20;
			}
		}

		public void AddCap(VertexPositionColorTexture[] array, ref int currentIndex, Color colour, Vector2 position, Vector2 startNormal, float width)
		{
			float halfWidth = width * 0.5f;
			float num = Utils.ToRotation(startNormal);
			float num2 = 3.1415927f;
			int segments = this.ExtraTris;
			float num3 = num2 / (float)segments;
			float cos = (float)Math.Cos((double)num3);
			float sin = (float)Math.Sin((double)num3);
			float x = (float)Math.Cos((double)num) * halfWidth;
			float y = (float)Math.Sin((double)num) * halfWidth;
			position -= Main.screenPosition;
			VertexPositionColorTexture center = new VertexPositionColorTexture(new Vector3(position.X, position.Y, 0f), colour, Vector2.One * 0.5f);
			VertexPositionColorTexture prev = new VertexPositionColorTexture(new Vector3(position.X + x, position.Y + y, 0f), colour, Vector2.One);
			for (int i = 0; i < segments; i++)
			{
				float t = x;
				x = cos * x - sin * y;
				y = sin * t + cos * y;
				VertexPositionColorTexture next = new VertexPositionColorTexture(new Vector3(position.X + x, position.Y + y, 0f), colour, Vector2.One);
				int num4 = currentIndex;
				currentIndex = num4 + 1;
				array[num4] = center;
				num4 = currentIndex;
				currentIndex = num4 + 1;
				array[num4] = prev;
				num4 = currentIndex;
				currentIndex = num4 + 1;
				array[num4] = next;
				prev = next;
			}
		}
	}
}
