using System;
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
				Redemption.ShowText(base.projectile, 1, 0, 220);
				Redemption.GirusSilence = true;
			}
			if (base.projectile.localAI[1] == 800f)
			{
				Redemption.ShowText(base.projectile, 1, 1, 200);
			}
			if (base.projectile.localAI[1] == 1000f)
			{
				Redemption.ShowText(base.projectile, 1, 2, 100);
			}
			if (base.projectile.localAI[1] == 1100f)
			{
				Redemption.ShowText(base.projectile, 1, 3, 300);
			}
			if (base.projectile.localAI[1] == 1400f)
			{
				Redemption.ShowText(base.projectile, 1, 4, 200);
			}
			if (base.projectile.localAI[1] == 1600f)
			{
				Redemption.ShowText(base.projectile, 1, 5, 300);
			}
			if (base.projectile.localAI[1] >= 1900f)
			{
				Redemption.ShowText(base.projectile, 1, 6, 100);
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
