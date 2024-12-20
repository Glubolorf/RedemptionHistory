using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class TeslaBladePro : ModProjectile
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
				TeslaBladePro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Holoblade");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(116);
			this.aiType = 116;
			base.projectile.width = 40;
			base.projectile.height = 40;
			base.projectile.melee = true;
			base.projectile.friendly = true;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.tileCollide = true;
			base.projectile.alpha = 50;
			base.projectile.timeLeft = 180;
			base.projectile.glowMask = TeslaBladePro.customGlowMask;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item93.WithVolume(0.3f), base.projectile.position);
			int pieCut = 4;
			for (int i = 0; i < pieCut; i++)
			{
				int projID = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, 440, base.projectile.damage, 3f, Main.myPlayer, 0f, 0f);
				Main.projectile[projID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)i / (float)pieCut * 6.28f);
			}
			for (int j = 0; j < 3; j++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 226, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public static short customGlowMask;
	}
}
