using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.TheKeeper
{
	public class ShadowBolt : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadow Bolt");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 8;
			base.projectile.height = 8;
			base.projectile.penetrate = 2;
			base.projectile.hostile = true;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 300;
			base.projectile.extraUpdates = 1;
		}

		public override void AI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				for (int i = 0; i < 8; i++)
				{
					int dustID = Dust.NewDust(base.projectile.Center - new Vector2(-1f, -1f), 2, 2, 173, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / 8f * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				base.projectile.localAI[0] = 1f;
			}
			int dust = Dust.NewDust(base.projectile.Center - new Vector2(-2f, -2f), 4, 4, 66, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, new Color(131, 0, 0), 2f);
			Main.dust[dust].velocity *= 0.5f;
			Main.dust[dust].noGravity = true;
			Main.dust[dust].noLight = false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (base.projectile.penetrate <= 0)
			{
				base.projectile.Kill();
			}
			base.projectile.penetrate--;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < 8; i++)
			{
				int dustID = Dust.NewDust(base.projectile.Center - new Vector2(-1f, -1f), 2, 2, 173, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / 8f * 6.28f);
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
