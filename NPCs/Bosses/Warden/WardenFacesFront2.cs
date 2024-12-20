using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenFacesFront2 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Warden/WardenFalseFaces2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Warden Mask");
			Main.projFrames[base.projectile.type] = 14;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 22;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.Center = new Vector2(player.Center.X, player.Center.Y - 120f);
			base.projectile.timeLeft = 10;
			base.projectile.frame = (int)base.projectile.ai[0];
			base.projectile.scale += 0.01f;
			float obj = base.projectile.localAI[0];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					return;
				}
				base.projectile.alpha++;
				if (base.projectile.alpha >= 255)
				{
					base.projectile.Kill();
				}
			}
			else
			{
				base.projectile.alpha -= 10;
				if (base.projectile.alpha <= 0)
				{
					base.projectile.localAI[0] = 1f;
					return;
				}
			}
		}
	}
}
