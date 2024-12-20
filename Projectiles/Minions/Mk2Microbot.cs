using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.Minions
{
	public class Mk2Microbot : MkTwo
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.projectile.type] = 4;
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.width = 44;
			base.projectile.height = 36;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			this.inertia = 30f;
			this.shoot = base.mod.ProjectileType("Mk2MicrobotPro");
			this.shootSpeed = 15f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.dead)
			{
				modPlayer.mk2MicrobotMinion = false;
			}
			if (modPlayer.mk2MicrobotMinion)
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
				base.projectile.frame = (base.projectile.frame + 1) % 3;
			}
		}
	}
}
