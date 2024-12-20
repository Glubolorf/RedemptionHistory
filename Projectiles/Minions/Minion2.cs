using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public abstract class Minion2 : ModProjectile
	{
		public override void AI()
		{
			this.CheckActive();
		}

		public abstract void CheckActive();
	}
}
