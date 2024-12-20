using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class GirusTalking1 : ModProjectile
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
			if (base.projectile.localAI[1] == 540f)
			{
				Main.NewText("...", new Color(255, 32, 32), false);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 640f)
			{
				Main.NewText("...Hm?", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 900f)
			{
				Main.NewText("Would you look at that, an Overlord Unit has been taken down.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1300f)
			{
				Main.NewText("What a shame. That one was almost useful.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1600f)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
				{
					Main.NewText("You know, I never expected one of my own to take down an Overlord..?", new Color(255, 32, 32), false);
				}
				else
				{
					Main.NewText("You know, I never expected someone like you take down an Overlord.", new Color(255, 32, 32), false);
				}
			}
			if (base.projectile.localAI[1] == 1900f)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
				{
					Main.NewText("Huh... Very interesting. I never expected this.", new Color(255, 32, 32), false);
				}
				else
				{
					Main.NewText("Keep on going. It's rather interesting to see you attempt such a feat...", new Color(255, 32, 32), false);
				}
			}
			if (base.projectile.localAI[1] == 2100f)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
				{
					Main.NewText("Anyway, carry on your way, my underling. Hmm...", new Color(255, 32, 32), false);
				}
				else
				{
					Main.NewText("Hehehehe...", new Color(255, 32, 32), false);
				}
			}
			if (base.projectile.localAI[1] >= 2300f)
			{
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeleft)
		{
			RedeWorld.girusTalk1 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}
	}
}
