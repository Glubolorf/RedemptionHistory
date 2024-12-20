using System;
using System.Linq;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class EarthBarrier : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floating Island");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 224;
			base.projectile.height = 78;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 320;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.position.X = player.Center.X - 112f;
			base.projectile.position.Y = player.Center.Y - 250f;
			if (base.projectile.ai[0] == 0f)
			{
				base.projectile.frame = 0;
			}
			if (base.projectile.ai[0] == 1f)
			{
				base.projectile.frame = 1;
			}
			if (base.projectile.ai[0] == 2f)
			{
				base.projectile.frame = 2;
			}
			if (base.projectile.ai[0] == 3f)
			{
				base.projectile.frame = 3;
			}
			if (base.projectile.ai[0] == 4f)
			{
				base.projectile.frame = 4;
			}
			if (!RedeUkkoAkka.TAearthProtection)
			{
				base.projectile.alpha += 10;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			if (base.projectile.alpha > 0 && base.projectile.timeLeft >= 60)
			{
				base.projectile.alpha -= 10;
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && proj.friendly && !proj.minion)
				{
					proj.Kill();
				}
			}
		}
	}
}
