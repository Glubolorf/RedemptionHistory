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
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Projectiles/" + base.GetType().Name + "_Glow");
				TeslaBladePro.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
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
			int num = 4;
			for (int i = 0; i < num; i++)
			{
				int num2 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, 440, base.projectile.damage, 3f, Main.myPlayer, 0f, 0f);
				Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(12f, 0f), (float)i / (float)num * 6.28f);
			}
			for (int j = 0; j < 3; j++)
			{
				int num3 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 226, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num3].velocity *= 1.4f;
			}
		}

		public static short customGlowMask;
	}
}
