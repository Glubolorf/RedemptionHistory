using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public class ZigZagTrailPosition : ITrailPosition
	{
		public ZigZagTrailPosition(float strength)
		{
			this._strength = strength;
			this._zigType = 0;
			this._zigMove = 1;
		}

		public Vector2 GetNextTrailPosition(Projectile projectile)
		{
			Vector2 offset = Vector2.Zero;
			if (this._zigType == -1)
			{
				offset = projectile.velocity.TurnLeft();
			}
			else if (this._zigType == 1)
			{
				offset = projectile.velocity.TurnRight();
			}
			if (this._zigType != 0)
			{
				offset.Normalize();
			}
			this._zigType += this._zigMove;
			if (this._zigType == 2)
			{
				this._zigType = 0;
				this._zigMove = -1;
			}
			else if (this._zigType == -2)
			{
				this._zigType = 0;
				this._zigMove = 1;
			}
			return projectile.Center + offset * this._strength;
		}

		private int _zigType;

		private int _zigMove;

		private readonly float _strength;
	}
}
