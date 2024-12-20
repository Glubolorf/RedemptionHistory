using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Redemption.Effects
{
	public class Trail
	{
		public Projectile MyProjectile { get; private set; }

		public bool Dead { get; private set; }

		public Trail(Projectile projectile, ITrailColor type, ITrailCap cap, ITrailPosition position, ITrailShader shader, float widthAtFront, float maxLength)
		{
			this.MyProjectile = projectile;
			this.Dead = false;
			this._trailCap = cap;
			this._trailColor = type;
			this._trailPosition = position;
			this._trailShader = shader;
			this._maxLength = maxLength;
			this._widthStart = widthAtFront;
			this._originalProjectileType = projectile.type;
			this._points = new List<Vector2>();
		}

		public void StartDissolve(float speed)
		{
			this._dissolving = true;
			this._dissolveSpeed = speed;
			this._originalWidth = this._widthStart;
			this._originalMaxLength = this._maxLength;
		}

		public void Update()
		{
			if (this._dissolving)
			{
				this._maxLength -= this._dissolveSpeed;
				this._widthStart = this._maxLength / this._originalMaxLength * this._originalWidth;
				if (this._maxLength <= 0f)
				{
					this.Dead = true;
					return;
				}
				this.TrimToLength(this._maxLength);
				return;
			}
			else
			{
				if (!this.MyProjectile.active || this.MyProjectile.type != this._originalProjectileType)
				{
					this.StartDissolve(this._maxLength / 10f);
					return;
				}
				Vector2 thisPoint = this._trailPosition.GetNextTrailPosition(this.MyProjectile);
				if (this._points.Count == 0)
				{
					this._points.Add(thisPoint);
					return;
				}
				float distance = Vector2.Distance(thisPoint, this._points[0]);
				this._points.Insert(0, thisPoint);
				if (this._currentLength + distance > this._maxLength)
				{
					this.TrimToLength(this._maxLength);
					return;
				}
				this._currentLength += distance;
				return;
			}
		}

		private void TrimToLength(float length)
		{
			if (this._points.Count == 0)
			{
				return;
			}
			this._currentLength = length;
			int firstPointOver = -1;
			float newLength = 0f;
			for (int i = 1; i < this._points.Count; i++)
			{
				newLength += Vector2.Distance(this._points[i], this._points[i - 1]);
				if (newLength > length)
				{
					firstPointOver = i;
					break;
				}
			}
			if (firstPointOver == -1)
			{
				return;
			}
			float leftOverLength = newLength - length;
			Vector2 between = this._points[firstPointOver] - this._points[firstPointOver - 1];
			float newPointDistance = between.Length() - leftOverLength;
			between.Normalize();
			int toRemove = this._points.Count - firstPointOver;
			this._points.RemoveRange(firstPointOver, toRemove);
			this._points.Add(Enumerable.Last<Vector2>(this._points) + between * newPointDistance);
		}

		public void Draw(Effect effect, BasicEffect effect2, GraphicsDevice device)
		{
			if (this.Dead || this._points.Count <= 1)
			{
				return;
			}
			float trailLength = 0f;
			for (int i = 1; i < this._points.Count; i++)
			{
				trailLength += Vector2.Distance(this._points[i - 1], this._points[i]);
			}
			Trail.<>c__DisplayClass25_0 CS$<>8__locals1;
			CS$<>8__locals1.currentIndex = 0;
			CS$<>8__locals1.vertices = new VertexPositionColorTexture[(this._points.Count - 1) * 6 + this._trailCap.ExtraTris * 3];
			float currentDistance = 0f;
			float halfWidth = this._widthStart * 0.5f;
			Vector2 startNormal = this.CurveNormal(this._points, 0);
			Vector2 prevClockwise = this._points[0] + startNormal * halfWidth;
			Vector2 prevCClockwise = this._points[0] - startNormal * halfWidth;
			Color previousColor = this._trailColor.GetColourAt(0f, trailLength, this._points);
			this._trailCap.AddCap(CS$<>8__locals1.vertices, ref CS$<>8__locals1.currentIndex, previousColor, this._points[0], startNormal, this._widthStart);
			for (int j = 1; j < this._points.Count; j++)
			{
				currentDistance += Vector2.Distance(this._points[j - 1], this._points[j]);
				float thisPointsWidth = halfWidth * (1f - (float)j / (float)(this._points.Count - 1));
				Vector2 normal = this.CurveNormal(this._points, j);
				Vector2 clockwise = this._points[j] + normal * thisPointsWidth;
				Vector2 cclockwise = this._points[j] - normal * thisPointsWidth;
				Color color = this._trailColor.GetColourAt(currentDistance, trailLength, this._points);
				Trail.<Draw>g__AddVertex|25_0(clockwise, color, Vector2.UnitX * (float)j, ref CS$<>8__locals1);
				Trail.<Draw>g__AddVertex|25_0(prevClockwise, previousColor, Vector2.UnitX * (float)(j - 1), ref CS$<>8__locals1);
				Trail.<Draw>g__AddVertex|25_0(prevCClockwise, previousColor, new Vector2((float)(j - 1), 1f), ref CS$<>8__locals1);
				Trail.<Draw>g__AddVertex|25_0(clockwise, color, Vector2.UnitX * (float)j, ref CS$<>8__locals1);
				Trail.<Draw>g__AddVertex|25_0(prevCClockwise, previousColor, new Vector2((float)(j - 1), 1f), ref CS$<>8__locals1);
				Trail.<Draw>g__AddVertex|25_0(cclockwise, color, new Vector2((float)j, 1f), ref CS$<>8__locals1);
				prevClockwise = clockwise;
				prevCClockwise = cclockwise;
				previousColor = color;
			}
			int width = device.Viewport.Width;
			int height = device.Viewport.Height;
			Vector2 zoom = Main.GameViewMatrix.Zoom;
			Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation((float)(width / 2), (float)(height / -2), 0f) * Matrix.CreateRotationZ(3.1415927f) * Matrix.CreateScale(zoom.X, zoom.Y, 1f);
			Matrix projection = Matrix.CreateOrthographic((float)width, (float)height, 0f, 1000f);
			effect.Parameters["WorldViewProjection"].SetValue(view * projection);
			this._trailShader.ApplyShader(effect, this, this._points);
			device.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, CS$<>8__locals1.vertices, 0, (this._points.Count - 1) * 2 + this._trailCap.ExtraTris);
		}

		private Vector2 CurveNormal(List<Vector2> points, int index)
		{
			if (points.Count == 1)
			{
				return points[0];
			}
			if (index == 0)
			{
				return this.Clockwise90(Vector2.Normalize(points[1] - points[0]));
			}
			if (index == points.Count - 1)
			{
				return this.Clockwise90(Vector2.Normalize(points[index] - points[index - 1]));
			}
			return this.Clockwise90(Vector2.Normalize(points[index + 1] - points[index - 1]));
		}

		private Vector2 Clockwise90(Vector2 vector)
		{
			return new Vector2(-vector.Y, vector.X);
		}

		[CompilerGenerated]
		internal static void <Draw>g__AddVertex|25_0(Vector2 position, Color color, Vector2 uv, ref Trail.<>c__DisplayClass25_0 A_3)
		{
			VertexPositionColorTexture[] vertices = A_3.vertices;
			int currentIndex = A_3.currentIndex;
			A_3.currentIndex = currentIndex + 1;
			vertices[currentIndex] = new VertexPositionColorTexture(new Vector3(position - Main.screenPosition, 0f), color, uv);
		}

		private readonly int _originalProjectileType;

		private ITrailCap _trailCap;

		private ITrailColor _trailColor;

		private ITrailPosition _trailPosition;

		private ITrailShader _trailShader;

		private float _widthStart;

		private float _currentLength;

		private float _maxLength;

		private List<Vector2> _points;

		private bool _dissolving;

		private float _dissolveSpeed;

		private float _originalMaxLength;

		private float _originalWidth;
	}
}
