﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
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
			float r = Utils.ToRotation(unit) + rotation;
			for (float i = (float)transDist; i <= this.Distance; i += step)
			{
				Color c = Color.White;
				Vector2 origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle?(new Rectangle(0, 26, 28, 26)), (i < (float)transDist) ? Color.Transparent : c, r, new Vector2(14f, 13f), scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(texture, start + unit * ((float)transDist - step) - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, 28, 26)), Color.White, r, new Vector2(14f, 13f), scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, start + (this.Distance + step) * unit - Main.screenPosition, new Rectangle?(new Rectangle(0, 52, 28, 26)), Color.White, r, new Vector2(14f, 13f), scale, SpriteEffects.None, 0f);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (this.AtMaxCharge)
			{
				Player player = Main.player[base.projectile.owner];
				Vector2 unit = base.projectile.velocity;
				float point = 0f;
				return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), player.Center, player.Center + unit * this.Distance, 22f, ref point));
			}
			return new bool?(false);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		public override void AI()
		{
			Vector2 mousePos = Main.MouseWorld;
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.owner == Main.myPlayer)
			{
				Vector2 diff = mousePos - player.Center;
				diff.Normalize();
				base.projectile.velocity = diff;
				base.projectile.direction = ((Main.MouseWorld.X > player.position.X) ? 1 : -1);
				base.projectile.netUpdate = true;
			}
			base.projectile.position = player.Center + base.projectile.velocity * 60f;
			base.projectile.timeLeft = 2;
			int dir = base.projectile.direction;
			player.ChangeDir(dir);
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(base.projectile.velocity.Y * (float)dir), (double)(base.projectile.velocity.X * (float)dir));
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
				Vector2 offset = base.projectile.velocity;
				offset *= 40f;
				Vector2 pos = player.Center + offset - new Vector2(10f, 10f);
				if (this.Charge < 50f)
				{
					float charge = this.Charge;
					this.Charge = charge + 1f;
				}
				int chargeFact = (int)(this.Charge / 20f);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = Utils.RotatedBy(dustVelocity, (double)(base.projectile.rotation - 1.57f), default(Vector2));
				Vector2 spawnPos = base.projectile.Center + dustVelocity;
				for (int i = 0; i < chargeFact + 1; i++)
				{
					Vector2 spawn = spawnPos + Utils.ToRotationVector2((float)Main.rand.NextDouble() * 6.28f) * (12f - (float)(chargeFact * 2));
					Dust dust2 = Main.dust[Dust.NewDust(pos, 20, 20, 235, base.projectile.velocity.X / 2f, base.projectile.velocity.Y / 2f, 0, default(Color), 1f)];
					dust2.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - (float)chargeFact * 2f) / 10f;
					dust2.noGravity = true;
					dust2.scale = (float)Main.rand.Next(10, 20) * 0.05f;
				}
			}
			if (this.Charge < 50f)
			{
				return;
			}
			Vector2 start = player.Center;
			Vector2 unit = base.projectile.velocity;
			unit *= -1f;
			this.Distance = 60f;
			while (this.Distance <= 2200f)
			{
				start = player.Center + base.projectile.velocity * this.Distance;
				if (!Collision.CanHitLine(player.Center, 1, 1, start, 1, 1))
				{
					this.Distance -= 5f;
					break;
				}
				this.Distance += 5f;
			}
			Vector2 dustPos = player.Center + base.projectile.velocity * this.Distance;
			for (int j = 0; j < 2; j++)
			{
				float num = Utils.ToRotation(base.projectile.velocity) + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.800000011920929 + 1.0);
				Vector2 dustVel = new Vector2((float)Math.Cos((double)num) * num2, (float)Math.Sin((double)num) * num2);
				Dust dust3 = Main.dust[Dust.NewDust(dustPos, 0, 0, 235, dustVel.X, dustVel.Y, 0, default(Color), 1f)];
				dust3.noGravity = true;
				dust3.scale = 1.2f;
				Dust dust4 = Dust.NewDustDirect(Main.player[base.projectile.owner].Center, 0, 0, 235, -unit.X * this.Distance, -unit.Y * this.Distance, 0, default(Color), 1f);
				dust4.fadeIn = 0f;
				dust4.noGravity = true;
				dust4.scale = 0.88f;
				dust4.color = Color.Red;
			}
			if (Main.rand.Next(5) == 0)
			{
				Vector2 offset2 = Utils.RotatedBy(base.projectile.velocity, 1.5700000524520874, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset2 - Vector2.One * 4f, 8, 8, 235, 0f, 0f, 100, default(Color), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
				unit = dustPos - Main.player[base.projectile.owner].Center;
				unit.Normalize();
				dust = Main.dust[Dust.NewDust(Main.player[base.projectile.owner].Center + 55f * unit, 8, 8, 235, 0f, 0f, 100, default(Color), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
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
			Vector2 unit = base.projectile.velocity;
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + unit * this.Distance, (float)(base.projectile.width + 16) * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		private const float MaxChargeValue = 50f;

		private const float MoveDistance = 60f;
	}
}
