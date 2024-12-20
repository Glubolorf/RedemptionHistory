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
				Main.NewText("Hm? Something took down that good-for-nothing robotic blade I corrupted a few decades ago?", new Color(255, 32, 32), false);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 700f)
			{
				Main.NewText("Haha, knew it was such a weak being!", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1200f)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
				{
					Main.NewText("You, little android! Gongratulations, you defeated the weakest of the 'Overlords'!", new Color(255, 32, 32), false);
				}
				else
				{
					Main.NewText("You, little organic thing! Gongratulations, you defeated the weakest of the 'Overlords'!", new Color(255, 32, 32), false);
				}
			}
			if (base.projectile.localAI[1] == 1600f)
			{
				Main.NewText("All of them are a bunch of useless, good for nothing minions of mine who do my bidding!", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1900f)
			{
				Main.NewText("Let's see if you have what it takes to take them all down... Hehehehe...", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 2100f)
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
