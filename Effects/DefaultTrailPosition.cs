using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public class DefaultTrailPosition : ITrailPosition
	{
		public Vector2 GetNextTrailPosition(Projectile projectile)
		{
			return projectile.Center;
		}
	}
}
