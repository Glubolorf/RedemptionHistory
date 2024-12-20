using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class CorruptedCopter : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Attack Minicopter");
			Main.projFrames[base.projectile.type] = 2;
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
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead || !player.HasBuff(ModContent.BuffType<CorruptedCopterBuff>()))
			{
				modPlayer.corruptedCopter = false;
				base.projectile.Kill();
			}
			if (modPlayer.corruptedCopter)
			{
				base.projectile.timeLeft = 2;
			}
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			float[] localAI = base.projectile.localAI;
			int num2 = 0;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 % 120f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 300.0);
				this.vector.Y = (float)(Math.Cos(angle) * 100.0);
				base.projectile.localAI[0] = 1f;
			}
			base.projectile.localAI[1] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 1000f, base.projectile.Center, false, player.MinionAttackTargetNPC))
			{
				Vector2 AttackPos = new Vector2((float)((base.projectile.Center.X > this.target.Center.X) ? 100 : -100), -100f);
				this.MoveToVector3(AttackPos);
				if (this.target.Center.X > base.projectile.Center.X)
				{
					base.projectile.spriteDirection = -1;
				}
				else
				{
					base.projectile.spriteDirection = 1;
				}
				if (Main.rand.Next(400) == 0)
				{
					Main.PlaySound(SoundID.Item74, (int)base.projectile.position.X, (int)base.projectile.position.Y);
					int p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), new Vector2(0f, -10f), ModContent.ProjectileType<OmegaMissileMini>(), 180, 6f, Main.myPlayer, 0f, 0f);
					Main.projectile[p].netUpdate = true;
				}
				if (Main.rand.Next(100) == 0 && this.timer == 0)
				{
					this.timer = 1;
				}
				if (this.timer >= 1)
				{
					if (base.projectile.localAI[1] % 2f == 0f)
					{
						int p2 = Projectile.NewProjectile(new Vector2((base.projectile.spriteDirection == 1) ? (base.projectile.position.X + 11f) : (base.projectile.position.X + 37f), base.projectile.position.Y + 31f), RedeHelper.PolarVector(12f, Utils.ToRotation(this.target.Center - base.projectile.Center) + Utils.NextFloat(Main.rand, -0.05f, 0.05f)), 14, 55, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
						Main.projectile[p2].ranged = false;
						Main.projectile[p2].minion = true;
					}
					this.timer++;
					if (this.timer >= 40)
					{
						this.timer = 0;
						return;
					}
				}
			}
			else
			{
				this.Move(new Vector2(this.vector.X, this.vector.Y - 200f));
				if (player.Center.X > base.projectile.Center.X)
				{
					base.projectile.spriteDirection = 1;
					return;
				}
				base.projectile.spriteDirection = -1;
			}
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

		public void MoveToVector3(Vector2 p)
		{
			float moveSpeed = 17f;
			Vector2 move = this.target.Center + p - base.projectile.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > moveSpeed)
			{
				move *= moveSpeed / magnitude;
			}
			float turnResistance = 30f;
			move = (base.projectile.velocity * turnResistance + move) / (turnResistance + 1f);
			magnitude = this.Magnitude(move);
			if (magnitude > moveSpeed)
			{
				move *= moveSpeed / magnitude;
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

		public int timer;
	}
}
