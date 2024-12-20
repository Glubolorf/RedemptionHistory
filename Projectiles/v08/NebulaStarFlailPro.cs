using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class NebulaStarFlailPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Daycrusher");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
		}

		public override void AI()
		{
			if (Utils.NextFloat(Main.rand) < 1f)
			{
				Vector2 position = base.projectile.position;
				Dust dust = Main.dust[Dust.NewDust(position, base.projectile.width, base.projectile.height, 58, 0f, 0f, 0, default(Color), 2f)];
				Dust dust2 = Main.dust[Dust.NewDust(position, base.projectile.width, base.projectile.height, 58, 0f, 0f, 0, default(Color), 2f)];
				dust.noGravity = true;
				dust2.noGravity = true;
			}
			if (base.projectile.timeLeft == 120)
			{
				base.projectile.ai[0] = 1f;
			}
			if (Main.player[base.projectile.owner].dead)
			{
				base.projectile.Kill();
				return;
			}
			Main.player[base.projectile.owner].itemAnimation = 5;
			Main.player[base.projectile.owner].itemTime = 5;
			if (base.projectile.alpha == 0)
			{
				if (base.projectile.position.X + (float)(base.projectile.width / 2) > Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2))
				{
					Main.player[base.projectile.owner].ChangeDir(1);
				}
				else
				{
					Main.player[base.projectile.owner].ChangeDir(-1);
				}
			}
			Vector2 vector;
			vector..ctor(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) - vector.X;
			float num2 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - vector.Y;
			float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
			if (base.projectile.ai[0] == 0f)
			{
				if (num3 > 700f)
				{
					base.projectile.ai[0] = 1f;
				}
				else if (num3 > 500f)
				{
					base.projectile.ai[0] = 1f;
				}
				base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
				base.projectile.ai[1] += 1f;
				if (base.projectile.ai[1] > 5f)
				{
					base.projectile.alpha = 0;
				}
				if (base.projectile.ai[1] > 8f)
				{
					base.projectile.ai[1] = 8f;
				}
				if (base.projectile.ai[1] >= 10f)
				{
					base.projectile.ai[1] = 15f;
					base.projectile.velocity.Y = base.projectile.velocity.Y + 0.3f;
				}
				if (base.projectile.velocity.X < 0f)
				{
					base.projectile.spriteDirection = -1;
				}
				else
				{
					base.projectile.spriteDirection = 1;
				}
			}
			else if (base.projectile.ai[0] == 1f)
			{
				base.projectile.tileCollide = false;
				base.projectile.rotation = (float)Math.Atan2((double)num2, (double)num) - 1.57f;
				float num4 = 30f;
				if (num3 < 50f)
				{
					base.projectile.Kill();
				}
				num3 = num4 / num3;
				num *= num3;
				num2 *= num3;
				base.projectile.velocity.X = num;
				base.projectile.velocity.Y = num2;
				if (base.projectile.velocity.X < 0f)
				{
					base.projectile.spriteDirection = 1;
				}
				else
				{
					base.projectile.spriteDirection = -1;
				}
			}
			if ((int)base.projectile.ai[1] % 8 == 0 && base.projectile.owner == Main.myPlayer && Main.rand.Next(10) == 0)
			{
				Vector2 vector2 = Main.player[base.projectile.owner].Center - base.projectile.Center;
				Vector2 vector3 = vector2 * -1f;
				vector3.Normalize();
				vector3 *= (float)Main.rand.Next(45, 65) * 0.1f;
				vector3 = Utils.RotatedBy(vector3, (Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default(Vector2));
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, vector3.X, vector3.Y, base.mod.ProjectileType("NebulaSparklePro"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, -10f, 0f);
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 14, 1f, 0f);
			int num = Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, base.mod.ProjectileType("NebulaSparklePro2"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
			Main.projectile[num].melee = true;
			Main.projectile[num].friendly = true;
			Main.projectile[num].hostile = false;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 26;
			height = 26;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.ai[0] = 1f;
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModLoader.GetTexture("Redemption/Projectiles/v08/NebulaStarChainPro");
			Vector2 vector = base.projectile.Center;
			Vector2 mountedCenter = Main.player[base.projectile.owner].MountedCenter;
			Rectangle? rectangle = null;
			Vector2 vector2;
			vector2..ctor((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			float num = (float)texture.Height;
			Vector2 vector3 = mountedCenter - vector;
			float num2 = (float)Math.Atan2((double)vector3.Y, (double)vector3.X) - 1.57f;
			bool flag = true;
			if (float.IsNaN(vector.X) && float.IsNaN(vector.Y))
			{
				flag = false;
			}
			if (float.IsNaN(vector3.X) && float.IsNaN(vector3.Y))
			{
				flag = false;
			}
			while (flag)
			{
				if ((double)vector3.Length() < (double)num + 1.0)
				{
					flag = false;
				}
				else
				{
					Vector2 vector4 = vector3;
					vector4.Normalize();
					vector += vector4 * num;
					vector3 = mountedCenter - vector;
					Color color = Lighting.GetColor((int)vector.X / 16, (int)((double)vector.Y / 16.0));
					color = base.projectile.GetAlpha(color);
					Main.spriteBatch.Draw(texture, vector - Main.screenPosition, rectangle, Color.White, num2, vector2, 1f, 0, 0f);
				}
			}
			return true;
		}
	}
}
