using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public abstract class Minion : ModProjectile
	{
		public override void AI()
		{
			this.CheckActive();
			this.Behavior();
		}

		public abstract void CheckActive();

		public abstract void Behavior();
	}
}
