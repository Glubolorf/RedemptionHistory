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
			base.projectile.thrown = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
			Projectile projectile2 = base.projectile;
			int num = projectile2.frameCounter + 1;
			projectile2.frameCounter = num;
			if (num >= 3)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile3 = base.projectile;
				num = projectile3.frame + 1;
				projectile3.frame = num;
				if (num >= 7)
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
			if (Main.netMode != 1)
			{
				int i = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, 398, 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}
	}
}
