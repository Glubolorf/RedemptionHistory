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
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] == 600f)
			{
				Main.NewText("Welp, so much for that prototype.", new Color(255, 32, 32), false);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 1000f)
			{
				Main.NewText("I've noted the flaws so I can perfect the design.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1100f)
			{
				Main.NewText("...", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1400f)
			{
				Main.NewText("Say, you would've now defeated every Overlord I had.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1760f)
			{
				Main.NewText("Well, bad or good news depending on you, that statement is now false.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1900f)
			{
				Main.NewText("One more has joined the ranks, and they are now contesting for YOUR place.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 2200f)
			{
				if (RedeWorld.downedSlayer)
				{
					Main.NewText("I think you two have already met.", new Color(255, 32, 32), false);
				}
				else if (RedeWorld.deathBySlayer)
				{
					Main.NewText("Now's your chance to gain revenge on them.", new Color(255, 32, 32), false);
				}
				else
				{
					Main.NewText("I don't think you two have met.", new Color(255, 32, 32), false);
				}
			}
			if (base.projectile.localAI[1] == 2600f)
			{
				Main.NewText("So.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 2800f)
			{
				Main.NewText("I'll be watching you two fighting.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 3100f)
			{
				Main.NewText("I want the strongest to be on my side.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 3350f)
			{
				Main.NewText("Good luck.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] >= 3600f)
			{
				Main.NewText("You'll need it.", new Color(255, 32, 32), false);
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
	}
}
