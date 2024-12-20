using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class MageDrone : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Jellyfish Drone");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = false;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 48;
			base.projectile.height = 34;
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
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead || !player.HasBuff(base.mod.BuffType("MageDroneBuff")))
			{
				modPlayer.jellyfishDrone = false;
				base.projectile.Kill();
			}
			if (modPlayer.jellyfishDrone)
			{
				base.projectile.timeLeft = 2;
			}
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			float[] localAI = base.projectile.localAI;
			int num2 = 0;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 % 180f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 200.0);
				this.vector.Y = (float)(Math.Cos(angle) * 200.0);
			}
			for (int i = 0; i < 4; i++)
			{
				double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector2.X = (float)(Math.Sin(angle2) * 100.0);
				this.vector2.Y = (float)(Math.Cos(angle2) * 100.0);
				Dust dust = Main.dust[Dust.NewDust(base.projectile.Center + this.vector2, 2, 2, 235, 0f, 0f, 100, default(Color), 1f)];
				dust.noGravity = true;
				dust.velocity *= 0f;
			}
			base.projectile.localAI[1] += 1f;
			for (int p = 0; p < 200; p++)
			{
				this.clearCheck = Main.projectile[p];
			}
			if (Vector2.Distance(base.projectile.Center, this.clearCheck.Center) < 100f && this.clearCheck.whoAmI != base.projectile.whoAmI && base.projectile.localAI[1] % 120f == 0f)
			{
				Main.PlaySound(SoundID.Item93, (int)base.projectile.position.X, (int)base.projectile.position.Y);
				int proj = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.clearCheck.Center - base.projectile.Center)), base.mod.ProjectileType("GirusDischarge2"), 100, 0f, Main.myPlayer, 0f, 0f);
				Main.projectile[proj].netUpdate = true;
			}
			if (RedeHelper.ClosestNPC(ref this.target, 100f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[1] % 120f == 0f)
			{
				Main.PlaySound(SoundID.Item93, (int)base.projectile.position.X, (int)base.projectile.position.Y);
				int proj2 = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), base.mod.ProjectileType("GirusDischarge2"), 100, 0f, Main.myPlayer, 0f, 0f);
				Main.projectile[proj2].netUpdate = true;
			}
			this.Move(new Vector2(this.vector.X, this.vector.Y));
		}

		public void Move(Vector2 offset)
		{
			Entity entity = Main.player[base.projectile.owner];
			this.speed = 11f;
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

		private Vector2 vector2;

		private NPC target;

		private Projectile clearCheck;

		public int timer;
	}
}
