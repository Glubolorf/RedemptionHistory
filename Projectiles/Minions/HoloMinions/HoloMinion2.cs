using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public class HoloMinion2 : HoverShooter
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
			base.projectile.width = 24;
			base.projectile.height = 32;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 100;
			this.inertia = 25f;
			this.shoot = ModContent.ProjectileType<HoloAndroidPro>();
			this.shootSpeed = 13f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.holoMinion = false;
			}
			if (modPlayer.holoMinion)
			{
				base.projectile.timeLeft = 2;
			}
		}

		public override void SelectFrame()
		{
			base.projectile.frameCounter++;
			if (base.projectile.frameCounter >= 5)
			{
				base.projectile.frameCounter = 0;
				base.projectile.frame++;
			}
			if (base.projectile.frame >= 2)
			{
				base.projectile.frame = 0;
			}
		}
	}
}
