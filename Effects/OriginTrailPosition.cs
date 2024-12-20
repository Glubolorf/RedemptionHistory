using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public class OriginTrailPosition : ITrailPosition
	{
		public Vector2 GetNextTrailPosition(Projectile projectile)
		{
			Vector2 drawOrigin = new Vector2((float)Main.projectileTexture[projectile.type].Width * 0.5f, (float)projectile.height * 0.5f);
			return projectile.position + drawOrigin + Vector2.UnitY * projectile.gfxOffY;
		}
	}
}
