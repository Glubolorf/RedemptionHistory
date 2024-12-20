using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Projectiles.Minions
{
	public class DarkSoulMinion : Minion2
	{
		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.CloneDefaults(388);
			this.aiType = 388;
			base.projectile.width = 26;
			base.projectile.height = 20;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
			Main.projPet[base.projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
		}

		public override void AI()
		{
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 2, base.projectile.height + 2, base.mod.DustType("VoidFlame"), base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 2.9f);
			Main.dust[num].noGravity = true;
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
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.dead)
			{
				modPlayer.darkSoulMinion = false;
			}
			if (modPlayer.darkSoulMinion)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
