using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Druid.Staves
{
	public class ShadePortalSummon : ModProjectile
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
			base.DisplayName.SetDefault("Portal Summon");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 16;
			base.projectile.aiStyle = 0;
			base.projectile.tileCollide = true;
			base.projectile.timeLeft = 350;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 0;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			Vector2 vector = new Vector2(base.projectile.ai[0], base.projectile.ai[1]) - base.projectile.Center;
			if ((double)base.projectile.timeLeft < 275.0)
			{
				base.projectile.Kill();
			}
			if ((double)vector.Length() < (double)base.projectile.velocity.Length())
			{
				base.projectile.Kill();
			}
			else
			{
				vector.Normalize();
				base.projectile.velocity = Vector2.Lerp(base.projectile.velocity, vector * 11.2f, 0.1f);
			}
			base.projectile.netUpdate = true;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 1f)
			{
				int dustType = 261;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
			Dust dust = Main.dust[Dust.NewDust(base.projectile.Center - new Vector2(4f, 4f), 1, 1, 261, 0f, 0f, 100, default(Color), 1f)];
			dust.noGravity = true;
			dust.velocity *= 0f;
		}

		public override void Kill(int timeLeft)
		{
			Player player = Main.player[base.projectile.owner];
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 261, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].velocity *= 1f;
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ShadeStavePortal>()] < 2)
			{
				Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<ShadeStavePortal>(), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
