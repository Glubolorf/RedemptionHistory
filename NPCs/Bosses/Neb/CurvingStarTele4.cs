using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class CurvingStarTele4 : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/CurvingStarTele2";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shooting Star");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 22;
			base.projectile.height = 22;
			base.projectile.hostile = false;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 50;
			base.projectile.alpha = 255;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			if (base.projectile.localAI[0] == 0f)
			{
				if (base.projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<CurvingStarTele3>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, base.projectile.ai[0], base.projectile.ai[1]);
				}
				base.projectile.localAI[0] += 1f;
				return;
			}
			float[] localAI = base.projectile.localAI;
			int num = 0;
			float num2 = localAI[num] + 1f;
			localAI[num] = num2;
			if (num2 >= 30f)
			{
				for (int i = 0; i < 8; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, 58, 0f, 0f, 100, Color.White, 3f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(4f, 0f), (float)i / 8f * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				Main.PlaySound(SoundID.Item117, base.projectile.position);
				if (base.projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(base.projectile.Center, base.projectile.velocity, ModContent.ProjectileType<CurvingStar2>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, base.projectile.ai[0], base.projectile.ai[1]);
				}
				base.projectile.Kill();
			}
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}
	}
}
