using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class ShakeScreen : ModPlayer
	{
		public override void ResetEffects()
		{
			this.shake = false;
			this.shakeSubtle = false;
			this.shakeQuake = false;
		}

		public override void UpdateDead()
		{
			this.shake = false;
			this.shakeSubtle = false;
			this.shakeQuake = false;
		}

		public override void ModifyScreenPosition()
		{
			if (this.shake)
			{
				Main.screenPosition.X = Main.screenPosition.X + (float)Main.rand.Next(-10, 11);
				Main.screenPosition.Y = Main.screenPosition.Y + (float)Main.rand.Next(-10, 11);
			}
			if (this.shakeSubtle)
			{
				Main.screenPosition.X = Main.screenPosition.X + (float)Main.rand.Next(-3, 3);
				Main.screenPosition.Y = Main.screenPosition.Y + (float)Main.rand.Next(-3, 3);
			}
			if (this.shakeQuake)
			{
				Main.screenPosition.Y = Main.screenPosition.Y + (float)Main.rand.Next(-5, 5);
			}
		}

		public bool shake;

		public bool shakeSubtle;

		public bool shakeQuake;
	}
}
