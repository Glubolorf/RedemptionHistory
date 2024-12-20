using System;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public abstract class Minion : ModProjectile
	{
		public override void AI()
		{
			this.CheckActive();
			this.Behavior();
			this.SelectFrame();
		}

		public abstract void CheckActive();

		public abstract void Behavior();

		public abstract void SelectFrame();
	}
}
