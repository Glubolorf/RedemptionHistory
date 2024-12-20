using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class XenomiteFlailHead : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Boulder");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 44;
			base.projectile.height = 44;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			base.projectile.aiStyle = 15;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModLoader.GetTexture("Redemption/Projectiles/XenomiteFlailChain");
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
					Main.spriteBatch.Draw(texture, vector - Main.screenPosition, rectangle, color, num2, vector2, 1.35f, 0, 0f);
				}
			}
			return true;
		}
	}
}
