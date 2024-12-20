using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles
{
	public class NatureRing : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nature Ring");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 422;
			base.projectile.height = 422;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 220;
		}

		public override void AI()
		{
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>().creationBonus)
			{
				base.projectile.Kill();
			}
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.04f;
			Player player = Main.player[base.projectile.owner];
			base.projectile.position.X = player.Center.X - 211f;
			base.projectile.position.Y = player.Center.Y - 211f;
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(ModContent.BuffType<DruidsBlessing>(), 2, false);
				}
			}
			for (int p2 = 0; p2 < 200; p2++)
			{
				this.clearCheck2 = Main.npc[p2];
				if (!this.clearCheck2.immortal && !this.clearCheck2.dontTakeDamage && !this.clearCheck2.friendly && this.clearCheck2.CanBeChasedBy(null, false) && base.projectile.Hitbox.Intersects(this.clearCheck2.Hitbox))
				{
					this.clearCheck2.AddBuff(ModContent.BuffType<DruidsBane>(), 10, false);
				}
			}
			if (player.dead)
			{
				base.projectile.Kill();
			}
		}

		private Player clearCheck;

		private NPC clearCheck2;
	}
}
