using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.VCleaver
{
	public class WielderOrb : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shield Orb");
			Main.projFrames[base.projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 26;
			base.projectile.tileCollide = false;
			base.projectile.friendly = false;
			base.projectile.hostile = false;
			base.projectile.alpha = 255;
			ProjectileID.Sets.TrailCacheLength[base.projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[base.projectile.type] = 0;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num2 = projectile.frameCounter + 1;
			projectile.frameCounter = num2;
			if (num2 >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num2 = projectile2.frame + 1;
				projectile2.frame = num2;
				if (num2 >= 4)
				{
					base.projectile.frame = 0;
				}
			}
			base.projectile.timeLeft = 50;
			Player player = Main.player[base.projectile.owner];
			Lighting.AddLight(base.projectile.Center, (float)(255 - base.projectile.alpha) * 0.5f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f, (float)(255 - base.projectile.alpha) * 0f / 255f);
			if (base.projectile.alpha <= 0)
			{
				base.projectile.alpha = 0;
			}
			else
			{
				base.projectile.alpha -= 3;
			}
			double rad = (double)base.projectile.ai[1] * 0.017453292519943295;
			NPC host = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.position.X = host.Center.X - (float)((int)(Math.Cos(rad) * this.dist)) - (float)(base.projectile.width / 2);
			base.projectile.position.Y = host.Center.Y - (float)((int)(Math.Sin(rad) * this.dist)) - (float)(base.projectile.height / 2);
			base.projectile.ai[1] += 5f;
			if (host.life <= 0 || !host.active || host.type != ModContent.NPCType<Wielder>())
			{
				base.projectile.Kill();
			}
			float num = 8f;
			Vector2 vector = new Vector2(base.projectile.position.X + (float)base.projectile.width * 0.5f, base.projectile.position.Y + (float)base.projectile.height * 0.5f);
			float hostX = host.position.X + (float)(host.width / 2);
			float hostY = host.position.Y + (float)(host.height / 2);
			hostX = (float)((int)(hostX / 8f) * 8);
			hostY = (float)((int)(hostY / 8f) * 8);
			vector.X = (float)((int)(vector.X / 8f) * 8);
			vector.Y = (float)((int)(vector.Y / 8f) * 8);
			hostX -= vector.X;
			hostY -= vector.Y;
			float rootXY = (float)Math.Sqrt((double)(hostX * hostX + hostY * hostY));
			if (rootXY == 0f)
			{
				hostX = base.projectile.velocity.X;
				hostY = base.projectile.velocity.Y;
			}
			else
			{
				rootXY = num / rootXY;
				hostX *= rootXY;
				hostY *= rootXY;
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && proj.minionSlots == 0f && proj.friendly && proj.damage > 5)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BallFire").WithVolume(0.2f).WithPitchVariance(0.1f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
					}
					for (int i = 0; i < 2; i++)
					{
						Dust dust = Dust.NewDustDirect(base.projectile.position, base.projectile.width, base.projectile.height, 235, 0f, 0f, 100, default(Color), 1f);
						dust.velocity = -base.projectile.DirectionTo(dust.position) * 2f;
					}
					proj.Kill();
				}
			}
		}

		private readonly double dist = 60.0;
	}
}
