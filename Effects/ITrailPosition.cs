using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Effects
{
	public interface ITrailPosition
	{
		Vector2 GetNextTrailPosition(Projectile projectile);
	}
}
