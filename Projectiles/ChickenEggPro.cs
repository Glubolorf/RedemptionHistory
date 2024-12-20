using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class ChickenEggPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Egg");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 16;
			base.projectile.height = 20;
			base.projectile.friendly = true;
			base.projectile.penetrate = 1;
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
			if (base.projectile.localAI[0] > 130f)
			{
				base.projectile.Kill();
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.projectile.position, oldVelocity, base.projectile.width, base.projectile.height);
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			if (Main.rand.Next(4) == 0)
			{
				NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("Chicken"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (Main.rand.Next(100) == 0)
			{
				NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("ChickenGold"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (Main.rand.Next(999) == 0)
			{
				NPC.NewNPC((int)base.projectile.position.X, (int)base.projectile.position.Y, base.mod.NPCType("RainbowChicken"), 0, 0f, 0f, 0f, 0f, 255);
			}
			return true;
		}
	}
}
