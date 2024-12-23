﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class BloodrootRoot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bloodroot");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 24;
			base.projectile.aiStyle = -1;
			base.projectile.timeLeft = 320;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			BaseAI.AIVilethorn(base.projectile, 50, 4, 10);
			this.spineEnd = (base.projectile.ai[1] == 10f);
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Color newLightColor = new Color(Math.Max(0, (int)BloodrootRoot.lightColor.R + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)BloodrootRoot.lightColor.G + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)BloodrootRoot.lightColor.B + Math.Min(0, -base.projectile.alpha + 20)));
			BaseDrawing.AddLight(base.projectile.Center, newLightColor, 1f);
			if (BloodrootRoot.mainTex == null)
			{
				BloodrootRoot.mainTex = Main.projectileTexture[base.projectile.type];
				BloodrootRoot.endTex = ModContent.GetTexture("Redemption/Projectiles/Druid/Seedbag/BloodrootEnd");
			}
			BaseDrawing.DrawTexture(sb, this.spineEnd ? BloodrootRoot.endTex : BloodrootRoot.mainTex, 0, base.projectile, null, false, default(Vector2));
			return false;
		}

		public static Color lightColor = new Color(100, 70, 0);

		public static Texture2D mainTex;

		public static Texture2D endTex;

		public bool spineEnd;
	}
}
