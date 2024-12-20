using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class ShieldDrone : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Dome");
			Main.projFrames[base.projectile.type] = 4;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = false;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
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

		private void LookInDirection(Vector2 look)
		{
			float angle = 1.5707964f;
			if (look.X != 0f)
			{
				angle = (float)Math.Atan((double)(look.Y / look.X));
			}
			else if (look.Y < 0f)
			{
				angle += 3.1415927f;
			}
			if (look.X < 0f)
			{
				angle += 3.1415927f;
			}
			base.projectile.rotation = angle - 1.5707964f;
		}

		public override void AI()
		{
			if (this.shieldDisabled)
			{
				base.projectile.frame = 3;
			}
			else
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
					if (num >= 3)
					{
						base.projectile.frame = 0;
					}
				}
				Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.7f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			}
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead || !player.HasBuff(ModContent.BuffType<ShieldDroneBuff>()))
			{
				modPlayer.shieldDrone = false;
				base.projectile.Kill();
			}
			if (modPlayer.shieldDrone)
			{
				base.projectile.timeLeft = 2;
			}
			if (!this.iLoveRedemption)
			{
				Vector2 playerDir = base.projectile.Center - player.Center;
				this.LookInDirection(-playerDir);
				this.Move(new Vector2(0f, 0f), player.Center, 30f, 17f);
				this.distance = 1000f;
			}
			base.projectile.localAI[1] += 1f;
			for (int i = 0; i < 200; i++)
			{
				if (Main.projectile[i].active && !Main.projectile[i].friendly && Main.projectile[i].type != ModContent.ProjectileType<ShieldDrone>() && Main.projectile[i].hostile)
				{
					Rectangle rectangle = new Rectangle((int)Main.projectile[i].position.X, (int)Main.projectile[i].position.Y, Main.projectile[i].width, Main.projectile[i].height);
					Rectangle rectangle2 = new Rectangle((int)base.projectile.position.X, (int)base.projectile.position.Y, base.projectile.width, base.projectile.height);
					if (rectangle.Intersects(rectangle2) && !this.shieldDisabled)
					{
						for (int j = 0; j < 3; j++)
						{
							int p = Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(base.projectile.Center - player.Center) + Utils.NextFloat(Main.rand, -0.05f, 0.05f)), ModContent.ProjectileType<GirusDischarge>(), 180, 3f, Main.myPlayer, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						Main.projectile[i].velocity *= -1f;
						Main.projectile[i].damage = Main.projectile[i].damage * 4;
						Main.projectile[i].friendly = true;
						Main.projectile[i].hostile = false;
						base.projectile.localAI[0] += (float)Main.projectile[i].damage * 0.75f;
						Main.PlaySound(SoundID.NPCHit34, base.projectile.position);
						if (base.projectile.localAI[0] >= 500f)
						{
							Main.PlaySound(SoundID.NPCDeath56, base.projectile.position);
							for (int k = 0; k < 7; k++)
							{
								int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 1.5f);
								Main.dust[dustIndex].velocity *= 1.9f;
							}
							this.shieldDisabled = true;
						}
					}
					float num2 = Main.projectile[i].position.X + (float)Main.projectile[i].width * 0.5f - base.projectile.Center.X;
					float shootToY = Main.projectile[i].position.Y - base.projectile.Center.Y;
					float distanceNew = (float)Math.Sqrt((double)(num2 * num2 + shootToY * shootToY));
					if (distanceNew < this.distance)
					{
						this.distance = distanceNew;
						this.iLoveRedemption = true;
						Vector2 distFalseMag = Main.projectile[i].Center - player.Center;
						Vector2 playerFalseMag = base.projectile.Center - player.Center;
						this.LookInDirection(-playerFalseMag);
						float distFromPlayer = 60f;
						float distTrueMag = Vector2.Distance(Main.projectile[i].Center, player.Center);
						if (((Main.projectile[i].velocity.X > 0f && distFalseMag.X < 0f) || (Main.projectile[i].velocity.X < 0f && distFalseMag.X > 0f && Main.projectile[i].type != ModContent.ProjectileType<ShieldDrone>()) || Math.Abs(Main.projectile[i].velocity.X) < 1f) && ((Main.projectile[i].velocity.Y > 0f && distFalseMag.Y < 0f) || (Main.projectile[i].velocity.Y < 0f && distFalseMag.Y > 0f) || Math.Abs(Main.projectile[i].velocity.Y) < 1f) && base.projectile.localAI[1] % 20f == 0f)
						{
							this.moveTo = new Vector2(distFalseMag.X * distFromPlayer / distTrueMag, distFalseMag.Y * distFromPlayer / distTrueMag);
						}
						this.Move(new Vector2(0f, 0f), player.Center + this.moveTo, 10f, 10f);
					}
					else
					{
						this.iLoveRedemption = false;
					}
				}
				else
				{
					this.iLoveRedemption = false;
				}
			}
			if (this.shieldDisabled)
			{
				float[] localAI = base.projectile.localAI;
				int num3 = 0;
				float num4 = localAI[num3] + 1f;
				localAI[num3] = num4;
				if (num4 >= 1105f)
				{
					this.shieldDisabled = false;
					base.projectile.localAI[0] = 0f;
				}
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

		public bool iLoveRedemption;

		public float distance = 1000f;

		public bool shieldDisabled;

		public int shieldAlpha;

		private Vector2 moveTo;
	}
}
