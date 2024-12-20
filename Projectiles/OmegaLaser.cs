using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class OmegaLaser : ModProjectile
	{
		public float Distance
		{
			get
			{
				return base.projectile.ai[0];
			}
			set
			{
				base.projectile.ai[0] = value;
			}
		}

		public float Charge
		{
			get
			{
				return base.projectile.localAI[0];
			}
			set
			{
				base.projectile.localAI[0] = value;
			}
		}

		public bool AtMaxCharge
		{
			get
			{
				return this.Charge == 50f;
			}
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.magic = true;
			base.projectile.hide = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (this.AtMaxCharge)
			{
				this.DrawLaser(spriteBatch, Main.projectileTexture[base.projectile.type], Main.player[base.projectile.owner].Center, base.projectile.velocity, 10f, base.projectile.damage, -1.57f, 1f, 1000f, Color.White, 60);
			}
			return false;
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float num = Utils.ToRotation(unit) + rotation;
			for (float num2 = (float)transDist; num2 <= this.Distance; num2 += step)
			{
				Color white = Color.White;
				Vector2 vector = start + num2 * unit;
				spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, 26, 28, 26)), (num2 < (float)transDist) ? Color.Transparent : white, num, new Vector2(14f, 13f), scale, 0, 0f);
			}
			spriteBatch.Draw(texture, start + unit * ((float)transDist - step) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 28, 26)), Color.White, num, new Vector2(14f, 13f), scale, 0, 0f);
			spriteBatch.Draw(texture, start + (this.Distance + step) * unit - Main.screenPosition, new Rectangle?(new Rectangle(0, 52, 28, 26)), Color.White, num, new Vector2(14f, 13f), scale, 0, 0f);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (this.AtMaxCharge)
			{
				Player player = Main.player[base.projectile.owner];
				Vector2 velocity = base.projectile.velocity;
				float num = 0f;
				return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), player.Center, player.Center + velocity * this.Distance, 22f, ref num));
			}
			return new bool?(false);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override void AI()
		{
			Vector2 mouseWorld = Main.MouseWorld;
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.owner == Main.myPlayer)
			{
				Vector2 velocity = mouseWorld - player.Center;
				velocity.Normalize();
				base.projectile.velocity = velocity;
				base.projectile.direction = ((Main.MouseWorld.X > player.position.X) ? 1 : -1);
				base.projectile.netUpdate = true;
			}
			base.projectile.position = player.Center + base.projectile.velocity * 60f;
			base.projectile.timeLeft = 2;
			int direction = base.projectile.direction;
			player.ChangeDir(direction);
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(base.projectile.velocity.Y * (float)direction), (double)(base.projectile.velocity.X * (float)direction));
			if (!player.channel)
			{
				base.projectile.Kill();
			}
			else
			{
				if (Main.time % 10.0 < 1.0 && !player.CheckMana(player.inventory[player.selectedItem].mana, true, false))
				{
					base.projectile.Kill();
				}
				Vector2 vector = base.projectile.velocity;
				vector *= 40f;
				Vector2 vector2 = player.Center + vector - new Vector2(10f, 10f);
				if (this.Charge < 50f)
				{
					this.Charge += 1f;
				}
				int num = (int)(this.Charge / 20f);
				Vector2 vector3 = Vector2.UnitX * 18f;
				vector3 = Utils.RotatedBy(vector3, (double)(base.projectile.rotation - 1.57f), default(Vector2));
				Vector2 vector4 = base.projectile.Center + vector3;
				for (int i = 0; i < num + 1; i++)
				{
					Vector2 vector5 = vector4 + Utils.ToRotationVector2((float)Main.rand.NextDouble() * 6.28f) * (12f - (float)(num * 2));
					Dust dust = Main.dust[Dust.NewDust(vector2, 20, 20, 235, base.projectile.velocity.X / 2f, base.projectile.velocity.Y / 2f, 0, default(Color), 1f)];
					dust.velocity = Vector2.Normalize(vector4 - vector5) * 1.5f * (10f - (float)num * 2f) / 10f;
					dust.noGravity = true;
					dust.scale = (float)Main.rand.Next(10, 20) * 0.05f;
				}
			}
			if (this.Charge < 50f)
			{
				return;
			}
			Vector2 vector6 = player.Center;
			Vector2 vector7 = base.projectile.velocity;
			vector7 *= -1f;
			this.Distance = 60f;
			while (this.Distance <= 2200f)
			{
				vector6 = player.Center + base.projectile.velocity * this.Distance;
				if (!Collision.CanHit(player.Center, 1, 1, vector6, 1, 1))
				{
					this.Distance -= 5f;
					break;
				}
				this.Distance += 5f;
			}
			Vector2 vector8 = player.Center + base.projectile.velocity * this.Distance;
			for (int j = 0; j < 2; j++)
			{
				float num2 = Utils.ToRotation(base.projectile.velocity) + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57f;
				float num3 = (float)(Main.rand.NextDouble() * 0.800000011920929 + 1.0);
				Vector2 vector9;
				vector9..ctor((float)Math.Cos((double)num2) * num3, (float)Math.Sin((double)num2) * num3);
				Dust dust2 = Main.dust[Dust.NewDust(vector8, 0, 0, 235, vector9.X, vector9.Y, 0, default(Color), 1f)];
				dust2.noGravity = true;
				dust2.scale = 1.2f;
				dust2 = Dust.NewDustDirect(Main.player[base.projectile.owner].Center, 0, 0, 235, -vector7.X * this.Distance, -vector7.Y * this.Distance, 0, default(Color), 1f);
				dust2.fadeIn = 0f;
				dust2.noGravity = true;
				dust2.scale = 0.88f;
				dust2.color = Color.Red;
			}
			if (Main.rand.Next(5) == 0)
			{
				Vector2 vector10 = Utils.RotatedBy(base.projectile.velocity, 1.5700000524520874, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.projectile.width;
				Dust dust3 = Main.dust[Dust.NewDust(vector8 + vector10 - Vector2.One * 4f, 8, 8, 235, 0f, 0f, 100, default(Color), 1.5f)];
				dust3.velocity *= 0.5f;
				dust3.velocity.Y = -Math.Abs(dust3.velocity.Y);
				vector7 = vector8 - Main.player[base.projectile.owner].Center;
				vector7.Normalize();
				dust3 = Main.dust[Dust.NewDust(Main.player[base.projectile.owner].Center + 55f * vector7, 8, 8, 235, 0f, 0f, 100, default(Color), 1.5f)];
				dust3.velocity *= 0.5f;
				dust3.velocity.Y = -Math.Abs(dust3.velocity.Y);
			}
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + base.projectile.velocity * (this.Distance - 60f), 26f, new Utils.PerLinePoint(DelegateMethods.CastLight));
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = 2;
			Vector2 velocity = base.projectile.velocity;
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + velocity * this.Distance, (float)(base.projectile.width + 16) * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		private const float MaxChargeValue = 50f;

		private const float MoveDistance = 60f;
	}
}
