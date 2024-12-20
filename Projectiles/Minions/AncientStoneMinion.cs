using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class AncientStoneMinion : Minion2
	{
		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.CloneDefaults(388);
			this.aiType = 388;
			base.projectile.width = 22;
			base.projectile.height = 22;
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
			base.DisplayName.SetDefault("Ancient Pebble");
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
			if (player.dead || !player.HasBuff(ModContent.BuffType<AncientStoneMinionBuff>()))
			{
				modPlayer.ancientStoneMinion = false;
				base.projectile.Kill();
			}
			if (modPlayer.ancientStoneMinion)
			{
				base.projectile.timeLeft = 2;
			}
		}
	}
}
