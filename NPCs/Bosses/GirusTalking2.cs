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
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] == 580f)
			{
				Main.NewText("Come on, Vlitch Gigipede, I had at least a shred of hope in you...", new Color(238, 77, 58), false);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 800f)
			{
				Main.NewText("Oh whatever, he was a just a big edgy worm, a useless minion!", new Color(238, 77, 58), false);
			}
			if (base.projectile.localAI[1] == 1200f)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
				{
					Main.NewText("Little android! I'm interested in you, keep this up you may even become the latest Overlord!", new Color(238, 77, 58), false);
				}
				else
				{
					Main.NewText("Little organic thing! I'm interested in you, keep going and see how far you get...", new Color(238, 77, 58), false);
				}
			}
			if (base.projectile.localAI[1] == 1600f)
			{
				Main.NewText("Good luck.", new Color(238, 77, 58), false);
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeleft)
		{
			RedeWorld.girusTalk2 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}
	}
}
