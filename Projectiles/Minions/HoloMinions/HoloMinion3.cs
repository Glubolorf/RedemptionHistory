using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.Minions.HoloMinions
{
	public class HoloMinion3 : HoloMinion3INFO
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holo Blade");
			Main.projFrames[base.projectile.type] = 2;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.LightPet[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.width = 48;
			base.projectile.height = 48;
			base.projectile.friendly = true;
			Main.projPet[base.projectile.type] = true;
			base.projectile.minion = true;
			base.projectile.netImportant = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			this.inertia = 35f;
			base.projectile.aiStyle = 66;
			this.aiType = 533;
			base.projectile.damage = 250;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void CheckActive()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.dead)
			{
				redePlayer.holoMinion = false;
			}
			if (redePlayer.holoMinion)
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
