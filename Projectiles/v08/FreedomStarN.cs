using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class FreedomStarN : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Freedom Star");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 74;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.ranged = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			float num = 1.5707964f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			base.projectile.ai[0] += 1f;
			int num2 = 0;
			if (base.projectile.ai[0] >= 30f)
			{
				num2++;
			}
			if (base.projectile.ai[0] >= 60f)
			{
				num2++;
			}
			if (base.projectile.ai[0] >= 90f)
			{
				num2++;
			}
			int num3 = 24;
			int num4 = 6;
			base.projectile.ai[1] += 1f;
			bool flag = false;
			if (base.projectile.ai[1] >= (float)(num3 - num4 * num2))
			{
				base.projectile.ai[1] = 0f;
				flag = true;
			}
			if (base.projectile.ai[1] == 1f && base.projectile.ai[0] != 1f)
			{
				Vector2 vector2 = Vector2.UnitX * 24f;
				vector2 = Utils.RotatedBy(vector2, (double)(base.projectile.rotation - 1.5707964f), default(Vector2));
				Vector2 value = base.projectile.Center + vector2;
				for (int i = 0; i < 3; i++)
				{
					int num5 = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 58, base.projectile.velocity.X / 2f, base.projectile.velocity.Y / 2f, 100, default(Color), 1f);
					Dust dust = Main.dust[num5];
					dust.position.Y = dust.position.Y - 0.3f;
					Main.dust[num5].velocity *= 0.66f;
					Main.dust[num5].noGravity = true;
					Main.dust[num5].scale = 1.4f;
				}
			}
			if (flag && Main.myPlayer == base.projectile.owner && player.channel && !player.noItems && !player.CCed)
			{
				float scaleFactor = player.inventory[player.selectedItem].shootSpeed * base.projectile.scale;
				Vector2 vector3 = vector;
				Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
				if (player.gravDir == -1f)
				{
					value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
				}
				Vector2 vector4 = Vector2.Normalize(value2);
				if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
				{
					vector4 = -Vector2.UnitY;
				}
				vector4 *= scaleFactor;
				if (vector4.X != base.projectile.velocity.X || vector4.Y != base.projectile.velocity.Y)
				{
					base.projectile.netUpdate = true;
				}
				base.projectile.velocity = vector4;
				float scaleFactor2 = 14f;
				int num6 = 7;
				vector3 = base.projectile.Center + new Vector2((float)Main.rand.Next(-num6, num6 + 1), (float)Main.rand.Next(-num6, num6 + 1));
				Vector2 vector5 = Vector2.Normalize(base.projectile.velocity) * scaleFactor2;
				vector5 = Utils.RotatedBy(vector5, Main.rand.NextDouble() * 0.19634954631328583 - 0.09817477315664291, default(Vector2));
				if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
				{
					vector5 = -Vector2.UnitY;
				}
			}
			base.projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - base.projectile.Size / 2f;
			base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + num;
			base.projectile.spriteDirection = base.projectile.direction;
			base.projectile.timeLeft = 2;
			player.ChangeDir(base.projectile.direction);
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(base.projectile.velocity.Y * (float)base.projectile.direction), (double)(base.projectile.velocity.X * (float)base.projectile.direction));
			this.counter++;
			if (this.counter >= 90)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 93, 1f, 0f);
				this.chargeLevel = 3;
			}
			else if (this.counter >= 90)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 101, 1f, 0f);
				this.chargeLevel = 2;
			}
			else if (this.counter >= 30)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 13, 1f, 0f);
				this.chargeLevel = 1;
			}
			else if (this.counter >= 30)
			{
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 13, 1f, 0f);
				this.chargeLevel = 0;
			}
			if (!player.channel)
			{
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.owner == Main.myPlayer)
			{
				float num = 12f;
				Vector2 vector2 = new Vector2(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
				float f = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				float f2 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				if ((double)player.gravDir == -1.0)
				{
					f2 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector2.Y;
				}
				float num2 = (float)Math.Sqrt((double)f * (double)f + (double)f2 * (double)f2);
				float num3;
				if ((float.IsNaN(f) && float.IsNaN(f2)) || ((double)f == 0.0 && (double)f2 == 0.0))
				{
					f = (float)player.direction;
					f2 = 0f;
					num3 = num;
				}
				else
				{
					num3 = num / num2;
				}
				float SpeedX = f * num3;
				float SpeedY = f2 * num3;
				switch (this.chargeLevel)
				{
				case 0:
					Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 89, 1f, 0f);
					Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, base.mod.ProjectileType("FreedomShotN"), base.projectile.damage, 1f, player.whoAmI, 0f, 0f);
					return;
				case 1:
					Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 89, 1f, 0f);
					Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, base.mod.ProjectileType("FreedomShotN"), base.projectile.damage * 2, 1f, player.whoAmI, 0f, 0f);
					return;
				case 2:
					Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 88, 1f, 0f);
					Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, base.mod.ProjectileType("FreedomShotN"), base.projectile.damage * 2, 1f, player.whoAmI, 0f, 0f);
					return;
				case 3:
					Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 88, 1f, 0f);
					Projectile.NewProjectile(vector2.X, vector2.Y, SpeedX, SpeedY, base.mod.ProjectileType("FreedomShotNCharged"), base.projectile.damage * 6, 1f, player.whoAmI, 0f, 0f);
					break;
				default:
					return;
				}
			}
		}

		public int counter;

		public int chargeLevel;
	}
}
