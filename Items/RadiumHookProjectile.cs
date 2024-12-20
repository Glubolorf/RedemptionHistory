﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	internal class RadiumHookProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radium Digger Hook");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(315);
		}

		public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == base.projectile.type)
				{
					hooksOut++;
				}
			}
			if (hooksOut > 2)
			{
				return new bool?(false);
			}
			return new bool?(true);
		}

		public override float GrappleRange()
		{
			return 500f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 2;
		}

		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 16f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 8f;
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
				spriteBatch.Draw(base.mod.GetTexture("Items/RadiumHookChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height)), lightColor, projRotation, new Vector2((float)Main.chain30Texture.Width * 0.5f, (float)Main.chain30Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
