using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class CageFlail_Ball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cage Crusher");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 26;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.melee = true;
			base.projectile.aiStyle = 15;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (base.projectile.localAI[0] == 0f)
			{
				for (int i = 0; i < Main.rand.Next(6, 14); i++)
				{
					if (Main.myPlayer == base.projectile.owner)
					{
						Projectile.NewProjectile(base.projectile.Center, RedeHelper.PolarVector((float)Main.rand.Next(3, 14), Utils.NextFloat(Main.rand, 0f, 6.2831855f)), ModContent.ProjectileType<EchoF>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				Main.PlaySound(29, base.projectile.position, 81);
				base.projectile.localAI[0] = 1f;
			}
		}

		public override void PostAI()
		{
			Vector2 position = base.projectile.Center;
			Vector2 vector2_4 = Main.player[base.projectile.owner].MountedCenter - position;
			base.projectile.rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) + 1.57f;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			damage *= (int)(target.knockBackResist + 1f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = ModContent.GetTexture("Redemption/Items/Weapons/PostML/Melee/CageFlail_Chain");
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
