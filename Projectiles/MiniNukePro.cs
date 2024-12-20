using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class MiniNukePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mini-Nuke");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 34;
			base.projectile.aiStyle = 16;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 170;
		}

		public override void Kill(int timeLeft)
		{
			Vector2 center = base.projectile.Center;
			Main.PlaySound(SoundID.Item14, (int)center.X, (int)center.Y);
			int num = 20;
			for (int i = -num; i <= num; i++)
			{
				for (int j = -num; j <= num; j++)
				{
					int num2 = (int)((float)i + center.X / 16f);
					int num3 = (int)((float)j + center.Y / 16f);
					if (Math.Sqrt((double)(i * i + j * j)) <= (double)num + 0.5)
					{
						WorldGen.KillTile(num2, num3, false, false, false);
						Dust.NewDust(center, 22, 22, 31, 0f, 0f, 120, default(Color), 4f);
					}
				}
			}
		}
	}
}
