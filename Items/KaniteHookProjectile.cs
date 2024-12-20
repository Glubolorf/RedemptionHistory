using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	internal class KaniteHookProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("${ProjectileName.GemHookAmethyst}");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(230);
		}

		public override bool? SingleGrappleHook(Player player)
		{
			return new bool?(true);
		}

		public override float GrappleRange()
		{
			return 240f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 1;
		}

		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 10f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 6f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 mountedCenter = Main.player[base.projectile.owner].MountedCenter;
			Vector2 vector = base.projectile.Center;
			Vector2 vector2 = mountedCenter - base.projectile.Center;
			float num = Utils.ToRotation(vector2) - 1.57f;
			float num2 = vector2.Length();
			while (num2 > 30f && !float.IsNaN(num2))
			{
				vector2.Normalize();
				vector2 *= 24f;
				vector += vector2;
				vector2 = mountedCenter - vector;
				num2 = vector2.Length();
				spriteBatch.Draw(base.mod.GetTexture("Items/KaniteHookChain"), new Vector2(vector.X - Main.screenPosition.X, vector.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height)), lightColor, num, new Vector2((float)Main.chain30Texture.Width * 0.5f, (float)Main.chain30Texture.Height * 0.5f), 1f, 0, 0f);
			}
			return true;
		}
	}
}
