using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class CursedThornPro6 : ModProjectile
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
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			BaseAI.AIVilethorn(base.projectile, 190, 4, 36);
			this.spineEnd = (base.projectile.ai[1] == 36f);
			base.projectile.localAI[0] += 1f;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(70, 600, true);
		}

		public override bool PreDraw(SpriteBatch sb, Color drawColor)
		{
			Color newLightColor = new Color(Math.Max(0, (int)CursedThornPro6.lightColor.R + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CursedThornPro6.lightColor.G + Math.Min(0, -base.projectile.alpha + 20)), Math.Max(0, (int)CursedThornPro6.lightColor.B + Math.Min(0, -base.projectile.alpha + 20)));
			BaseDrawing.AddLight(base.projectile.Center, newLightColor, 1f);
			if (CursedThornPro6.mainTex == null)
			{
				CursedThornPro6.mainTex = Main.projectileTexture[base.projectile.type];
				CursedThornPro6.endTex = ModContent.GetTexture("Redemption/NPCs/Bosses/Thorn/CursedThornPro4");
			}
			BaseDrawing.DrawTexture(sb, this.spineEnd ? CursedThornPro6.endTex : CursedThornPro6.mainTex, 0, base.projectile, null, false, default(Vector2));
			return false;
		}

		public static Color lightColor = new Color(0, 40, 0);

		public static Texture2D mainTex;

		public static Texture2D endTex;

		public bool spineEnd;
	}
}
