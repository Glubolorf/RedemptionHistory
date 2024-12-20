using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class XeniumTurretMinion : ModProjectile
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
			base.projectile.height = 34;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 1f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return new bool?(false);
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.xeniumMinion = false;
			}
			if (modPlayer.xeniumMinion)
			{
				base.projectile.timeLeft = 2;
			}
			base.projectile.frameCounter++;
			if (base.projectile.frameCounter >= 8)
			{
				base.projectile.frameCounter = 0;
				base.projectile.frame = (base.projectile.frame + 1) % 2;
			}
			if (RedeHelper.ClosestNPC(ref this.target, 2000f, player.Center, false, player.MinionAttackTargetNPC, null))
			{
				this.flyTo = this.target.Center;
				base.projectile.spriteDirection = (base.projectile.direction = (((this.flyTo - base.projectile.Center).X > 0f) ? -1 : 1));
				this.shotTime++;
				int bulletID = -1;
				float shootSpeed = 10f;
				int shootDamage = base.projectile.damage;
				float shootKnockback = base.projectile.knockBack;
				if (this.shotTime >= 20 && base.projectile.UseAmmo(AmmoID.Bullet, ref bulletID, ref shootSpeed, ref shootDamage, ref shootKnockback, Main.rand.Next(5) > 0))
				{
					this.shotTime = 0;
					Projectile projectile = Main.projectile[Projectile.NewProjectile(base.projectile.Top + new Vector2((float)(12 * base.projectile.direction * -1), 23f), Vector2.UnitX * (float)base.projectile.direction * -1f * shootSpeed, bulletID, shootDamage, shootKnockback, Main.myPlayer, 0f, 0f)];
					projectile.ranged = false;
					projectile.minion = true;
				}
			}
			else
			{
				this.shotTime = 0;
				this.flyTo = player.Center;
			}
			this.trigCounter += 0.10471976f;
			this.flyTo += Vector2.UnitY * (float)Math.Sin((double)this.trigCounter) * 10f;
			int identity = RedeHelper.MinionHordeIdentity(base.projectile);
			Vector2 diff = this.flyTo - base.projectile.Center;
			base.projectile.spriteDirection = (base.projectile.direction = ((diff.X > 0f) ? -1 : 1));
			if (diff.Y > 100f || diff.Y < -100f)
			{
				for (int i = 0; i < 20; i++)
				{
					Main.dust[Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, base.mod.DustType("XenoDust"), 0f, 0f, 0, default(Color), 1f)].velocity *= 3f;
				}
				base.projectile.Center = this.flyTo + Vector2.UnitX * (float)(identity * 30 + 200) * (float)base.projectile.direction;
				for (int j = 0; j < 20; j++)
				{
					Main.dust[Dust.NewDust(base.projectile.position, base.projectile.width, base.projectile.height, base.mod.DustType("XenoDust"), 0f, 0f, 0, default(Color), 1f)].velocity *= 3f;
				}
			}
			else
			{
				base.projectile.velocity.Y = diff.Y;
				if (diff.Y > 10f)
				{
					base.projectile.velocity.Y = 10f;
				}
				if (diff.Y < -10f)
				{
					base.projectile.velocity.Y = -10f;
				}
			}
			if (diff.X * (float)base.projectile.direction < (float)((identity * 50 + 200) * -1))
			{
				base.projectile.velocity.X = -10f * (float)base.projectile.direction;
				return;
			}
			Projectile projectile2 = base.projectile;
			projectile2.velocity.X = projectile2.velocity.X * 0.9f;
		}

		private const int shotCooldown = 20;

		private int shotTime;

		private NPC target;

		private Vector2 flyTo;

		private float trigCounter;
	}
}
