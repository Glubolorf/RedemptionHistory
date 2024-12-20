using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.DruidProjectiles.WorldStave
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
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			base.projectile.velocity.Y = 0f;
			base.projectile.velocity.X = 0f;
			base.projectile.rotation += 0.04f;
			if (base.projectile.localAI[0] >= 900f)
			{
				base.projectile.Kill();
			}
			for (int p = 0; p < 255; p++)
			{
				this.clearCheck = Main.player[p];
				if (Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck.position, this.clearCheck.Size))
				{
					this.clearCheck.AddBuff(base.mod.BuffType("FireForceBuff"), 1800, false);
				}
			}
			for (int p2 = 0; p2 < Main.npc.Length; p2++)
			{
				this.clearCheck2 = Main.npc[p2];
				if (!this.clearCheck2.immortal && !this.clearCheck2.dontTakeDamage && Collision.CheckAABBvAABBCollision(base.projectile.position, base.projectile.Size, this.clearCheck2.position, this.clearCheck2.Size))
				{
					this.clearCheck2.AddBuff(24, 10, false);
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
			target.AddBuff(24, 160, false);
		}

		private Player clearCheck;

		private NPC clearCheck2;
	}
}
