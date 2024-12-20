using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class TreeBugFall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("TreeBug");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 26;
			base.projectile.height = 16;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = false;
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
				int i = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, ModContent.NPCType<TreeBug>(), 0, 0f, 0f, 0f, 0f, 255);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}
	}
}
