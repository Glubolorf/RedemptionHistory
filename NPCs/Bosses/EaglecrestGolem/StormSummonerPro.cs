using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class StormSummonerPro : ModProjectile
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
			base.DisplayName.SetDefault("Ukko's Lightning");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 10;
			base.projectile.height = 10;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 100;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.ai[0] == 0f)
			{
				if (base.projectile.localAI[0] % 10f == 0f && base.projectile.localAI[0] > 10f)
				{
					this.PosX += 200;
				}
				if (base.projectile.localAI[0] % 10f == 0f)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X - 1000f + (float)this.PosX, base.projectile.Center.Y), base.projectile.velocity, base.mod.ProjectileType("UkkoZap1"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
					return;
				}
			}
			else if (base.projectile.ai[0] == 1f)
			{
				if (base.projectile.localAI[0] % 10f == 0f && base.projectile.localAI[0] > 10f)
				{
					this.PosX += 200;
				}
				if (base.projectile.localAI[0] % 10f == 0f)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X + 1000f - (float)this.PosX, base.projectile.Center.Y), base.projectile.velocity, base.mod.ProjectileType("UkkoZap1"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
					return;
				}
			}
			else if (base.projectile.ai[0] == 2f)
			{
				if (base.projectile.localAI[0] % 5f == 0f && base.projectile.localAI[0] > 5f)
				{
					this.PosX += 200;
				}
				if (base.projectile.localAI[0] % 5f == 0f && base.projectile.localAI[0] <= 50f)
				{
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X - 1000f + (float)this.PosX, base.projectile.Center.Y + 400f), base.projectile.velocity, base.mod.ProjectileType("UkkoZap1"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
					Projectile.NewProjectile(new Vector2(base.projectile.Center.X + 1000f - (float)this.PosX, base.projectile.Center.Y - 400f), base.projectile.velocity, base.mod.ProjectileType("UkkoZap1"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 1f);
					return;
				}
			}
			else if (base.projectile.ai[0] == 3f && base.projectile.localAI[0] == 2f)
			{
				for (int i = -6; i <= 6; i++)
				{
					Projectile.NewProjectile(base.projectile.Center, 10f * Utils.RotatedBy(Vector2.UnitX, 0.5235987755982988 * (double)i, default(Vector2)), base.mod.ProjectileType("StormSummonerPro2"), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
				}
			}
		}

		private int PosX;
	}
}
