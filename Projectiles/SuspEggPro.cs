using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class SuspEggPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Egg");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 20;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.magic = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			if (++base.projectile.frameCounter >= 3)
			{
				base.projectile.frameCounter = 0;
				if (++base.projectile.frame >= 7)
				{
					base.projectile.frame = 0;
				}
			}
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BAZINGA").WithVolume(0.9f).WithPitchVariance(0.1f), -1, -1);
			NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, 398, 0, 0f, 0f, 0f, 0f, 255);
			return true;
		}
	}
}
