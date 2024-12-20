using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class LavaCubeMinion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lava Cube");
			Main.projFrames[base.projectile.type] = 6;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.CloneDefaults(266);
			base.projectile.width = 36;
			base.projectile.height = 26;
			base.projectile.minion = true;
			base.projectile.friendly = true;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = true;
			base.projectile.netImportant = true;
			this.aiType = 266;
			base.projectile.alpha = 30;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.minionSlots = 1f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (base.projectile.penetrate == 0)
			{
				base.projectile.Kill();
			}
			return false;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(24, 160, false);
		}

		public override void AI()
		{
			bool flag = base.projectile.type == base.mod.ProjectileType("LavaCubeMinion");
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (flag)
			{
				if (player.dead)
				{
					modPlayer.lavaCubeMinion = false;
				}
				if (modPlayer.lavaCubeMinion)
				{
					base.projectile.timeLeft = 2;
				}
			}
			if (Main.rand.Next(2) == 0)
			{
				int dust = Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
			}
		}

		public override bool MinionContactDamage()
		{
			return true;
		}
	}
}
