using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class HonorsReach2Pro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/" + base.GetType().Name);
				HonorsReach2Pro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Honor's Reach");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(116);
			this.aiType = 116;
			base.projectile.alpha = 0;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 200;
			base.projectile.glowMask = HonorsReach2Pro.customGlowMask;
		}

		public static short customGlowMask;
	}
}
