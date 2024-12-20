using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class ScarletShardPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlet Shard");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.melee = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = false;
		}

		public override void AI()
		{
			if (Main.rand.Next(8) == 0)
			{
				int dustID = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 115, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
				Main.dust[dustID].noGravity = true;
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
			if (base.projectile.localAI[0] == 0f)
			{
				int dustType = 115;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				base.projectile.localAI[0] = 1f;
			}
		}

		public override void Kill(int timeLeft)
		{
			Vector2 usePos = base.projectile.position;
			Vector2 rotVector = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			usePos += rotVector * 16f;
			for (int i = 0; i < 20; i++)
			{
				Dust dust = Dust.NewDustDirect(usePos, base.projectile.width, base.projectile.height, 115, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
		}
	}
}
