using System;
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
				Redemption.ShowText(base.projectile, 2, 0, 200);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 800f)
			{
				Redemption.ShowText(base.projectile, 2, 1, 300);
			}
			if (base.projectile.localAI[1] == 1100f)
			{
				Redemption.ShowText(base.projectile, 2, 2, 150);
			}
			if (base.projectile.localAI[1] == 1250f)
			{
				Redemption.ShowText(base.projectile, 2, 3, 300);
			}
			if (base.projectile.localAI[1] == 1550f)
			{
				Redemption.ShowText(base.projectile, 2, 4, 300);
			}
			if (base.projectile.localAI[1] == 1850f)
			{
				Redemption.ShowText(base.projectile, 2, 5, 300);
			}
			if (base.projectile.localAI[1] == 2150f)
			{
				Redemption.ShowText(base.projectile, 2, 6, 250);
			}
			if (base.projectile.localAI[1] == 2400f)
			{
				Redemption.ShowText(base.projectile, 2, 7, 100);
			}
			if (base.projectile.localAI[1] == 2500f)
			{
				Redemption.ShowText(base.projectile, 2, 8, 200);
			}
			if (base.projectile.localAI[1] == 2700f)
			{
				Redemption.ShowText(base.projectile, 2, 9, 220);
			}
			if (base.projectile.localAI[1] == 2920f)
			{
				Redemption.ShowText(base.projectile, 2, 10, 120);
			}
			if (base.projectile.localAI[1] >= 3040f)
			{
				Redemption.ShowText(base.projectile, 2, 11, 160);
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
