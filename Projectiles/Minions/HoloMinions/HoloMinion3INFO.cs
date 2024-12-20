using System;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public abstract class HoloMinion3INFO : Minion
	{
		public override void SelectFrame()
		{
		}

		public override void Behavior()
		{
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = false;
			return true;
		}

		protected float idleAccel = 0.05f;

		protected float spacingMult = 1f;

		protected float viewDist = 700f;

		protected float chaseDist = 700f;

		protected float chaseAccel = 7f;

		protected float inertia = 40f;
	}
}
