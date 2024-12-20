using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	internal class ViolinProj1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Violin");
			Main.projFrames[base.projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 40;
			base.projectile.height = 18;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.ownerHitCheck = true;
			base.projectile.magic = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 20;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.Kill();
				}
			}
			Player projOwner = Main.player[base.projectile.owner];
			base.projectile.direction = projOwner.direction;
			base.projectile.spriteDirection = projOwner.direction;
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			base.projectile.position.X = projOwner.Center.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = projOwner.Center.Y - (float)(base.projectile.height / 2);
		}
	}
}
