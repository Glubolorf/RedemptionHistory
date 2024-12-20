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

		public static void LookAt(Vector2 lookTarget, Entity c, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.1f, bool flipSpriteDir = false)
		{
			int spriteDirection = (c is NPC) ? ((NPC)c).spriteDirection : ((c is Projectile) ? ((Projectile)c).spriteDirection : 0);
			float rotation = (c is NPC) ? ((NPC)c).rotation : ((c is Projectile) ? ((Projectile)c).rotation : 0f);
			MageDrone.LookAt(lookTarget, c.Center, ref rotation, ref spriteDirection, lookType, rotAddon, rotAmount, flipSpriteDir);
			if (c is NPC)
			{
				((NPC)c).spriteDirection = spriteDirection;
				((NPC)c).rotation = rotation;
				return;
			}
			if (c is Projectile)
			{
				((Projectile)c).spriteDirection = spriteDirection;
				((Projectile)c).rotation = rotation;
			}
		}

		public static void LookAt(Vector2 lookTarget, Vector2 center, ref float rotation, ref int spriteDirection, int lookType = 0, float rotAddon = 0f, float rotAmount = 0.075f, bool flipSpriteDir = false)
		{
			if (lookType == 0)
			{
				if (lookTarget.X > center.X)
				{
					spriteDirection = -1;
				}
				else
				{
					spriteDirection = 1;
				}
				if (flipSpriteDir)
				{
					spriteDirection *= -1;
				}
				float rotX = lookTarget.X - center.X;
				float rotY = lookTarget.Y - center.Y;
				rotation = -((float)Math.Atan2((double)rotX, (double)rotY) - 1.57f + rotAddon);
				if (spriteDirection == 1)
				{
					rotation -= 3.1415927f;
					return;
				}
			}
			else if (lookType == 1)
			{
				if (lookTarget.X > center.X)
				{
					spriteDirection = -1;
				}
				else
				{
					spriteDirection = 1;
				}
				if (flipSpriteDir)
				{
					spriteDirection *= -1;
					return;
				}
			}
			else
			{
				if (lookType == 2)
				{
					float rotX2 = lookTarget.X - center.X;
					float rotY2 = lookTarget.Y - center.Y;
					rotation = -((float)Math.Atan2((double)rotX2, (double)rotY2) - 1.57f + rotAddon);
					return;
				}
				if (lookType == 3 || lookType == 4)
				{
					int num = spriteDirection;
					if (lookType == 3 && lookTarget.X > center.X)
					{
						spriteDirection = -1;
					}
					else
					{
						spriteDirection = 1;
					}
					if (lookType == 3 && flipSpriteDir)
					{
						spriteDirection *= -1;
					}
					if (num != spriteDirection)
					{
						rotation += 3.1415927f * (float)spriteDirection;
					}
					float pi2 = 6.2831855f;
					float rotX3 = lookTarget.X - center.X;
					float rot = (float)Math.Atan2((double)(lookTarget.Y - center.Y), (double)rotX3) + rotAddon;
					if (spriteDirection == 1)
					{
						rot += 3.1415927f;
					}
					if (rot > pi2)
					{
						rot -= pi2;
					}
					else if (rot < 0f)
					{
						rot += pi2;
					}
					if (rotation > pi2)
					{
						rotation -= pi2;
					}
					else if (rotation < 0f)
					{
						rotation += pi2;
					}
					if (rotation < rot)
					{
						if ((double)(rot - rotation) > 3.1415927410125732)
						{
							rotation -= rotAmount;
						}
						else
						{
							rotation += rotAmount;
						}
					}
					else if (rotation > rot)
					{
						if ((double)(rotation - rot) > 3.1415927410125732)
						{
							rotation += rotAmount;
						}
						else
						{
							rotation -= rotAmount;
						}
					}
					if (rotation > rot - rotAmount && rotation < rot + rotAmount)
					{
						rotation = rot;
					}
				}
			}
		}

		public override void SetDefaults()
		{
			base.projectile.width = 38;
			base.projectile.height = 38;
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
			if (base.projectile.frame < 4)
			{
				base.projectile.frameCounter++;
				if (base.projectile.frameCounter >= 4)
				{
					base.projectile.frameCounter = 0;
					base.projectile.frame++;
					base.projectile.frame %= 4;
				}
			}
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.ai[1] == 0f)
			{
				this.projecc = 0;
				this.speed1 = 4f;
				base.projectile.ai[0] += 0.08f;
				base.projectile.rotation -= base.projectile.rotation / 20f;
				Projectile projectile = base.projectile;
				projectile.velocity.Y = projectile.velocity.Y + (float)Math.Sin((double)base.projectile.ai[0]) / 10f;
				this.Move(new Vector2(30f, -30f), player.Center, 20f, this.speed1);
			}
			if (Vector2.Distance(base.projectile.Center, player.Center) > 300f)
			{
				base.projectile.ai[1] = 1f;
			}
			else if (!this.iLoveRedemption && this.projecc % 2 == 0 && this.projecc != 0)
			{
				base.projectile.ai[1] = 0f;
			}
			if (this.iLoveRedemption && base.projectile.ai[1] == 1f)
			{
				this.speed1 = 1f;
				base.projectile.velocity *= 0.982f;
				if (this.Magnitude(base.projectile.velocity) < 5f)
				{
					this.projecc++;
					this.iLoveRedemption = false;
				}
			}
			if (!this.iLoveRedemption && base.projectile.ai[1] == 1f)
			{
				MageDrone.LookAt(base.projectile.Center + base.projectile.velocity, base.projectile, 4, -1.5707964f, 0.1f, false);
				this.speed1 += 0.9f;
				this.Move(new Vector2(30f, -30f), player.Center, 30f, this.speed1);
				if (this.speed1 >= 26f + Vector2.Distance(base.projectile.Center, player.Center) / 50f)
				{
					this.projecc++;
					this.iLoveRedemption = true;
				}
			}
			if (Vector2.Distance(base.projectile.Center, player.Center) > 2000f)
			{
				base.projectile.position = player.Center + new Vector2(30f, 30f);
				base.projectile.alpha = 255;
			}
			else
			{
				base.projectile.alpha -= 5;
				if (base.projectile.alpha < 0)
				{
					base.projectile.alpha = 0;
				}
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 % 180f == 0f)
			{
				double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector.X = (float)(Math.Sin(angle) * 200.0);
				this.vector.Y = (float)(Math.Cos(angle) * 200.0);
			}
			for (int i = 0; i < 4; i++)
			{
				double angle2 = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
				this.vector2.X = (float)(Math.Sin(angle2) * 200.0);
				this.vector2.Y = (float)(Math.Cos(angle2) * 200.0);
				Dust dust = Main.dust[Dust.NewDust(base.projectile.Center + this.vector2, 2, 2, 235, 0f, 0f, 100, default(Color), 1f)];
				dust.noGravity = true;
				dust.velocity *= 0f;
			}
			base.projectile.localAI[1] += 1f;
			for (int p = 0; p < 200; p++)
			{
				this.clearCheck = Main.projectile[p];
			}
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead || !modPlayer.jellyfishDrone)
			{
				base.projectile.Kill();
			}
			if (modPlayer.jellyfishDrone)
			{
				base.projectile.timeLeft = 2;
			}
			if (Vector2.Distance(base.projectile.Center, this.clearCheck.Center) < 200f && this.clearCheck.whoAmI != base.projectile.whoAmI && base.projectile.localAI[1] % 120f == 0f)
			{
				Main.PlaySound(SoundID.Item93, (int)base.projectile.position.X, (int)base.projectile.position.Y);
				int proj = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.clearCheck.Center - base.projectile.Center)), ModContent.ProjectileType<GirusDischarge2>(), 100, 0f, Main.myPlayer, 0f, 0f);
				Main.projectile[proj].netUpdate = true;
			}
			if (RedeHelper.ClosestNPC(ref this.target, 200f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[1] % 120f == 0f)
			{
				Main.PlaySound(SoundID.Item93, (int)base.projectile.position.X, (int)base.projectile.position.Y);
				int proj2 = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<GirusDischarge2>(), 100, 0f, Main.myPlayer, 0f, 0f);
				Main.projectile[proj2].netUpdate = true;
			}
		}

		public void Move(Vector2 offset, Vector2 moveTo1, float resistance, float speed2)
		{
			Player player = Main.player[base.projectile.owner];
			this.speed = speed2;
			Vector2 move = moveTo1 + offset - base.projectile.Center;
			float magnitude = this.Magnitude(move);
			if (magnitude > this.speed)
			{
				move *= this.speed / magnitude;
			}
			move = (base.projectile.velocity * resistance + move) / (resistance + 1f);
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

		public bool iLoveRedemption;

		public float distance = 1000f;

		public float speed1 = 2f;

		public int projecc;
	}
}
