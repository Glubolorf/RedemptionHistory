using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class GirusTalking2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ominous Voice");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 144;
			base.projectile.height = 144;
			base.projectile.friendly = false;
		}

		public override void AI()
		{
			this.timer++;
			if (this.timer == 580)
			{
				Main.NewText("Come on, Vlitch Gigipede, I had at least a shred of hope in you...", new Color(238, 77, 58), false);
				Redemption.GirusSilence = true;
			}
			if (this.timer == 800)
			{
				Main.NewText("Oh whatever, he was a just a big edgy worm, a useless minion!", new Color(238, 77, 58), false);
			}
			if (this.timer == 1200)
			{
				Main.NewText("Little organic thing! I'm interested in you, keep going and see how far you get...", new Color(238, 77, 58), false);
			}
			if (this.timer == 1600)
			{
				Main.NewText("Good luck.", new Color(238, 77, 58), false);
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeleft)
		{
			RedeWorld.girusTalk2 = true;
			Redemption.GirusSilence = false;
		}

		public int timer;
	}
}
