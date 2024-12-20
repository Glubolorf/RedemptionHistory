using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
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
			Vector2 playerCenter = Main.player[base.projectile.owner].MountedCenter;
			Vector2 center = base.projectile.Center;
			Vector2 distToProj = playerCenter - base.projectile.Center;
			float projRotation = Utils.ToRotation(distToProj) - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance))
			{
				distToProj.Normalize();
				distToProj *= 24f;
				center += distToProj;
				distToProj = playerCenter - center;
				distance = distToProj.Length();
				spriteBatch.Draw(base.mod.GetTexture("Items/Accessories/PreHM/KaniteHookChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height)), lightColor, projRotation, new Vector2((float)Main.chain30Texture.Width * 0.5f, (float)Main.chain30Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
