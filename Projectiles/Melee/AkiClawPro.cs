using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	internal class AkiClawPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wind Slash");
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
			base.projectile.melee = true;
			base.projectile.alpha = 100;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 30;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 10)
				{
					base.projectile.frame = 0;
				}
			}
			Player projOwner = Main.player[base.projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			base.projectile.direction = projOwner.direction;
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			base.projectile.position.X = ownerMountedCenter.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = ownerMountedCenter.Y - (float)(base.projectile.height / 2);
		}
	}
}
