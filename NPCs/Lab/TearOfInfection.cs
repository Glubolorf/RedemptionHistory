using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class TearOfInfection : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tear of Infection");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.penetrate = 1;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			float[] localAI = base.projectile.localAI;
			int num2 = 0;
			float num3 = localAI[num2] + 1f;
			localAI[num2] = num3;
			if (num3 > 8f)
			{
				base.projectile.tileCollide = true;
			}
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0.8f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y + 0.1f;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.NPCDeath1, base.projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}
	}
}
