using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public class ArrowGlowPosition : ITrailPosition
	{
		public Vector2 GetNextTrailPosition(Projectile projectile)
		{
			return projectile.Center + projectile.velocity + Vector2.UnitY * projectile.gfxOffY;
		}
	}
}
