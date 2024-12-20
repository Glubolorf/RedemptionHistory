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
				Main.NewText("Second Overlord is down. Oh well.", new Color(255, 32, 32), false);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 800f)
			{
				Main.NewText("Wasn't my creation though, in fact it was our...", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1000f)
			{
				Main.NewText("...", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1100f)
			{
				Main.NewText("... That doesn't matter. 'Grats! You defeated a black-and-red worm machine thing!", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1400f)
			{
				Main.NewText("*slow clapping* Very impressive.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] == 1750f)
			{
				Main.NewText("Now, the next Overlord is a prototype of mine. It's already quite the fighter.", new Color(255, 32, 32), false);
			}
			if (base.projectile.localAI[1] >= 2000f)
			{
				Main.NewText("Good luck.", new Color(255, 32, 32), false);
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
