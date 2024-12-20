using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GiantDandelionSeed : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Dandelion Seed");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 42;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 800;
		}

		public override void AI()
		{
			if (++base.projectile.frameCounter >= 10)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.localAI[0] += 1f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.05f;
			if (base.projectile.velocity.Y >= 3f)
			{
				base.projectile.velocity.Y = 2.9f;
			}
			if (this.changeSway)
			{
				this.vectorOffset -= 0.01f;
				if (this.vectorOffset <= -1f)
				{
					this.vectorOffset = -1f;
					this.changeSway = false;
				}
			}
			else
			{
				this.vectorOffset += 0.01f;
				if (this.vectorOffset >= 1f)
				{
					this.vectorOffset = 1f;
					this.changeSway = true;
				}
			}
			float num = BaseUtility.RotationTo(base.projectile.Center, base.projectile.Center + this.originalVelocity);
			base.projectile.velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(0f, base.projectile.velocity.Length()), num + this.vectorOffset * 0.5f);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int crit2 = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref crit2);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref crit2);
			if (crit2 >= 100 || Main.rand.Next(1, 101) <= crit2)
			{
				crit = true;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 8; i++)
			{
				int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 16, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num].velocity *= 1.4f;
			}
		}

		public float vectorOffset;

		public bool changeSway;

		public Vector2 originalVelocity = Vector2.Zero;
	}
}
