using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	internal class TuhonAuraPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tuhon Aura");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 66;
			base.projectile.height = 36;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = false;
			base.projectile.tileCollide = false;
			base.projectile.ownerHitCheck = true;
			base.projectile.melee = true;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 60;
		}

		public override void AI()
		{
			Player projOwner = Main.player[base.projectile.owner];
			base.projectile.direction = projOwner.direction;
			base.projectile.spriteDirection = projOwner.direction;
			projOwner.heldProj = base.projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			base.projectile.position.X = projOwner.Center.X - (float)(base.projectile.width / 2);
			base.projectile.position.Y = projOwner.Center.Y - (float)(base.projectile.height / 2);
			if (base.projectile.timeLeft < 10)
			{
				projOwner.velocity *= 0.96f;
			}
		}
	}
}
