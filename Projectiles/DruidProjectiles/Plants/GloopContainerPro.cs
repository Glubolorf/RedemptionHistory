using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class GloopContainerPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/DruidProjectiles/Plants/" + base.GetType().Name + "_Glow");
				GloopContainerPro.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Gloop Container");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.glowMask = GloopContainerPro.customGlowMask;
			base.projectile.timeLeft = 35;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
			base.projectile.GetGlobalProjectile<DruidProjectile>().fromSeedbag = true;
		}

		public override void AI()
		{
			base.projectile.rotation += 0.06f;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.7f;
			if (base.projectile.velocity.Y >= 10f)
			{
				base.projectile.velocity.Y = 9f;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, base.mod.DustType("GloopDust"), 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
			if (Main.rand.Next(2) == 0)
			{
				Projectile.NewProjectile(base.projectile.position.X + 10f, base.projectile.position.Y + 18f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), base.mod.ProjectileType("GloopPro1"), base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}

		public static short customGlowMask;
	}
}
