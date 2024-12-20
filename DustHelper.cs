using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption
{
	public static class DustHelper
	{
		public static void DrawStar(Vector2 position, int dustType, float pointAmount = 5f, float mainSize = 1f, float dustDensity = 1f, float dustSize = 1f, float pointDepthMult = 1f, float pointDepthMultOffset = 0.5f, bool noGravity = false, float randomAmount = 0f, float rotationAmount = -1f)
		{
			float rot;
			if (rotationAmount < 0f)
			{
				rot = Utils.NextFloat(Main.rand, 0f, 6.2831855f);
			}
			else
			{
				rot = rotationAmount;
			}
			float density = 1f / dustDensity * 0.1f;
			for (float i = 0f; i < 6.28f; i += density)
			{
				float rand = 0f;
				if (randomAmount > 0f)
				{
					rand = Utils.NextFloat(Main.rand, -0.01f, 0.01f) * randomAmount;
				}
				float x = (float)Math.Cos((double)(i + rand));
				float y = (float)Math.Sin((double)(i + rand));
				float mult = Math.Abs(i * (pointAmount / 2f) % 3.1415927f - 1.5707964f) * pointDepthMult + pointDepthMultOffset;
				Dust.NewDustPerfect(position, dustType, new Vector2?(Utils.RotatedBy(new Vector2(x, y), (double)rot, default(Vector2)) * mult * mainSize), 0, default(Color), dustSize).noGravity = noGravity;
			}
		}

		public static void DrawCircle(Vector2 position, int dustType, float mainSize = 1f, float RatioX = 1f, float RatioY = 1f, float dustDensity = 1f, float dustSize = 1f, float randomAmount = 0f, float rotationAmount = 0f, bool nogravity = false)
		{
			float rot;
			if (rotationAmount < 0f)
			{
				rot = Utils.NextFloat(Main.rand, 0f, 6.2831855f);
			}
			else
			{
				rot = rotationAmount;
			}
			float density = 1f / dustDensity * 0.1f;
			for (float i = 0f; i < 6.28f; i += density)
			{
				float rand = 0f;
				if (randomAmount > 0f)
				{
					rand = Utils.NextFloat(Main.rand, -0.01f, 0.01f) * randomAmount;
				}
				float x = (float)Math.Cos((double)(i + rand)) * RatioX;
				float y = (float)Math.Sin((double)(i + rand)) * RatioY;
				if (dustType == 222 || dustType == 130 || nogravity)
				{
					Dust.NewDustPerfect(position, dustType, new Vector2?(Utils.RotatedBy(new Vector2(x, y), (double)rot, default(Vector2)) * mainSize), 0, default(Color), dustSize).noGravity = true;
				}
				else
				{
					Dust.NewDustPerfect(position, dustType, new Vector2?(Utils.RotatedBy(new Vector2(x, y), (double)rot, default(Vector2)) * mainSize), 0, default(Color), dustSize);
				}
			}
		}

		public static void DrawDustImage(Vector2 position, int dustType, float size, string imagePath, float dustSize = 1f, bool noGravity = true, float rot = 0.34f)
		{
			if (Main.netMode != 2)
			{
				float rotation = Utils.NextFloat(Main.rand, 0f - rot, rot);
				Texture2D glyphTexture = ModContent.GetTexture(imagePath);
				Color[] data = new Color[glyphTexture.Width * glyphTexture.Height];
				glyphTexture.GetData<Color>(data);
				for (int i = 0; i < glyphTexture.Width; i += 2)
				{
					for (int j = 0; j < glyphTexture.Height; j += 2)
					{
						if (data[j * glyphTexture.Width + i] == new Color(0, 0, 0))
						{
							double dustX = (double)(i - glyphTexture.Width / 2);
							double dustY = (double)(j - glyphTexture.Height / 2);
							dustX *= (double)size;
							dustY *= (double)size;
							Dust.NewDustPerfect(position, dustType, new Vector2?(Utils.RotatedBy(new Vector2((float)dustX, (float)dustY), (double)rotation, default(Vector2))), 0, default(Color), dustSize).noGravity = noGravity;
						}
					}
				}
			}
		}

		public static void DrawDustImageRainbow(Vector2 position, float size, string imagePath, float dustSize = 1f, bool noGravity = true, float rot = 0.34f)
		{
			int red = Main.rand.Next(60, 255);
			int green = Main.rand.Next(60, 255);
			int blue = Main.rand.Next(60, 255);
			Color color = new Color(red, green, blue);
			if (Main.netMode != 2)
			{
				float rotation = Utils.NextFloat(Main.rand, 0f - rot, rot);
				Texture2D glyphTexture = ModContent.GetTexture(imagePath);
				Color[] data = new Color[glyphTexture.Width * glyphTexture.Height];
				glyphTexture.GetData<Color>(data);
				for (int i = 0; i < glyphTexture.Width; i += 2)
				{
					for (int j = 0; j < glyphTexture.Height; j += 2)
					{
						if (data[j * glyphTexture.Width + i] == new Color(0, 0, 0))
						{
							float num = (float)((double)(i - glyphTexture.Width / 2));
							double dustY = (double)(j - glyphTexture.Height / 2);
							float num2 = (float)((double)num * (double)size);
							dustY *= (double)size;
							Vector2 dir = Utils.RotatedBy(new Vector2(num2, (float)dustY), (double)rotation, default(Vector2));
							Dust.NewDustPerfect(position, 267, new Vector2?(dir), 0, color, dustSize).noGravity = noGravity;
						}
					}
				}
			}
		}

		public static void DrawElectricity(Vector2 point1, Vector2 point2, int dusttype, float scale = 1f, int armLength = 30, Color color = default(Color), float density = 0.05f)
		{
			int nodeCount = (int)Vector2.Distance(point1, point2) / armLength;
			Vector2[] nodes = new Vector2[nodeCount + 1];
			nodes[nodeCount] = point2;
			for (int i = 1; i < Enumerable.Count<Vector2>(nodes); i++)
			{
				nodes[i] = Vector2.Lerp(point1, point2, (float)i / (float)nodeCount) + ((i == Enumerable.Count<Vector2>(nodes) - 1) ? Vector2.Zero : (Utils.RotatedBy(Vector2.Normalize(point1 - point2), 1.5800000429153442, default(Vector2)) * Utils.NextFloat(Main.rand, (float)(-(float)armLength / 2), (float)(armLength / 2))));
				Vector2 prevPos = (i == 1) ? point1 : nodes[i - 1];
				for (float j = 0f; j < 1f; j += density)
				{
					Dust.NewDustPerfect(Vector2.Lerp(prevPos, nodes[i], j), dusttype, new Vector2?(Vector2.Zero), 0, color, scale).noGravity = true;
				}
			}
		}
	}
}
