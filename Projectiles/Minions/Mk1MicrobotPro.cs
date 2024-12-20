using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class Mk1MicrobotPro : ModProjectile
	{
		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(440);
			base.projectile.width = 2;
			base.projectile.height = 24;
			base.projectile.magic = true;
			base.projectile.penetrate = 1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 200;
			this.aiType = 440;
			base.projectile.usesLocalNPCImmunity = true;
		}

		public override bool PreAI()
		{
			if (base.projectile.localAI[0] == 0f)
			{
				Main.PlaySound(2, base.projectile.position, 12);
				base.projectile.localAI[0] = 1f;
			}
			return true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[base.projectile.owner] = 4;
		}

		private int[] dusts = new int[]
		{
			160,
			600,
			64,
			61,
			50,
			60,
			590
		};
	}
}
