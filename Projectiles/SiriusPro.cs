using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SiriusPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sirius");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			base.projectile.aiStyle = 15;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModContent.GetTexture("Redemption/Projectiles/SiriusChain");
			Vector2 position = base.projectile.Center;
			Vector2 mountedCenter = Main.player[base.projectile.owner].MountedCenter;
			Rectangle? sourceRectangle = null;
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			float num = (float)texture.Height;
			Vector2 vector2_4 = mountedCenter - position;
			float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
			bool flag = true;
			if (float.IsNaN(position.X) && float.IsNaN(position.Y))
			{
				flag = false;
			}
			if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
			{
				flag = false;
			}
			while (flag)
			{
				if ((double)vector2_4.Length() < (double)num + 1.0)
				{
					flag = false;
				}
				else
				{
					Vector2 vector2_5 = vector2_4;
					vector2_5.Normalize();
					position += vector2_5 * num;
					vector2_4 = mountedCenter - position;
					Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
					color2 = base.projectile.GetAlpha(color2);
					Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 21, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 11f, base.projectile.position.Y + 11f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 11f, base.projectile.position.Y + 11f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 11f, base.projectile.position.Y + 11f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 11f, base.projectile.position.Y + 11f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 11f, base.projectile.position.Y + 11f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), ModContent.ProjectileType<PoisonShard>(), 4, 1f, base.projectile.owner, 0f, 1f);
			}
			base.projectile.Kill();
		}
	}
}
