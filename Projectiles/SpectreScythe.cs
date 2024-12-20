using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SpectreScythe : ModProjectile
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
				SpectreScythe.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.DisplayName.SetDefault("Spectral Scythe");
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(45);
			this.aiType = 45;
			base.projectile.alpha = 50;
			base.projectile.melee = true;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 80;
			base.projectile.glowMask = SpectreScythe.customGlowMask;
		}

		public override void AI()
		{
			int num = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 2f), base.projectile.width + 16, base.projectile.height + 16, 56, base.projectile.velocity.X * 0.2f, base.projectile.velocity.Y * 0.2f, 20, default(Color), 1f);
			Main.dust[num].noGravity = true;
			base.projectile.localAI[0] += 1f;
			base.projectile.alpha = (int)base.projectile.localAI[0] * 2;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.type == 62)
			{
				damage *= 2;
			}
			if (target.type == 66)
			{
				damage *= 2;
			}
			if (target.type == 24)
			{
				damage *= 2;
			}
			if (target.type == 156)
			{
				damage *= 2;
			}
		}

		public static short customGlowMask;
	}
}
