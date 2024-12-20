using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public class RainbowTrail : ITrailColor
	{
		public RainbowTrail(float animationSpeed = 5f, float distanceMultiplier = 0.01f, float saturation = 1f, float lightness = 0.5f)
		{
			this._saturation = saturation;
			this._lightness = lightness;
			this._distanceMultiplier = distanceMultiplier;
			this._speed = animationSpeed;
		}

		public Color GetColourAt(float distanceFromStart, float trailLength, List<Vector2> points)
		{
			float progress = distanceFromStart / trailLength;
			float hue = (Main.GlobalTime * this._speed + distanceFromStart * this._distanceMultiplier) % 6.2831855f;
			return this.ColorFromHSL(hue, this._saturation, this._lightness) * (1f - progress);
		}

		private Color ColorFromHSL(float h, float s, float l)
		{
			h /= 6.2831855f;
			float r = 0f;
			float g = 0f;
			float b = 0f;
			if (l != 0f)
			{
				if (s == 0f)
				{
					b = l;
					g = l;
					r = l;
				}
				else
				{
					float temp2;
					if (l < 0.5f)
					{
						temp2 = l * (1f + s);
					}
					else
					{
						temp2 = l + s - l * s;
					}
					float temp3 = 2f * l - temp2;
					r = this.GetColorComponent(temp3, temp2, h + 0.33333334f);
					g = this.GetColorComponent(temp3, temp2, h);
					b = this.GetColorComponent(temp3, temp2, h - 0.33333334f);
				}
			}
			return new Color(r, g, b);
		}

		private float GetColorComponent(float temp1, float temp2, float temp3)
		{
			if (temp3 < 0f)
			{
				temp3 += 1f;
			}
			else if (temp3 > 1f)
			{
				temp3 -= 1f;
			}
			if (temp3 < 0.16666667f)
			{
				return temp1 + (temp2 - temp1) * 6f * temp3;
			}
			if (temp3 < 0.5f)
			{
				return temp2;
			}
			if (temp3 < 0.6666667f)
			{
				return temp1 + (temp2 - temp1) * (0.6666667f - temp3) * 6f;
			}
			return temp1;
		}

		private readonly float _saturation;

		private readonly float _lightness;

		private readonly float _speed;

		private readonly float _distanceMultiplier;
	}
}
