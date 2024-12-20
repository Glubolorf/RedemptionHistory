using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class ShadowBolt : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadow Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.penetrate = 2;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 200;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				int dustType = 173;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				Main.PlaySound(2, (int)base.projectile.position.X, (int)base.projectile.position.Y, 20, 1f, 0f);
				base.projectile.localAI[0] = 1f;
			}
			int num666 = 8;
			int num667 = Dust.NewDust(new Vector2(base.projectile.position.X + (float)num666 + 6f, base.projectile.position.Y + (float)num666), base.projectile.width - num666 * 2, base.projectile.height - num666 * 2, 66, 0f, 0f, 0, new Color(131, 0, 0), 1.5f);
			Main.dust[num667].velocity *= 0.5f;
			Main.dust[num667].velocity += base.projectile.velocity * 0.5f;
			Main.dust[num667].noGravity = true;
			Main.dust[num667].noLight = false;
			Main.dust[num667].scale = 2f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			int dustType = 173;
			int pieCut = 8;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
			if (base.projectile.velocity.X != oldVelocity.X)
			{
				base.projectile.velocity.X = -oldVelocity.X;
			}
			if (base.projectile.velocity.Y != oldVelocity.Y)
			{
				base.projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}
	}
}
