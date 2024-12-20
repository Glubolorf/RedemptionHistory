using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class GirusTalking3 : ModProjectile
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
			if (this.timer == 600)
			{
				Main.NewText("My prototype failed... What's the matter? Are you not satisfied?", new Color(238, 77, 58), false);
				Redemption.GirusSilence = true;
			}
			if (this.timer == 1000)
			{
				Main.NewText("I'm certainly not! They were all useless! All of the 'Overlords'!", new Color(238, 77, 58), false);
			}
			if (this.timer == 1400)
			{
				Main.NewText("But... Maybe the next one won't be as bad...", new Color(238, 77, 58), false);
			}
			if (this.timer == 1900)
			{
				if (RedeWorld.downedSlayer)
				{
					Main.NewText("I believe you've met him once before...", new Color(238, 77, 58), false);
				}
				else if (RedeWorld.deathBySlayer)
				{
					Main.NewText("I believe you've lost to him once, and gave up.", new Color(238, 77, 58), false);
				}
				else
				{
					Main.NewText("I don't believe you've met him...", new Color(238, 77, 58), false);
				}
			}
			if (this.timer == 2500)
			{
				Main.NewText("Now he's joined my side, and ready to fight a worthy opponent!", new Color(238, 77, 58), false);
			}
			if (this.timer == 3000)
			{
				Main.NewText("Hehehehe...", new Color(238, 77, 58), false);
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeleft)
		{
			RedeWorld.girusTalk3 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}

		public int timer;
	}
}
