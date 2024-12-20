using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class CursedThornPro4 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Thorns");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 20;
			base.projectile.height = 20;
			base.projectile.aiStyle = -1;
			base.projectile.timeLeft = 320;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			BaseAI.AIVilethorn(base.projectile, 190, 4, 8);
			this.spineEnd = (base.projectile.ai[1] == 8f);
			base.projectile.localAI[0] += 1f;
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Color color;
			color..ctor(Math.Max(0, (int)CursedThornPro4.lightColor.R + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CursedThornPro4.lightColor.G + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CursedThornPro4.lightColor.B + Math.Min(0, -base.projectile.alpha + 20)));
			BaseDrawing.AddLight(base.projectile.Center, color, 1f);
			if (CursedThornPro4.mainTex == null)
			{
				CursedThornPro4.mainTex = Main.projectileTexture[base.projectile.type];
				CursedThornPro4.endTex = ModLoader.GetTexture("Redemption/NPCs/Bosses/Thorn/CursedThornPro4");
			}
			BaseDrawing.DrawTexture(sb, this.spineEnd ? CursedThornPro4.endTex : CursedThornPro4.mainTex, 0, base.projectile, null, false, default(Vector2));
			return false;
		}

		public static Color lightColor = new Color(0, 40, 0);

		public static Texture2D mainTex;

		public static Texture2D endTex;

		public bool spineEnd;
	}
}
