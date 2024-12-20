using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class AncientSlingShotPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Sling Shot");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 22;
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
				Vector2 vector3 = base.projectile.Center + vector2;
				for (int i = 0; i < 3; i++)
				{
					int num5 = Dust.NewDust(vector3 - Vector2.One * 8f, 16, 16, 269, base.projectile.velocity.X / 2f, base.projectile.velocity.Y / 2f, 100, default(Color), 1f);
					Dust dust = Main.dust[num5];
					dust.position.Y = dust.position.Y - 0.3f;
					Main.dust[num5].velocity *= 0.66f;
					Main.dust[num5].noGravity = true;
					Main.dust[num5].scale = 1.4f;
				}
			}
			if (flag && Main.myPlayer == base.projectile.owner && player.channel && !player.noItems && !player.CCed)
			{
				float num6 = player.inventory[player.selectedItem].shootSpeed * base.projectile.scale;
				Vector2 vector4 = vector;
				Vector2 vector5 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector4;
				if (player.gravDir == -1f)
				{
					vector5.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector4.Y;
				}
				Vector2 vector6 = Vector2.Normalize(vector5);
				if (float.IsNaN(vector6.X) || float.IsNaN(vector6.Y))
				{
					vector6 = -Vector2.UnitY;
				}
				vector6 *= num6;
				if (vector6.X != base.projectile.velocity.X || vector6.Y != base.projectile.velocity.Y)
				{
					base.projectile.netUpdate = true;
				}
				base.projectile.velocity = vector6;
				float num7 = 14f;
				int num8 = 7;
				vector4 = base.projectile.Center + new Vector2((float)Main.rand.Next(-num8, num8 + 1), (float)Main.rand.Next(-num8, num8 + 1));
				Vector2 vector7 = Vector2.Normalize(base.projectile.velocity) * num7;
				vector7 = Utils.RotatedBy(vector7, Main.rand.NextDouble() * 0.19634954631328583 - 0.09817477315664291, default(Vector2));
				if (float.IsNaN(vector7.X) || float.IsNaN(vector7.Y))
				{
					vector7 = -Vector2.UnitY;
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
				this.chargeLevel = 3;
			}
			else if (this.counter >= 60)
			{
				this.chargeLevel = 2;
			}
			else if (this.counter >= 30)
			{
				this.chargeLevel = 1;
			}
			else if (this.counter < 30)
			{
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
				Vector2 vector;
				vector..ctor(player.position.X + (float)player.width * 0.5f, player.position.Y + (float)player.height * 0.5f);
				float num2 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
				float num3 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
				if ((double)player.gravDir == -1.0)
				{
					num3 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
				}
				float num4 = (float)Math.Sqrt((double)num2 * (double)num2 + (double)num3 * (double)num3);
				float num5;
				if ((float.IsNaN(num2) && float.IsNaN(num3)) || ((double)num2 == 0.0 && (double)num3 == 0.0))
				{
					num2 = (float)player.direction;
					num3 = 0f;
					num5 = num;
				}
				else
				{
					num5 = num / num4;
				}
				float num6 = num2 * num5;
				float num7 = num3 * num5;
				switch (this.chargeLevel)
				{
				case 0:
					Main.PlaySound(SoundID.Item5, base.projectile.position);
					Projectile.NewProjectile(vector.X, vector.Y, num6, num7, base.mod.ProjectileType("AncientSparkPro1"), base.projectile.damage, 1f, player.whoAmI, 0f, 0f);
					return;
				case 1:
					Main.PlaySound(SoundID.Item5, base.projectile.position);
					Projectile.NewProjectile(vector.X, vector.Y, num6 * 1.5f, num7 * 1.5f, base.mod.ProjectileType("AncientSparkPro1"), base.projectile.damage * 6, 1f, player.whoAmI, 0f, 0f);
					return;
				case 2:
					Main.PlaySound(SoundID.Item5, base.projectile.position);
					Projectile.NewProjectile(vector.X, vector.Y, num6 * 2f, num7 * 2f, base.mod.ProjectileType("AncientSparkPro1"), base.projectile.damage * 16, 1f, player.whoAmI, 0f, 0f);
					return;
				case 3:
					Main.PlaySound(SoundID.Item5, base.projectile.position);
					Projectile.NewProjectile(vector.X, vector.Y, num6 * 3f, num7 * 3f, base.mod.ProjectileType("AncientSparkPro1"), base.projectile.damage * 30, 1f, player.whoAmI, 0f, 0f);
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
