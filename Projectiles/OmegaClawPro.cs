using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	internal class OmegaClawPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloop Gauntlett");
			Main.projFrames[base.projectile.type] = 10;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 70;
			base.projectile.height = 54;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.ownerHitCheck = true;
			base.projectile.aiStyle = 19;
			base.projectile.magic = true;
			base.projectile.alpha = 10;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 30;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 10)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			base.projectile.direction = player.direction;
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = player.itemAnimation;
			base.projectile.position.X = vector.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = vector.Y - (float)(base.projectile.height / 2);
			int num = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 235, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(200, 0, 0, 0) * (1f - (float)base.projectile.alpha / 255f));
		}
	}
}
