using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	internal class NoveltyMop_Proj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Novelty Mop");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 36;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.ownerHitCheck = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 25;
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
			base.projectile.timeLeft = 4;
			projOwner.itemTime = 4;
			projOwner.itemAnimation = 4;
			base.projectile.direction = projOwner.direction;
			base.projectile.spriteDirection = projOwner.direction;
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			base.projectile.position.X = projOwner.Center.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = projOwner.Center.Y - (float)(base.projectile.height / 2) + 4f;
			if (!projOwner.channel)
			{
				base.projectile.Kill();
			}
		}
	}
}
