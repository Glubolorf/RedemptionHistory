using System;
using Microsoft.Xna.Framework;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles
{
	public class BoneSeed : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bone Seed");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 12;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 35;
			base.projectile.GetGlobalProjectile<DruidProjectile>().druidic = true;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
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
			Main.PlaySound(SoundID.NPCHit2, base.projectile.position);
			Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 31, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.myPlayer == base.projectile.owner)
			{
				for (int i = 0; i < Main.rand.Next(2, 3); i++)
				{
					int p = Projectile.NewProjectile(base.projectile.position.X + 6f, base.projectile.position.Y + 6f, (float)(-8 + Main.rand.Next(0, 17)), (float)(-8 + Main.rand.Next(0, 17)), 21, base.projectile.damage, 0f, base.projectile.owner, 0f, 1f);
					Main.projectile[p].noDropItem = true;
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			return true;
		}
	}
}
