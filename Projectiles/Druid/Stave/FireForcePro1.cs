using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Stave
{
	public class FireForcePro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starbound Fire Force");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 488;
			base.projectile.height = 488;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 200;
			base.projectile.timeLeft = 900;
		}

		public override void AI()
		{
			Projectile worldTree = Main.projectile[(int)base.projectile.ai[0]];
			base.projectile.Center = worldTree.Center;
			base.projectile.velocity = Vector2.Zero;
			base.projectile.rotation += 0.04f;
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(ModContent.BuffType<FireForceBuff>(), 1800, false);
				}
			}
			for (int p2 = 0; p2 < 200; p2++)
			{
				this.clearCheck2 = Main.npc[p2];
				if (!this.clearCheck2.immortal && !this.clearCheck2.dontTakeDamage && !this.clearCheck2.friendly && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck2.position, this.clearCheck2.Size))
				{
					this.clearCheck2.AddBuff(24, 10, false);
				}
			}
		}

		private Player clearCheck;

		private NPC clearCheck2;
	}
}
