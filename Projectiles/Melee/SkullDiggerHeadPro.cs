using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.TheKeeper;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Melee
{
	public class SkullDiggerHeadPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skull Digger's Skull Digger");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 34;
			base.projectile.height = 34;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			base.projectile.aiStyle = 15;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (target.type == ModContent.NPCType<Keeper>() && target.life <= 0)
			{
				string text = "*Betrayal fills the Keeper's mind*";
				Color rarityPurple = Colors.RarityPurple;
				byte r = rarityPurple.R;
				rarityPurple = Colors.RarityPurple;
				byte g = rarityPurple.G;
				rarityPurple = Colors.RarityPurple;
				Main.NewText(text, r, g, rarityPurple.B, false);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModContent.GetTexture("Redemption/Projectiles/Melee/SkullDiggerChainPro");
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
	}
}
