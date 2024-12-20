using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class AkkaSeedF : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Thorn/ThornSeed";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Thorn Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 6;
			base.projectile.height = 6;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 120;
			base.projectile.alpha = 0;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.45f;
			base.projectile.velocity.X = 0f;
			if (!this.spawnDust)
			{
				int dustType = 2;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				this.spawnDust = true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, Utils.NextFloat(Main.rand, -2f, 2f), Utils.NextFloat(Main.rand, -16f, -8f), ModContent.ProjectileType<CursedThornPro5>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
			return true;
		}

		private bool spawnDust;
	}
}
