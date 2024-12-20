using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public class WaveTrailPos : ITrailPosition
	{
		public WaveTrailPos(float strength)
		{
			this._strength = strength;
		}

		public Vector2 GetNextTrailPosition(Projectile proj)
		{
			this._counter += 0.33f;
			Vector2 offset = Utils.RotatedBy(Vector2.UnitX, (double)((float)Math.Sin((double)(0.7853982f * this._counter))), default(Vector2));
			return proj.Center + Utils.RotatedBy(offset, (double)Utils.ToRotation(proj.velocity), default(Vector2)) * this._strength;
		}

		private float _counter;

		private readonly float _strength;
	}
}
