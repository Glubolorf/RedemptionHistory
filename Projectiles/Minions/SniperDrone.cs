using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class SniperDrone : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Sniper Drone");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = false;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 42;
			base.projectile.height = 30;
			base.projectile.penetrate = -1;
			base.projectile.minion = true;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.netImportant = true;
			base.projectile.timeLeft = 18000;
			base.projectile.minionSlots = 0f;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead || !modPlayer.girusSniperDrone)
			{
				base.projectile.Kill();
			}
			if (modPlayer.girusSniperDrone)
			{
				base.projectile.timeLeft = 2;
			}
			float[] localAI = base.projectile.localAI;
			int num2 = 0;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 % 120f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 200.0);
				this.vector.Y = (float)(Math.Cos(angle) * 100.0);
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.rotation = Utils.ToRotation(Main.MouseWorld - base.projectile.Center);
			base.projectile.localAI[1] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 1000f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[1] % 40f == 0f)
			{
				this.Shoot();
			}
			this.Move(new Vector2(this.vector.X, this.vector.Y - 100f));
		}

		private void Shoot()
		{
			Player player = Main.player[base.projectile.owner];
			Main.PlaySound(SoundID.Item11, base.projectile.position);
			int weaponDamage = base.projectile.damage;
			float weaponKnockback = base.projectile.knockBack;
			Item sItem = RedeHelper.MakeItemFromID(164);
			sItem.damage = weaponDamage;
			player.PickAmmo(sItem, ref this.bullet, ref this.speedB, ref this.canShoot, ref weaponDamage, ref weaponKnockback, true);
			Projectile projectile = Main.projectile[Projectile.NewProjectile(base.projectile.Center, RedeHelper.PolarVector(20f, Utils.ToRotation(Main.MouseWorld - base.projectile.Center)), this.bullet, weaponDamage, weaponKnockback, Main.myPlayer, 0f, 0f)];
			projectile.ranged = true;
			projectile.minion = false;
		}

		public void Move(Vector2 offset)
		{
			Entity entity = Main.player[base.projectile.owner];
			this.speed = 17f;
			Vector2 move = entity.Center + offset - base.projectile.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			float turnResistance = 30f;
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			base.projectile.velocity = move;
		}

		private float Magnitude(Vector2 mag)
		{
			return (float)Math.Sqrt((double)(mag.X * mag.X + mag.Y * mag.Y));
		}

		public override bool MinionContactDamage()
		{
			return false;
		}

		public float speed;

		private Vector2 vector;

		private NPC target;

		public int bullet = 1;

		public bool canShoot = true;

		public float speedB = 14f;
	}
}
