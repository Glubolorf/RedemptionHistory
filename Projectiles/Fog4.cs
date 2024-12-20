using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class Fog4 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fog");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 90;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 220;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] <= 110f)
			{
				base.projectile.alpha--;
			}
			if (base.projectile.localAI[0] > 110f)
			{
				base.projectile.alpha++;
			}
			int num = (int)base.projectile.ai[0];
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num2 = Main.player[num].Center.X - vector.X;
			float num3 = Main.player[num].Center.Y - vector.Y;
			Math.Sqrt((double)(num2 * num2 + num3 * num3));
			if (base.projectile.position.X < Main.player[num].position.X + (float)Main.player[num].width && base.projectile.position.X + (float)base.projectile.width > Main.player[num].position.X && base.projectile.position.Y < Main.player[num].position.Y + (float)Main.player[num].height && base.projectile.position.Y + (float)base.projectile.height > Main.player[num].position.Y && base.projectile.owner == Main.myPlayer)
			{
				Main.player[num].AddBuff(80, 10, false);
			}
		}
	}
}
