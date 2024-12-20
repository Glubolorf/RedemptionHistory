using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.v08
{
	public class MoltenStavePro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Projectiles/v08/FirebreakPro1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Molten Droplet");
			Main.projFrames[base.projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.aiStyle = 0;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 350;
			base.projectile.penetrate = 1;
			base.projectile.alpha = 0;
			base.projectile.friendly = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromStave = true;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 3)
				{
					base.projectile.frame = 0;
				}
			}
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			base.projectile.rotation = (float)Math.Atan2((double)base.projectile.velocity.Y, (double)base.projectile.velocity.X) + 1.57f;
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
				int dustType = 6;
				int pieCut = 8;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.Bottom.Y), base.projectile.width, 2, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].velocity *= 1f;
			}
			Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, 0f, 0f, base.mod.ProjectileType("MoltenDropletPro"), base.projectile.damage, base.projectile.knockBack, Main.myPlayer, 0f, 0f);
		}
	}
}
