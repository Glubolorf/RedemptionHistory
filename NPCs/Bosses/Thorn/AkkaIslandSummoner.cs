using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class AkkaIslandSummoner : ModProjectile
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
			base.DisplayName.SetDefault("Island Summoner");
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
			base.projectile.timeLeft = 190;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 5f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, player.Center.Y - 1500f), new Vector2(0f, 15f), ModContent.ProjectileType<AkkaIslandWarning>(), 0, 0f, base.projectile.owner, 0f, 0f);
			}
			if (base.projectile.localAI[0] == 120f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, player.Center.Y - 1500f), new Vector2(0f, 5f), ModContent.ProjectileType<AkkaIsland>(), 0, 0f, base.projectile.owner, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, player.Center.Y - 1244f), new Vector2(0f, 5f), ModContent.ProjectileType<AkkaIslandSummoner.AkkaIslandHitbox>(), base.projectile.damage, base.projectile.knockBack, base.projectile.owner, 0f, 0f);
				base.projectile.Kill();
			}
		}

		public class AkkaIslandHitbox : ModProjectile
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
				base.DisplayName.SetDefault("Floating Island");
			}

			public override void SetDefaults()
			{
				base.projectile.width = 1696;
				base.projectile.height = 320;
				base.projectile.penetrate = -1;
				base.projectile.hostile = true;
				base.projectile.friendly = false;
				base.projectile.ignoreWater = true;
				base.projectile.tileCollide = false;
				base.projectile.alpha = 255;
				base.projectile.timeLeft = 600;
				base.projectile.extraUpdates = 1;
			}

			public override void AI()
			{
				for (int p = 0; p < 255; p++)
				{
					this.clearCheck = Main.player[p];
					if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
					{
						this.clearCheck.velocity.Y = base.projectile.velocity.Y;
					}
				}
			}

			private Player clearCheck;
		}
	}
}
