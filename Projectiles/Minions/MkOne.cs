using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace Redemption.Projectiles.Minions
{
	public abstract class MkOne : Minion
	{
		public virtual void CreateDust()
		{
		}

		public virtual void SelectFrame()
		{
		}

		public override void Behavior()
		{
			Player player = Main.player[base.projectile.owner];
			float spacing = (float)base.projectile.width * this.spacingMult;
			for (int i = 0; i < 1000; i++)
			{
				Projectile otherProj = Main.projectile[i];
				if (i != base.projectile.whoAmI && otherProj.active && otherProj.owner == base.projectile.owner && otherProj.type == base.projectile.type && Math.Abs(base.projectile.position.X - otherProj.position.X) + Math.Abs(base.projectile.position.Y - otherProj.position.Y) < spacing)
				{
					if (base.projectile.position.X < Main.projectile[i].position.X)
					{
						Projectile projectile = base.projectile;
						projectile.velocity.X = projectile.velocity.X - this.idleAccel;
					}
					else
					{
						Projectile projectile2 = base.projectile;
						projectile2.velocity.X = projectile2.velocity.X + this.idleAccel;
					}
					if (base.projectile.position.Y < Main.projectile[i].position.Y)
					{
						Projectile projectile3 = base.projectile;
						projectile3.velocity.Y = projectile3.velocity.Y - this.idleAccel;
					}
					else
					{
						Projectile projectile4 = base.projectile;
						projectile4.velocity.Y = projectile4.velocity.Y + this.idleAccel;
					}
				}
			}
			Vector2 targetPos = base.projectile.position;
			float targetDist = this.viewDist;
			bool target = false;
			base.projectile.tileCollide = true;
			for (int j = 0; j < 200; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.CanBeChasedBy(this, false))
				{
					float distance = Vector2.Distance(npc.Center, base.projectile.Center);
					if ((distance < targetDist || !target) && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = distance;
						targetPos = npc.Center;
						target = true;
					}
				}
			}
			if (Vector2.Distance(player.Center, base.projectile.Center) > (target ? 1000f : 500f))
			{
				base.projectile.ai[0] = 1f;
				base.projectile.netUpdate = true;
			}
			if (base.projectile.ai[0] == 1f)
			{
				base.projectile.tileCollide = false;
			}
			if (target && base.projectile.ai[0] == 0f)
			{
				Vector2 direction = targetPos - base.projectile.Center;
				if (direction.Length() > this.chaseDist)
				{
					direction.Normalize();
					base.projectile.velocity = (base.projectile.velocity * this.inertia + direction * this.chaseAccel) / (this.inertia + 1f);
				}
				else
				{
					base.projectile.velocity *= (float)Math.Pow(0.97, 40.0 / (double)this.inertia);
				}
			}
			else
			{
				if (!Collision.CanHitLine(base.projectile.Center, 1, 1, player.Center, 1, 1))
				{
					base.projectile.ai[0] = 1f;
				}
				float speed = 6f;
				if (base.projectile.ai[0] == 1f)
				{
					speed = 15f;
				}
				Vector2 center = base.projectile.Center;
				Vector2 direction2 = player.Center - center;
				base.projectile.ai[1] = 3600f;
				base.projectile.netUpdate = true;
				int num = 1;
				for (int k = 0; k < base.projectile.whoAmI; k++)
				{
					if (Main.projectile[k].active && Main.projectile[k].owner == base.projectile.owner && Main.projectile[k].type == base.projectile.type)
					{
						num++;
					}
				}
				direction2.X -= (float)((10 + num * 40) * player.direction);
				direction2.Y -= 70f;
				float num2 = direction2.Length();
				if (num2 > 200f && speed < 9f)
				{
					speed = 9f;
				}
				if (num2 < 100f && base.projectile.ai[0] == 1f && !Collision.SolidCollision(base.projectile.position, base.projectile.width, base.projectile.height))
				{
					base.projectile.ai[0] = 0f;
					base.projectile.netUpdate = true;
				}
				if (num2 > 2000f)
				{
					base.projectile.Center = player.Center;
				}
				if (num2 > 48f)
				{
					direction2.Normalize();
					direction2 *= speed;
					float temp = this.inertia / 2f;
					base.projectile.velocity = (base.projectile.velocity * temp + direction2) / (temp + 1f);
				}
				else
				{
					base.projectile.direction = Main.player[base.projectile.owner].direction;
					base.projectile.velocity *= (float)Math.Pow(0.9, 40.0 / (double)this.inertia);
				}
			}
			base.projectile.rotation = base.projectile.velocity.X * 0.05f;
			this.SelectFrame();
			this.CreateDust();
			if (base.projectile.velocity.X > 0f)
			{
				base.projectile.spriteDirection = (base.projectile.direction = -1);
			}
			else if (base.projectile.velocity.X < 0f)
			{
				base.projectile.spriteDirection = (base.projectile.direction = 1);
			}
			if (base.projectile.ai[1] > 0f)
			{
				base.projectile.ai[1] += 1f;
				if (Main.rand.Next(3) == 0)
				{
					base.projectile.ai[1] += 1f;
				}
			}
			if (base.projectile.ai[1] > this.shootCool)
			{
				base.projectile.ai[1] = 0f;
				base.projectile.netUpdate = true;
			}
			if (base.projectile.ai[0] == 0f && target)
			{
				if ((targetPos - base.projectile.Center).X > 0f)
				{
					base.projectile.spriteDirection = (base.projectile.direction = -1);
				}
				else if ((targetPos - base.projectile.Center).X < 0f)
				{
					base.projectile.spriteDirection = (base.projectile.direction = 1);
				}
				if (base.projectile.ai[1] == 0f)
				{
					base.projectile.ai[1] = 1f;
					if (Main.myPlayer == base.projectile.owner)
					{
						Vector2 shootVel = targetPos - base.projectile.Center;
						if (shootVel == Vector2.Zero)
						{
							shootVel = new Vector2(0f, 1f);
						}
						shootVel.Normalize();
						shootVel *= this.shootSpeed;
						int proj = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, shootVel.X, shootVel.Y, this.shoot, base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
						Main.projectile[proj].timeLeft = 300;
						Main.projectile[proj].netUpdate = true;
						base.projectile.netUpdate = true;
					}
				}
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = true;
			return true;
		}

		protected float idleAccel = 0.25f;

		protected float spacingMult = 1f;

		protected float viewDist = 300f;

		protected float chaseDist = 200f;

		protected float chaseAccel = 16f;

		protected float inertia = 40f;

		protected float shootCool = 8f;

		protected float shootSpeed;

		protected int shoot;
	}
}
