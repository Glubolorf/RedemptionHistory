using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.Minions
{
	public class BirdMinion2 : Minion2
	{
		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.CloneDefaults(388);
			this.aiType = 388;
			base.projectile.width = 28;
			base.projectile.height = 28;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 0f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blue Jay");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = oldVelocity.Y;
			}
			return false;
		}

		public override void CheckActive()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.dead || !modPlayer.birdMinion)
			{
				base.projectile.Kill();
			}
			if (modPlayer.birdMinion)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
