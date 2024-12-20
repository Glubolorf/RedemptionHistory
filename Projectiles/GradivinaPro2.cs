using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class GradivinaPro2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Projectiles/" + base.GetType().Name + "_Glow");
				GradivinaPro2.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Gradivina");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(116);
			this.aiType = 116;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 200;
			base.projectile.glowMask = GradivinaPro2.customGlowMask;
		}

		public static short customGlowMask;
	}
}
