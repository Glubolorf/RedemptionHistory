using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.Minions
{
	public class XeniumTurretMinion7 : MkOne
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.projectile.type] = 2;
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.width = 38;
			base.projectile.height = 32;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 2f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			this.inertia = 25f;
			this.shoot = 279;
			this.shootSpeed = 20f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.xeniumMinion7 = false;
			}
			if (modPlayer.xeniumMinion7)
			{
				base.projectile.timeLeft = 2;
			}
		}

		public override void SelectFrame()
		{
			base.projectile.frameCounter++;
			if (base.projectile.frameCounter >= 8)
			{
				base.projectile.frameCounter = 0;
				base.projectile.frame = (base.projectile.frame + 1) % 2;
			}
		}
	}
}
