using System;
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
				Redemption.ShowText(base.projectile, 0, 0, 100);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 640f)
			{
				Redemption.ShowText(base.projectile, 0, 1, 260);
			}
			if (base.projectile.localAI[1] == 900f)
			{
				Redemption.ShowText(base.projectile, 0, 2, 400);
			}
			if (base.projectile.localAI[1] == 1300f)
			{
				Redemption.ShowText(base.projectile, 0, 3, 300);
			}
			if (base.projectile.localAI[1] == 1600f)
			{
				Redemption.ShowText(base.projectile, 0, 4, 300);
			}
			if (base.projectile.localAI[1] == 1900f)
			{
				Redemption.ShowText(base.projectile, 0, 5, 300);
			}
			if (base.projectile.localAI[1] == 2100f)
			{
				Redemption.ShowText(base.projectile, 0, 6, 200);
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
