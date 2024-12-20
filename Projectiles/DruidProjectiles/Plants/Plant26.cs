using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.Plants
{
	public class Plant26 : DruidPlant
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Projectiles/DruidProjectiles/Plants/" + base.GetType().Name);
				Plant26.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Creation Rose");
			Main.projFrames[base.projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 30;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
			base.projectile.glowMask = Plant26.customGlowMask;
		}

		protected override void PlantAI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 20)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 2)
				{
					base.projectile.frame = 0;
				}
			}
			Projectile projectile3 = base.projectile;
			projectile3.velocity.Y = projectile3.velocity.Y * 0f;
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC) && base.projectile.localAI[0] % 40f == 0f)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(20f, Utils.ToRotation(this.target.Center - base.projectile.Center) + 25f), ModContent.ProjectileType<CreationRosePetal>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(20f, Utils.ToRotation(this.target.Center - base.projectile.Center) - 25f), ModContent.ProjectileType<CreationRosePetal>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(20f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<CreationRosePetal>(), base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y), base.projectile.width, base.projectile.height, 163, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
		}

		public static short customGlowMask;

		private NPC target;
	}
}
