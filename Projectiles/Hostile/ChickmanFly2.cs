using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.ChickenInv;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Hostile
{
	public class ChickmanFly2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chickman");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 24;
			base.projectile.height = 28;
			base.projectile.friendly = false;
			base.projectile.penetrate = -1;
			base.projectile.hostile = true;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 180;
		}

		public override void AI()
		{
			base.projectile.rotation += base.projectile.velocity.X / 40f * (float)base.projectile.direction;
			Projectile projectile = base.projectile;
			projectile.velocity.Y = projectile.velocity.Y + 0.3f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			if (Main.netMode != 1)
			{
				int i = NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, ModContent.NPCType<ChickenMan>(), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}
	}
}
