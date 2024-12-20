using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class CursedRootFlailPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Root");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
		}

		public override void AI()
		{
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
			Vector2 vector14 = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float num166 = Main.player[base.projectile.owner].position.X + (float)(Main.player[base.projectile.owner].width / 2) - vector14.X;
			float num167 = Main.player[base.projectile.owner].position.Y + (float)(Main.player[base.projectile.owner].height / 2) - vector14.Y;
			float num168 = (float)Math.Sqrt((double)(num166 * num166 + num167 * num167));
			if (base.projectile.ai[0] != 0f)
			{
				if (base.projectile.ai[0] == 1f)
				{
					base.projectile.tileCollide = false;
					base.projectile.rotation = (float)Math.Atan2((double)num167, (double)num166) - 1.57f;
					float num169 = 30f;
					if (num168 < 50f)
					{
						base.projectile.Kill();
					}
					num168 = num169 / num168;
					num166 *= num168;
					num167 *= num168;
					base.projectile.velocity.X = num166;
					base.projectile.velocity.Y = num167;
					if (base.projectile.velocity.X < 0f)
					{
						base.projectile.spriteDirection = 1;
						return;
					}
					base.projectile.spriteDirection = -1;
				}
				return;
			}
			if (num168 > 700f)
			{
				base.projectile.ai[0] = 1f;
			}
			else if (num168 > 500f)
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
				return;
			}
			base.projectile.spriteDirection = 1;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 8;
			height = 8;
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.projectile.ai[0] = 1f;
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModContent.GetTexture("Redemption/Projectiles/Melee/CursedRootFlail2");
			Vector2 position = base.projectile.Center;
			Vector2 mountedCenter = Main.player[base.projectile.owner].MountedCenter;
			Rectangle? sourceRectangle = null;
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			float num = (float)texture.Height;
			Vector2 vector24 = mountedCenter - position;
			float rotation = (float)Math.Atan2((double)vector24.Y, (double)vector24.X) - 1.57f;
			bool flag = true;
			if (float.IsNaN(position.X) && float.IsNaN(position.Y))
			{
				flag = false;
			}
			if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
			{
				flag = false;
			}
			while (flag)
			{
				if ((double)vector24.Length() < (double)num + 1.0)
				{
					flag = false;
				}
				else
				{
					Vector2 vector25 = vector24;
					vector25.Normalize();
					position += vector25 * num;
					vector24 = mountedCenter - position;
					Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
					color2 = base.projectile.GetAlpha(color2);
					Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
				}
			}
			return true;
		}
	}
}
